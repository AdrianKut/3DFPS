using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool IsPaused = false;

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }

        SetPauseState( true );
    }

    public void SetPauseState( bool isPause )
    {
        if( isPause )
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        IsPaused = isPause;
    }
}
