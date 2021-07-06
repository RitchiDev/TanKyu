using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();   
    }

    public void DisableMusic()
    {
        m_AudioSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        m_AudioSource.PlayOneShot(sound);
    }
}
