using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResolutionChange : MonoBehaviour
{
    public int width;
    public int height;
    public bool FullScreen = false;
    // Start is called before the first frame update
   public void SetWidth(int newWidth)
    {
        width = newWidth;
    }
    public void SetHeight(int newHeight)
    {
        height = newHeight;
    }
    public void SetFullScreen(bool fullscreen)
    {
        FullScreen = fullscreen;
    }
    public void SetRes()
    {
        Screen.SetResolution(width, height, FullScreen);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
