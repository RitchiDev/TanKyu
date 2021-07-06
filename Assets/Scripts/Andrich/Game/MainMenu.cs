using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioManager m_AudioManager;

    [Header("Audio")]
    [SerializeField] private AudioClip m_SelectSound;
    [SerializeField] private AudioClip m_ChangeSound;
    [SerializeField] private AudioClip m_BackSound;

    [Header("Menus")]
    [SerializeField] private GameObject m_ShowMenu;
    [SerializeField] private GameObject m_ShowArenas;
    [SerializeField] private GameObject m_ShowControls;

    [SerializeField] private Text m_GameMode;

    private string m_DeathMatch = "DeathMatch";
    private string m_SpearThrow = "SpearThrow";
    private string m_SpikeBall = "SpikeBall";

    private void Start()
    {
        Time.timeScale = 1;

        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        m_GameMode.text = "DeathMatch";


        m_ShowArenas.SetActive(false);
        m_ShowControls.SetActive(false);

        m_ShowMenu.SetActive(true);
    }

    public void ChangeGameMode()
    {
        m_AudioManager.PlaySound(m_ChangeSound);

        if (m_GameMode.text == m_DeathMatch)
        {
            m_GameMode.text = m_SpearThrow;
        }
        else if (m_GameMode.text == m_SpearThrow)
        {
            m_GameMode.text = m_SpikeBall;
        }
        else if (m_GameMode.text == m_SpikeBall)
        {
            m_GameMode.text = m_DeathMatch;
        }
        else
        {
            m_GameMode.text = m_DeathMatch;
        }
    }

    public void LoadParkArena()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        if (m_GameMode.text == m_DeathMatch)
        {
            SceneManager.LoadScene("ParkDM");
        }
        else if (m_GameMode.text == m_SpearThrow)
        {
            SceneManager.LoadScene("ParkST");
        }
        else if (m_GameMode.text == m_SpikeBall)
        {
            SceneManager.LoadScene("ParkSB");
        }
    }

    public void LoadDungeonArena()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        if (m_GameMode.text == m_DeathMatch)
        {
            SceneManager.LoadScene("DungeonDM");
        }
        else if (m_GameMode.text == m_SpearThrow)
        {
            SceneManager.LoadScene("DungeonST");
        }
        else if (m_GameMode.text == m_SpikeBall)
        {
            SceneManager.LoadScene("DungeonSB");
        }
    }

    public void ShowArenas()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        m_ShowMenu.SetActive(false);
        m_ShowControls.SetActive(false);

        m_ShowArenas.SetActive(true);
    }

    public void ShowControls()
    {
        m_AudioManager.PlaySound(m_SelectSound);

        m_ShowMenu.SetActive(false);
        m_ShowArenas.SetActive(false);

        m_ShowControls.SetActive(true);
    }
    public void Back()
    {
        m_AudioManager.PlaySound(m_BackSound);

        m_ShowArenas.SetActive(false);
        m_ShowControls.SetActive(false);

        m_ShowMenu.SetActive(true);
    }

    public void ExitGame()
    {
        m_AudioManager.PlaySound(m_SelectSound);
        //Debug.Log("Closed game");
        Application.Quit();
    }
}
