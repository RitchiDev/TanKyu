using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{ 
    Allow_Shooting = 0,
    Allow_Throwing,
    No_Projectiles,
}


public class Player : MonoBehaviour
{
    //[Header("Components")]
    private AudioManager m_AudioManager;
    private GameManager m_GameManager;
    private ScreenShake m_Camera;
    private PlayerController m_PlayerController;
    private PlayerShooting m_PlayerShooting;
    private PlayerThrowing m_PlayerThrowing;
    private Animator m_PlayerAnimation;

    [SerializeField] private PlayerType m_PlayerType;

    [Header("Audio")]
    [SerializeField] private AudioClip m_HurtSound;


    [Header("Health")]
    [SerializeField] private float m_MaxPlayerHealth = 3f;
    [SerializeField] private GameObject[] m_Hearts;
    private float m_PlayerHealth;

    [Header("Invincibility")]
    [SerializeField] private float m_TimeInvincible = 0.8f;
    private bool m_PlayerIsInvincible;
    private float m_InvincibleTimer;

    private float m_DeathTimer;


    private void Start()
    {
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();

        m_PlayerAnimation = GetComponent<Animator>();
        m_PlayerController = GetComponent<PlayerController>();


        if(m_PlayerType == PlayerType.Allow_Shooting)
        {
            m_PlayerShooting = GetComponent<PlayerShooting>();
        }
        else if(m_PlayerType == PlayerType.Allow_Throwing)
        {
            m_PlayerThrowing = GetComponent<PlayerThrowing>();
        }

        ResetPlayer();
    }


    private void Update()
    {
        InvincibleChecker();
        HealthChecker();
    }


    private void InvincibleChecker()
    {
       if(m_PlayerIsInvincible)
        {
            m_InvincibleTimer -= Time.deltaTime;

            if(m_InvincibleTimer < 0)
            {

                m_PlayerIsInvincible = false;
                m_PlayerAnimation.SetBool("Hit", false);
                m_PlayerAnimation.SetBool("Roll", false);

                if (gameObject.tag == "PlayerOne")
                {
                    gameObject.layer = 8; //8 = PlayerOne (Layer)
                }
                else
                {
                    gameObject.layer = 9; //9 = PlayerTwo (Layer)
                }
            }
        }
    }


    public void ChangeHealth(float amount)
    {

        if(amount < 0) //Als de speler schade neemt
        {
            if(m_PlayerIsInvincible)
            {
                return;
            }


            m_AudioManager.PlaySound(m_HurtSound);
            m_PlayerAnimation.SetBool("Hit", true);
            
            m_PlayerIsInvincible = true;
            m_InvincibleTimer = m_TimeInvincible;

            gameObject.layer = 12; //12 = Invincible (Layer)
        }

        m_PlayerHealth = Mathf.Clamp(m_PlayerHealth + amount, 0, m_MaxPlayerHealth); //Mathf.Clamp houdt het getal tussen het minimum en het maximum

        HealthChecker();
    }

    private void HealthChecker()
    {
        if(m_PlayerHealth == 3)
        {
            for (int i = 0; i < m_Hearts.Length; i++)
            {
                m_Hearts[i].SetActive(true);
            }
        }
        else if(m_PlayerHealth == 2)
        {
            m_Hearts[0].SetActive(true);
            m_Hearts[1].SetActive(true);
            m_Hearts[2].SetActive(false);

        }
        else if(m_PlayerHealth == 1)
        {
            m_Hearts[0].SetActive(true);
            m_Hearts[1].SetActive(false);
            m_Hearts[2].SetActive(false);


        }
        else if (m_PlayerHealth <= 0)
        {
            m_Hearts[0].SetActive(false);
            m_Hearts[1].SetActive(false);
            m_Hearts[2].SetActive(false);

            m_GameManager.TriggerSlowMo();
            m_Camera.TriggerSmallScreenShake();
            DisableComponents();

            m_DeathTimer -= Time.deltaTime;

            if (m_DeathTimer < 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        m_GameManager.TriggerRespawn(gameObject);

        ResetPlayer();

    }

    private void ResetPlayer()
    {
        m_PlayerHealth = m_MaxPlayerHealth;
        m_DeathTimer = m_TimeInvincible;

        for (int i = 0; i < m_Hearts.Length; i++)
        {
            m_Hearts[i].SetActive(true);
        }

        TriggerInvincibility();
        EnableComponents();
        m_PlayerController.NeutralSpeed();
        m_PlayerAnimation.SetBool("Hit", false);
        m_PlayerAnimation.SetBool("Roll", false);

    }

    private void TriggerInvincibility()
    {
        gameObject.layer = 16; //Layer 16 invincible
        m_PlayerIsInvincible = true;
        m_InvincibleTimer = m_TimeInvincible;
    }

    public void TriggerRollInvincibility()
    {
        m_InvincibleTimer = (m_TimeInvincible / 2) - 0.2f;
        gameObject.layer = 16; //Layer 16 invincible
        m_PlayerAnimation.SetBool("Roll", true);
        m_PlayerIsInvincible = true;
    }

    private void EnableComponents()
    {

        if (m_PlayerType == PlayerType.Allow_Shooting)
        {
            m_PlayerShooting.enabled = true;
        }
        else if (m_PlayerType == PlayerType.Allow_Throwing)
        {
            m_PlayerThrowing.enabled = true;
        }

        m_PlayerController.enabled = true;

    }

    private void DisableComponents()
    {
        if (m_PlayerType == PlayerType.Allow_Shooting)
        {
            m_PlayerShooting.enabled = false;
        }
        else if (m_PlayerType == PlayerType.Allow_Throwing)
        {
            m_PlayerThrowing.enabled = false;
        }

        m_PlayerController.enabled = false;
    }
}



//m_RigidBody = GetComponent<Rigidbody2D>();
//m_PlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
//m_PlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");

//m_RigidBody.AddForce((m_PlayerOne.transform.position - m_PlayerTwo.transform.position).normalized * 20, ForceMode2D.Impulse);
//m_RigidBody.velocity = new Vector3(50, 500, 500);


//for (int i = 0; i < m_Hearts.Length - 2; i++)
//{
//    m_Hearts[i].SetActive(true);
//}


//for (int i = 0; i < m_Hearts.Length - 1; i++)
//{
//    m_Hearts[i].SetActive(true);
//}

//m_PlayerAnimation.CrossFade("Hit", m_TimeInvincible);
//HealthChecker();


//m_PlayerHealth = m_MaxPlayerHealth;

//m_DeathTimer = m_TimeInvincible - 0.05f;

//for (int i = 0; i < m_Hearts.Length; i++)
//{
//    m_Hearts[i].SetActive(true);
//}


//private Rigidbody2D m_RigidBody;
//private GameObject m_PlayerOne, m_PlayerTwo; 

//private Color m_FullAlpha;
//private Color m_SeeThrough;

//m_FullAlpha = GetComponent<SpriteRenderer>().color;
//m_FullAlpha.a = 255f;
//m_SeeThrough = GetComponent<SpriteRenderer>().color;
//m_SeeThrough.a = 80f;

//m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

//m_SpriteRenderer.color = m_FullAlpha;

//m_SpriteRenderer.color = m_SeeThrough;
//private SpriteRenderer m_SpriteRenderer;
//private Rigidbody2D m_Rigidbody;
//m_Rigidbody = GetComponent<Rigidbody2D>();
