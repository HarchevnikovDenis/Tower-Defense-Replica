using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private SceneFader sceneFader;


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
