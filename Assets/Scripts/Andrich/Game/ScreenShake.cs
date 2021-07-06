using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Transform m_Transform;

    Vector3 m_StartPosition;
    [SerializeField] private float m_ShakeTime = 2;
    [SerializeField] private float m_ShakeAmount = 0.7f;
    [SerializeField] private float m_StopSpeed = 1.0f;
    private float m_Shake;


    private void Start()
    {
        m_Transform = GetComponent<Transform>();
        m_StartPosition = m_Transform.position;
    }

    void Update()
    {
        if (m_Shake > 0)
        {
            m_Transform.position += Random.insideUnitSphere * m_ShakeAmount;
            m_Shake -= Time.deltaTime * m_StopSpeed;
        }
        else
        {
            m_Shake = 0f;
            m_Transform.position = m_StartPosition;
        }
    }

    public void TriggerScreenShake()
    {
        m_Shake = m_ShakeTime;
    }

    public void TriggerSmallScreenShake()
    {
        m_Shake = 0.1f;
    }
}
