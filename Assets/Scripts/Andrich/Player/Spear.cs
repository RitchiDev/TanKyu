using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private ScreenShake m_Camera;
    private AudioManager m_AudioManager;
    private Rigidbody2D M_RigidBody;

    [Header("Audio")]
    [SerializeField] private AudioClip m_ImpactSound;

    [SerializeField] private float m_ProjectileDamage = 1f;

    private void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
        M_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.layer == 14) //SpearP1
        {
            if (collision.gameObject.tag == "PlayerTwo")
            {
                collision.gameObject.GetComponent<Player>().ChangeHealth(-m_ProjectileDamage);
            }
            else// if(collision.gameObject.tag == "Border")
            {
                m_AudioManager.PlaySound(m_ImpactSound);

                gameObject.layer = 16; //PickUpSpear;
                M_RigidBody.velocity = Vector2.zero;
                m_Camera.TriggerScreenShake();
            }
        }
        else if(gameObject.layer == 15) //SpearP2
        {
            if (collision.gameObject.tag == "PlayerOne")
            {
                collision.gameObject.GetComponent<Player>().ChangeHealth(-m_ProjectileDamage);
            }
            else// if(collision.gameObject.tag == "Border")
            {
                m_AudioManager.PlaySound(m_ImpactSound);

                gameObject.layer = 16; //PickUpSpear;
                M_RigidBody.velocity = Vector2.zero;
                m_Camera.TriggerScreenShake();
            }
        }


        if(gameObject.layer == 16) //PickUpSpear
        {
            if(collision.gameObject.tag == "PlayerOne" || collision.gameObject.tag == "PlayerTwo")
            {
                if(collision.gameObject.GetComponent<PlayerThrowing>().AllowPickup())
                {
                    collision.gameObject.GetComponent<PlayerThrowing>().PickupSpear(gameObject);
                }

            }
        }
    }
}
