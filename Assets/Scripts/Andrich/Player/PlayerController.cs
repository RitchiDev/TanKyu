using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player m_Player;
    private ScreenShake m_Camera;
    private Rigidbody2D m_Rigidbody;
    private Animator m_PlayerAnimation;
    private AudioManager m_AudioManager;
    private SpriteRenderer m_SpriteRenderer;

    [Header("Roll")]
    [SerializeField] private float RollDelay = 0.7f;
    [SerializeField] private float m_RollDistance = 15f;
    private bool m_PlayerRolled;
    private float m_RollTimer;

    [Header("Movement Speed")]
    [SerializeField] private float m_NeutralSpeed = 5f;
    [SerializeField] private float m_SlowSpeed = 2f;
    [SerializeField] private float m_FastSpeed = 8f;
    [SerializeField] private float m_TimeMovementDisabled = 0.2f;
    private float m_DisableMovementTimer;
    private Vector2 m_MovementInput;
    private float m_PlayerSpeed;

    void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();

        m_Player = GetComponent<Player>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimation = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_PlayerSpeed = m_NeutralSpeed;
    }

    private void Update()
    {
        InputChecker();

        PlayerAnimation();

        PlayerFlipper();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        RollChecker();
    }

    private void InputChecker()
    {
        if (gameObject.tag == "PlayerOne")
        {
            m_MovementInput.x = Input.GetAxis("HorizontalP1");
            m_MovementInput.y = Input.GetAxis("VerticalP1");
            m_PlayerRolled = Input.GetKeyDown(KeyCode.G);
        }
        else
        {
            m_MovementInput.x = Input.GetAxis("HorizontalP2");
            m_MovementInput.y = Input.GetAxis("VerticalP2");
            m_PlayerRolled = Input.GetKeyDown(KeyCode.Comma);
        }
    }

    private void MovePlayer()
    {
        if(m_DisableMovementTimer <= 0)
        {
            m_Rigidbody.velocity = Vector2.zero;
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_MovementInput * m_PlayerSpeed * Time.deltaTime);

        }
        else
        {
            m_DisableMovementTimer -= Time.deltaTime;
        }
    }

    private void PlayerAnimation()
    {
        m_PlayerAnimation.SetFloat("Movement", Mathf.Abs(m_MovementInput.x) + Mathf.Abs(m_MovementInput.y)); //Mathf.Abs geeft altijd een positief getal :)
    }

    private void PlayerFlipper()
    {
        if(m_MovementInput.x > 0)
        {
            m_SpriteRenderer.flipX = false;
        }
        else if(m_MovementInput.x < 0)
        {
            m_SpriteRenderer.flipX = true;
        }
    }

    private void RollChecker()
    {
        
        if (m_RollTimer <= 0)
        {
            if(m_PlayerRolled && m_MovementInput != Vector2.zero) //Een korte manier van (0.0) schrijven
            {
                Roll();
            }
        }
        else
        {
            m_RollTimer -= Time.deltaTime;
        }
    }

    private void Roll()
    {
        m_Player.TriggerRollInvincibility();
        m_Camera.TriggerScreenShake();
        m_Rigidbody.AddForce(m_MovementInput.normalized * m_RollDistance, ForceMode2D.Impulse);

        m_DisableMovementTimer = m_TimeMovementDisabled;
        m_RollTimer = RollDelay;
    }

    public void SlowSpeed()
    {
        m_PlayerSpeed = m_SlowSpeed;
    }

    public void NeutralSpeed()
    {
        m_PlayerSpeed = m_NeutralSpeed;
    }

    public void FastSpeed()
    {
        m_PlayerSpeed = m_FastSpeed;
    }
}




//m_Rigidbody.AddForce(m_DashAim.transform.localPosition * m_DashSpeed, ForceMode2D.Impulse);
//gameObject.GetComponent<Rigidbody2D>().AddForce(m_MovementInput.normalized * m_DashSpeed, ForceMode2D.Impulse);

//Debug.Log(m_MovementInput.x + "/" + m_MovementInput.y);

//if(m_PlayerDashed)
//{
//m_Rigidbody.AddForce(transform.up * 3000);
//}


//m_MovementInput.Normalize();

//if (m_MovementInput.x != 0)
//{
//    m_Rigidbody.AddForce(new Vector2(m_MovementInput.x, 0) * m_DashSpeed, ForceMode2D.Impulse);
//}

//if(m_MovementInput.y != 0)
//{
//    m_Rigidbody.AddForce(new Vector2(0, m_MovementInput.y) * m_DashSpeed, ForceMode2D.Impulse);
//}
//if(gameObject.tag == "PlayerTwo")
//{
//    m_MovementInput.x = 1f;
//}

//m_MovementInput = Vector2.zero; //Een korte manier van (0.0) schrijven
//m_MovementInput.x = 0;
//Debug.Log(m_MovementInput);


//m_Rigidbody.AddForce(m_Rigidbody.transform.up * m_DashSpeed);
//m_Rigidbody.velocity = dashAngle * m_DashSpeed;
//Vector2 dashAngle = m_Rigidbody.position + m_MovementInput * m_DashSpeed;
//m_Rigidbody.AddForce(dashAngle, ForceMode2D.Impulse);
//m_Camera.TriggerSmallScreenShake();
//float a = Vector3.Distance(m_Rigidbody.position, dashPosition);
//Vector3 b = new Vector3(a, a, a);
//GameObject dashEffect = Instantiate(m_DashEffect, m_Rigidbody.position += new Vector2(1.5f, 0), Quaternion.identity);
//RaycastHit2D hit = Physics2D.Raycast(m_Rigidbody.position, m_MovementInput.normalized, m_DashDistance, m_DashDetection);
//Vector3 dashPosition = m_Rigidbody.position + m_MovementInput.normalized * m_DashDistance;
//if(hit.collider != null)
//                {
//                    dashPosition = hit.point; //- new Vector2(0.8f, 0.8f);   
//                }

//                GameObject dashEffect = Instantiate(m_DashEffect, m_Rigidbody.position, Quaternion.identity);
//dashEffect.transform.Rotate(0, 0, Mathf.Atan2(m_MovementInput.normalized.y, m_MovementInput.normalized.x) * Mathf.Rad2Deg);
//dashEffect.transform.localScale = new Vector3(m_DashDistance / 3, dashEffect.transform.localScale.y, dashEffect.transform.localScale.z);

//Destroy(dashEffect, 0.1f);
//Vector3 dashAngle = m_MovementInput.normalized;

//m_Rigidbody.MovePosition(dashPosition);