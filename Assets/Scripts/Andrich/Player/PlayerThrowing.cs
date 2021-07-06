using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{
    private AudioManager m_AudioManager;
    private PlayerController m_PlayerController;

    private Vector2 m_MovementInput;

    [Header("Audio")]
    [SerializeField] private AudioClip m_ShootSound;

    [Header("Spear")]
    [SerializeField] private GameObject m_SpearPrefab;
    [SerializeField] private float m_ThrowingSpeed = 6f;
    private bool m_SpearIsAvailable;
    private bool m_PreparingToThrow;
    private bool m_Throw;

    [Header("Crosshair")]
    [SerializeField] private GameObject m_Crosshair;
    [SerializeField] private float m_CrosshairRotateSpeed = 20f;
    [SerializeField] private float m_CrosshairOffset = 4f;

    private void Start()
    {

        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        m_Crosshair.SetActive(false);
        m_PlayerController = GetComponent<PlayerController>();

        m_SpearPrefab.SetActive(true);
        m_SpearIsAvailable = true;

    }

    void Update()
    {
        InputChecker();

        Aim();
    }

    private void InputChecker()
    {
        if (gameObject.tag == "PlayerOne")
        {
            m_MovementInput.x = Input.GetAxis("HorizontalP1");
            m_MovementInput.y = Input.GetAxis("VerticalP1");
            m_PreparingToThrow = Input.GetKey(KeyCode.F);
            m_Throw = Input.GetKeyUp(KeyCode.F);

        }
        else
        {
            m_MovementInput.x = Input.GetAxis("HorizontalP2");
            m_MovementInput.y = Input.GetAxis("VerticalP2");
            m_PreparingToThrow = Input.GetKey(KeyCode.Period);
            m_Throw = Input.GetKeyUp(KeyCode.Period);
        }


        if (m_SpearIsAvailable)
        {
            if (m_PreparingToThrow)
            {
                m_PlayerController.SlowSpeed();
            }
        }
        else
        {
            m_PlayerController.NeutralSpeed();
        }
    }

    void Aim()
    {

        if (gameObject.tag == "PlayerOne")
        {
            m_SpearPrefab.layer = 14; //SpearP1
        }
        else if (gameObject.tag == "PlayerTwo")
        {
            m_SpearPrefab.layer = 15; //SpearP2
        }

        if (m_MovementInput != Vector2.zero) //Een korte manier van (0.0) schrijven
        {
            m_Crosshair.SetActive(true);
            m_Crosshair.transform.localPosition = m_MovementInput.normalized * m_CrosshairOffset;
            m_Crosshair.transform.rotation = Quaternion.identity;

        }
        else
        {
            RotateCrosshair();
        }


        if (m_Throw && m_Crosshair.transform.localPosition != Vector3.zero) //Een korte manier van (0.0.0) schrijven
        {
            if(m_SpearIsAvailable)
            {
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        m_AudioManager.PlaySound(m_ShootSound);

        Vector3 shootAngle = m_Crosshair.transform.localPosition;
        GameObject projectile = Instantiate(m_SpearPrefab, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(shootAngle * m_ThrowingSpeed, ForceMode2D.Impulse);
        projectile.transform.Rotate(0, 0, Mathf.Atan2(shootAngle.y, shootAngle.x) * Mathf.Rad2Deg);

        m_SpearIsAvailable = false;
    }

    public void PickupSpear(GameObject spear)
    {
        Destroy(spear);
        m_SpearIsAvailable = true;
    }

    public bool AllowPickup()
    {
        if(m_SpearIsAvailable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void RotateCrosshair()
    {
        m_Crosshair.transform.Rotate(0, 0, m_CrosshairRotateSpeed * 10 * Time.deltaTime);
    }
}
