using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    private AudioManager m_AudioManager;

    [SerializeField] private float m_ReduceScaleSpeed = 1.2f;
    [SerializeField] private float m_RotateSpeed = 50;
    private void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(transform.localScale.x > 0 && transform.localScale.y > 0)
        {
            transform.Rotate(0, 0, m_RotateSpeed * 10 * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x - m_ReduceScaleSpeed * Time.deltaTime, transform.localScale.y - m_ReduceScaleSpeed * Time.deltaTime, transform.localScale.z); 
        }
    }
}
