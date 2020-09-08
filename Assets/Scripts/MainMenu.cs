using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void Play(string sceneName)
    {
        sceneFader.FadeTo(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
