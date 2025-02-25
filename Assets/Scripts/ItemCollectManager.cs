using TMPro;
using UnityEngine;

public class ItemCollectManager : MonoBehaviour
{
    public static ItemCollectManager Instance;
    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
    }

    public CollectableItem[] collectableItems;
    public TextMeshProUGUI itemsText;
    public int collected = 0;

    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        itemsText.text = $"Zbierz prezenty: {collected}/{collectableItems.Length}";
    }

    public void CollectItem()
    {
        collected++;
        UpdateText();

        if( collected == collectableItems.Length )
        {
            GameController.Instance.SetPauseState( true );
            MainMenuManager.Instance.WinnerMenu.gameObject.SetActive( true );
        }
    }
}
