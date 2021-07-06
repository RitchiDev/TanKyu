using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private AudioManager m_AudioManager;
    private SpriteRenderer m_SpriteRenderer;

    //[Header("Audio")]

    [SerializeField] private Sprite[] m_SpikeTrapFrames;
    private int m_SpikeTrapFrameNumber = 1;

    private float m_AnimationTimer;
    [SerializeField] private float m_AnimationSpeed = 1;

    [SerializeField] private int m_Damage = 1;

    private void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        SpikeTrapAnimation();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (m_SpikeTrapFrameNumber == 5 || m_SpikeTrapFrameNumber == 6)
        {
            if (collision.gameObject.tag == "PlayerOne" || collision.gameObject.tag == "PlayerTwo")
            {
                collision.gameObject.GetComponent<Player>().ChangeHealth(-m_Damage);
            }
        }
    }

    void SpikeTrapAnimation()
    {
        m_AnimationTimer += Time.deltaTime;

        if (m_AnimationTimer > (1f / m_AnimationSpeed))
        {
            m_SpikeTrapFrameNumber++;
            //Debug.Log(m_SpikeTrapAnimationIndex);
            if (m_SpikeTrapFrameNumber == m_SpikeTrapFrames.Length)
            {
                m_SpikeTrapFrameNumber = 0;

            }
            m_SpriteRenderer.sprite = m_SpikeTrapFrames[m_SpikeTrapFrameNumber];
            m_AnimationTimer = 0f;
        }
    }
}
