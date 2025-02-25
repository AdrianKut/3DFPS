using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameOverMenu;
    public GameObject WinnerMenu;

    public static MainMenuManager Instance;
    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            MainMenu.SetActive( true );
            GameController.Instance.SetPauseState( true );
        }
    }

    public void StartGame()
    {
        MainMenu.SetActive( false );
        GameController.Instance.SetPauseState( false );
    }

    public void OpenOptionSubMenu()
    {
        //TODO
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene( 0 );
    }
}
