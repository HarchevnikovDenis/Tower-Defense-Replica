using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject completeLevelUI;
    public static bool GameIsOver { get; set; }

    private void Start()
    {
        GameIsOver = false;        
    }

    private void Update()
    {
        if (GameIsOver)
            return;

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        if(GameIsOver)
        {
            return;
        }

        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
