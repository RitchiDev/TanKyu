using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene(3);
    }
}
