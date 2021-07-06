using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AudioManager m_AudioManager;

    [Header("Audio")]
    [SerializeField] private AudioClip m_ImpactSound;

    [SerializeField] private GameObject m_ImpactEffect;
    [SerializeField] private float m_ProjectileDamage = 1f;

    private void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerOne" || collision.gameObject.tag == "PlayerTwo")
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(-m_ProjectileDamage);
        }

        m_AudioManager.PlaySound(m_ImpactSound);
        GameObject impactEffect = Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);

        Destroy(impactEffect, 0.2f);
        Destroy(gameObject);
    }
}



//collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500,500), ForceMode2D.Impulse);
