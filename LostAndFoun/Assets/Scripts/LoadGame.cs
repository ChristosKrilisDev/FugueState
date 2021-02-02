using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void QuitApp()
    {
        Application.Quit();
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }

}
