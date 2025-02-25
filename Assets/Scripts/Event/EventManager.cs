using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnuEventType
{
   None = 0,
   Blackout = 1,
   SilentSlap = 2,
   BallmanShop = 3,
   LeakingGas = 4,
}

public class EventManager : MonoBehaviour
{
    private float delayTime;

    [Range( 5,10 )] 
    public int minTime;

    [Range( 11, 20 )]
    public int maxTime;

    private List<EnuEventType> customEventsLists 
        = new List<EnuEventType>() { EnuEventType.Blackout, EnuEventType.SilentSlap, EnuEventType.BallmanShop };

    private void Start()
    {
        StartCoroutine( DrawEvent() );
    }

    private IEnumerator DrawEvent()
    {
        int randomValue = Random.Range( minTime, maxTime );
        Debug.LogWarning( "Czekam " + randomValue );
        yield return new WaitForSeconds( randomValue );
    }

    public void EventBlackout()
    {
        //jeden event wylosowany z pozostalych
        //muzyka na start
        //jak ten sie wykonuje to inne nie moga
        //

        

        //var a = Random.Range( -10.0f, 10.0f );
    }
}
