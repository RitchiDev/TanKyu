using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ScreenShake m_Camera;
    private PauseMenu m_PauseMenu;
    private WinMenu m_WinMenu;
    private GameObject m_DeadPlayer;
    private bool m_PlayerHasWon;

    [Header("Audio")]
    private AudioManager m_AudioManager;
    [SerializeField] private AudioClip m_SpawnSound;
    [SerializeField] private AudioClip m_WinSound;
    [SerializeField] private GameObject m_CountdownAnimation;

    [SerializeField] private int m_MaxPlayerLives = 2;
    private Text m_LivesTextP1, m_LivesTextP2;
    private int m_LivesP1, m_LivesP2;

    [SerializeField] private GameObject[] m_SpawnpointsP1, m_SpawnpointsP2;
    private GameObject m_WhichPlaceToSpawn;

    [SerializeField] private GameObject m_RedSpawnEffect, m_BlueSpawnEffect;
    private GameObject m_SpawnEffect;

    [SerializeField] private GameObject m_RedDeathEffect, m_BlueDeathEffect;
    private GameObject m_DeathEffect;

    [SerializeField] private float m_SlowmoTime = 0.3f;

    
    void Start()
    {
        m_CountdownAnimation.SetActive(true);
        StartCoroutine("Countdown");

        m_AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();

        m_LivesTextP1 = GameObject.FindGameObjectWithTag("LivesTextP1").GetComponent<Text>();
        m_LivesTextP2 = GameObject.FindGameObjectWithTag("LivesTextP2").GetComponent<Text>();
        m_LivesP1 = m_MaxPlayerLives ;
        m_LivesP2 = m_MaxPlayerLives;
        m_LivesTextP1.text = "P1: " + m_LivesP1.ToString() + "/" + m_MaxPlayerLives.ToString();
        m_LivesTextP2.text = "P2: " + m_LivesP2.ToString() + "/" + m_MaxPlayerLives.ToString();

        m_PauseMenu = GetComponent<PauseMenu>();
        m_WinMenu = GetComponent<WinMenu>();
        m_PauseMenu.enabled = false;
    }

    IEnumerator Countdown()
    {
        Time.timeScale = 0;

        //float timeBeforeStart = WaitForSecondsRealtime(3);
        float timeBeforeStart = Time.realtimeSinceStartup + 2.5f;

        while(Time.realtimeSinceStartup < timeBeforeStart)
        {
            yield return 0;
        }

        m_CountdownAnimation.gameObject.SetActive(false);
        Time.timeScale = 1;
        m_PauseMenu.enabled = true;
    }

    public void TriggerRespawn(GameObject player)
    {
        m_DeadPlayer = player;

        if (m_DeadPlayer.tag == "PlayerOne")
        {
            m_LivesP1 = Mathf.Clamp(m_LivesP1 - 1, 0, m_MaxPlayerLives); //Mathf.Clamp houdt het getal tussen het minimum en het maximum
            m_LivesTextP1.text = "P1: " + m_LivesP1.ToString() + "/" + m_MaxPlayerLives.ToString();

            CheckForWin();

            if(m_PlayerHasWon == false)
            {

                m_WhichPlaceToSpawn = m_SpawnpointsP1[Random.Range(0, m_SpawnpointsP1.Length)];
                m_DeathEffect = Instantiate(m_RedDeathEffect, m_DeadPlayer.transform.position, Quaternion.identity);
                m_SpawnEffect = Instantiate(m_RedSpawnEffect, m_WhichPlaceToSpawn.transform.position, Quaternion.identity);
                RespawnPlayer();

                return;
            }
        }
        else
        {
            m_LivesP2 = Mathf.Clamp(m_LivesP2 - 1, 0, m_MaxPlayerLives); //Mathf.Clamp houdt het getal tussen het minimum en het maximum
            m_LivesTextP2.text = "P2: " + m_LivesP2.ToString() + "/" + m_MaxPlayerLives.ToString();

            CheckForWin();

            if(m_PlayerHasWon == false)
            {
                m_WhichPlaceToSpawn = m_SpawnpointsP2[Random.Range(0, m_SpawnpointsP2.Length)];
                m_DeathEffect = Instantiate(m_BlueDeathEffect, m_DeadPlayer.transform.position, Quaternion.identity);
                m_SpawnEffect = Instantiate(m_BlueSpawnEffect, m_WhichPlaceToSpawn.transform.position, Quaternion.identity);
                RespawnPlayer();

                return;
            }
        }

        m_AudioManager.PlaySound(m_WinSound);
    }

    private void CheckForWin()
    {

        if (m_LivesP1 <= 0) //Speler 2 Heeft gewonnen!
        {
            m_WinMenu.TriggerWinscreenP2();
            DisableEverything();

            return;
        }
        else if (m_LivesP2 <= 0) //Speler 1 heeft gewonnen!
        {
            m_WinMenu.TriggerWinscreenP1();
            DisableEverything();

            return;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void RespawnPlayer()
    {

        m_DeadPlayer.transform.position = m_WhichPlaceToSpawn.transform.position;

        m_Camera.TriggerScreenShake();

        Destroy(m_SpawnEffect, 1.2f);
        Destroy(m_DeathEffect, 0.4f);

        m_AudioManager.PlaySound(m_SpawnSound);

        m_PauseMenu.enabled = true;
    }
    
    public void TriggerSlowMo()
    {
        Time.timeScale = m_SlowmoTime;
        m_PauseMenu.enabled = false;
    }


    private void DisableEverything()
    {
        m_PlayerHasWon = true;
        m_AudioManager.DisableMusic();

        m_LivesTextP1.enabled = false;
        m_LivesTextP2.enabled = false;
        m_PauseMenu.enabled = false;
        m_Camera.enabled = false;

        DestroyGameObjects();
    }

    private void DestroyGameObjects()
    {
        Destroy(GameObject.FindGameObjectWithTag("PlayerOne"));
        Destroy(GameObject.FindGameObjectWithTag("PlayerTwo"));

        GameObject[] activeProjectilesP1 = GameObject.FindGameObjectsWithTag("ProjectileP1");

        for (int i = 0; i < activeProjectilesP1.Length; i++)
        {
            Destroy(activeProjectilesP1[i]);
        }

        GameObject[] activeProjectilesP2 = GameObject.FindGameObjectsWithTag("ProjectileP2");

        for (int i = 0; i < activeProjectilesP2.Length; i++)
        {
            Destroy(activeProjectilesP2[i]);
        }
    }
}
