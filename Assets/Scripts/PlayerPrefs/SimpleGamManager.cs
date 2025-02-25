using TMPro;
using UnityEngine;

public class SimpleGamManager : MonoBehaviour
{
    public TextMeshProUGUI playerMoneyText;
    public float playerMoney;

    private void Start()
    {
        //Wa¿ne, ¿e zero na start
        playerMoney = SaveSystem.Instance.GetInt( SaveSystem.PLAYER_MONEY_KEY );
        Debug.Log( PlayerPrefs.GetInt( SaveSystem.PLAYER_MONEY_KEY ) );
        UpdateText();
    }

    public void Add()
    {
        playerMoney = SaveSystem.Instance.GetInt( SaveSystem.PLAYER_MONEY_KEY );
        playerMoney++;
        SaveSystem.Instance.SetInt( SaveSystem.PLAYER_MONEY_KEY, (int)playerMoney );
        UpdateText();
    }

    public void Substract()
    {
        playerMoney = SaveSystem.Instance.GetInt( SaveSystem.PLAYER_MONEY_KEY );
        playerMoney--;
        SaveSystem.Instance.SetInt( SaveSystem.PLAYER_MONEY_KEY, (int)playerMoney );
        UpdateText();
    }

    public void Reset()
    {
        SaveSystem.Instance.Reset();
        UpdateText();
    }

    private void UpdateText()
    {
        playerMoney = SaveSystem.Instance.GetInt( SaveSystem.PLAYER_MONEY_KEY );
        playerMoneyText.text = "MONETY: " + playerMoney.ToString();
    }
}
