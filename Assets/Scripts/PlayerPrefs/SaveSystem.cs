using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public const string PLAYER_MONEY_KEY = "PlayerMoneyKey";







    private void Awake()
    {
        Instance = this;
    }

    public void SetFloat( string key, float value )
    {
        PlayerPrefs.SetFloat( key, value );
        PlayerPrefs.Save();
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt( key, value );
        PlayerPrefs.Save(); 
    }

    public void SetString( string key, string value )
    {
        PlayerPrefs.SetString( key, value );
        PlayerPrefs.Save();
    }

    public float GetFloat( string key )
    {
        return PlayerPrefs.GetFloat( key, 0 );
    }

    public int GetInt( string key )
    {
        return PlayerPrefs.GetInt( key, 0 );
    }

    public string GetString( string key )
    {
        return PlayerPrefs.GetString( key, "" );
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
