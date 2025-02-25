using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float time;

    private void Update()
    {
        if( timerText != null )
        {
            time = time - 1 * Time.deltaTime;
            timerText.text = string.Format( $"{time:F2}" );

            if( time <= 0 )
            {
                GameController.Instance.SetPauseState( true ); 
                MainMenuManager.Instance.GameOverMenu.SetActive( true );
                Debug.LogError( "GAME OVER!" );
            }
        }
    }
}
