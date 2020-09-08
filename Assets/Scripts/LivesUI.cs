using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private Text lives;

    private void Update()
    {
        lives.text = PlayerStats.Lives.ToString() + " LIVES";
    }
}
