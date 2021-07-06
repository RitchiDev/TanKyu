using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinMenu : MonoBehaviour
{
    private AudioManager m_AudioManager;

    [Header("Audio")]
    [SerializeField] private AudioClip m_SelectSound;
    [SerializeField] private AudioClip m_WinSound;

    [Header("WinScreen")]
    [SerializeField] private GameObject m_WinScreen;
    [SerializeField] private GameObject m_WinImageP1, m_WinImageP2;

    void Start()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Restart()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        Application.Quit();
    }

    public void TriggerWinscreenP1()
    {
        m_WinScreen.SetActive(true);

        m_WinImageP1.SetActive(true);
        m_WinImageP2.SetActive(false);

        Time.timeScale = 1;
    }

    public void TriggerWinscreenP2()
    {

        m_WinScreen.SetActive(true);

        m_WinImageP1.SetActive(false);
        m_WinImageP2.SetActive(true);

        m_AudioManager.PlaySound(m_WinSound);
        Time.timeScale = 1;
    }
}
