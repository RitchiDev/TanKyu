using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private AudioManager m_AudioManager;
    private Vector2 m_MovementInput;
    private Animator m_PlayerAnimation;

    [Header("Audio")]
    [SerializeField] private AudioClip m_ShootSound;

    [Header("Projectile")]
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private float m_ProjectileSpeed = 2f;
    [SerializeField] private float m_DestroyDelay = 2f;
    [SerializeField] private float ShootDelay = 0.5f;
    private bool m_PlayerShot;
    private float m_ShootTimer;
    private float m_ShootAnimationTimer;

    [Header("Crosshair")]
    [SerializeField] private GameObject m_Crosshair;
    [SerializeField] private float m_CrosshairRotateSpeed = 20f;
    [SerializeField] private float m_CrosshairOffset = 4f;

    private void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        m_PlayerAnimation = GetComponent<Animator>();

        m_Crosshair.SetActive(false);
        m_ShootTimer = ShootDelay;

    }
    void Update()
    {
        InputChecker();

        ShootChecker();

        Aim();
    }

    private void InputChecker()
    {
        if (gameObject.tag == "PlayerOne")
        {
            m_MovementInput.x = Input.GetAxis("HorizontalP1");
            m_MovementInput.y = Input.GetAxis("VerticalP1");
            m_PlayerShot = Input.GetKeyDown(KeyCode.F);
        }
        else
        {
            m_MovementInput.x = Input.GetAxis("HorizontalP2");
            m_MovementInput.y = Input.GetAxis("VerticalP2");
            m_PlayerShot = Input.GetKeyDown(KeyCode.Period);
        }
    }

    void Aim()
    {
        if(m_MovementInput != Vector2.zero) //Een korte manier van (0.0) schrijven
        {

            m_Crosshair.SetActive(true);
            m_Crosshair.transform.localPosition = m_MovementInput.normalized * m_CrosshairOffset;
            m_Crosshair.transform.rotation = Quaternion.identity;

        }
        else
        {
            RotateCrosshair();
        }

        m_ShootTimer -= Time.deltaTime;
        if (m_PlayerShot && m_Crosshair.transform.localPosition != Vector3.zero) //Een korte manier van (0.0.0) schrijven
        {
            if (m_ShootTimer <= 0)
            {
                Shoot();
                m_ShootTimer = ShootDelay;
            }
        }
    }

    private void Shoot()
    {
        m_ShootAnimationTimer = 0.2f;
        m_AudioManager.PlaySound(m_ShootSound);

        Vector2 shootAngle = m_Crosshair.transform.localPosition;
        GameObject projectile = Instantiate(m_ProjectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(shootAngle * m_ProjectileSpeed, ForceMode2D.Impulse);
        projectile.transform.Rotate(0, 0, Mathf.Atan2(shootAngle.y, shootAngle.x) * Mathf.Rad2Deg);

    }

    private void ShootChecker()
    {
        if(m_ShootAnimationTimer > 0)
        {
            m_ShootAnimationTimer -= Time.deltaTime;
            m_PlayerAnimation.SetBool("Shoot", true);
        }
        else
        {
            m_PlayerAnimation.SetBool("Shoot", false);
        }
    }

    private void RotateCrosshair()
    {
        m_Crosshair.transform.Rotate(0, 0, m_CrosshairRotateSpeed * 10 * Time.deltaTime);
    }
}



//[SerializeField] private GameObject m_Gun;
//[SerializeField] private float m_GunOffset = 1f;

//m_Gun.transform.localPosition = m_MovementInput.normalized* m_GunOffset;
