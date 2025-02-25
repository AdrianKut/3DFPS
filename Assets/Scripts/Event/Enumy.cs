using System.Collections.Generic;
using UnityEngine;

public enum EEventType
{
    None = 0,
    Blackout = 1,
    Earthquake = 2,
    Fire = 3,
    Flood = 4,
    Hurricane = 5,
    Tornado = 6,
    Tsunami = 7,
    Volcano = 8
}

public class Enumy : MonoBehaviour
{
    void Start()
    {
        //DniTygodnia dniTygodnia = DniTygodnia.Poniedzialek;
        //Debug.LogWarning( dniTygodnia );
        //Debug.LogWarning( (int)dniTygodnia );

        //List<DniTygodnia> dni = new List<DniTygodnia>() { DniTygodnia.Poniedzialek, DniTygodnia.Wtorek, DniTygodnia.Sroda, DniTygodnia.Czwartek, DniTygodnia.Piatek, DniTygodnia.Sobota, DniTygodnia.Niedziela };

        //foreach( DniTygodnia dzien in dni )
        //{
        //    Debug.LogWarning( dzien + " " + (int)dzien );
        //}
    }
}
