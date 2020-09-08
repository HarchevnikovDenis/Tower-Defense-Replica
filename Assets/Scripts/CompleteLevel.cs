using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    [SerializeField] private string nextLevel;
    [SerializeField] private int levelToUnlock;

    public void Menu()
    {
        sceneFader.FadeTo("MenuScene");
    }

    public void Complete()
    {
        OpenNextLevel();
        sceneFader.FadeTo(nextLevel);
    }

    private void OpenNextLevel()
    {
        int currentOpenedLevel = PlayerPrefs.GetInt("levelReached");
        if(currentOpenedLevel < levelToUnlock)
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }
}
