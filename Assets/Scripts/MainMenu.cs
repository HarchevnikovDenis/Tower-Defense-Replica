using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public void Play(string sceneName)
    {
        sceneFader.FadeTo(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
