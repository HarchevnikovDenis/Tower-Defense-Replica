using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField] private Text roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while(round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();


            yield return new WaitForSeconds(0.05f);
        }
    }

}
