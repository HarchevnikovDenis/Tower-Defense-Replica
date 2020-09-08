using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text money;

    private void Update()
    {
        money.text = "$" + PlayerStats.Money.ToString();    
    }
}
