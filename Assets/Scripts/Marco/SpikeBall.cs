using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    private ScreenShake m_Camera;
    private AudioManager m_AudioManager;
    private Rigidbody2D m_RigidBody;

    private int count;

    //[Header("speed gain Time")]
    //[SerializeField]private Transform P1;

    //[Header("speed gain Time")]
    //[SerializeField] private Transform P2;

    [Header("speed gain Time")]
    [SerializeField] private int speedGainTime = 200;

    [Header("speed gain increase")]
    [SerializeField] private float speedGainIncrease = 1f;

    private float maxSpeedGain = 20f;
    private float controlledSpeed = 5f;

    [Header("Audio")]
    [SerializeField] private AudioClip m_BounceSound;

    [Header("Speed")]
    [SerializeField] private float m_Speed = 5;
    [SerializeField] private float m_RotateSpeed = 10;

    [Header("Damage")]
    [SerializeField] private int m_Damage = 1;

    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(8, 9);
        //m_RigidBody.AddForce(m_RigidBody.transform.up * 50, ForceMode2D.Impulse);
        //m_RigidBody.AddForce(m_RigidBody.transform.right * 50, ForceMode2D.Impulse);

        m_RigidBody.velocity = new Vector2(m_Speed, m_Speed);
        //m_Speed = 30f;

    }

    void Update()
    {
        //m_Speed = Time.deltaTime;
        RotateSpikeBall();
        SpikeBallSpeedGain();
        //m_speed = controlledSpeed;
    }

    private void SpikeBallSpeedGain()
    {
        m_Speed = controlledSpeed;
        count++;
        if (count > speedGainTime)
        {
            //if (maxSpeedGain < controlledSpeed)
            //{
            controlledSpeed = controlledSpeed + speedGainIncrease;
            //}
            count = 0;
        }
    }

    private void RotateSpikeBall()
    {
        m_RigidBody.transform.Rotate(0, 0, m_RotateSpeed * 10 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //m_RigidBody.AddForce(m_RigidBody.transform.up * m_Speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerOne" || collision.gameObject.tag == "PlayerTwo")
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(-m_Damage);
        }

        m_AudioManager.PlaySound(m_BounceSound);
        m_Camera.TriggerScreenShake();
    }
}
