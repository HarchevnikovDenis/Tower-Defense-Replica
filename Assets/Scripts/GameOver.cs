using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject shopUI;
    public SceneFader sceneFader;


    private void OnEnable()
    {
        if (GameManager.GameIsOver)
            shopUI.SetActive(false);
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo("MenuScene");
    }
}
