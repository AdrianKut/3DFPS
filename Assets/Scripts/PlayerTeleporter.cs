using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform PlayerTeleport;

    private void OnCollisionEnter( Collision collision )
    {
        //Kolizja z obiektem gracza
        if( collision.gameObject.tag == "Player" )
        {
            // Je�li gracz u�ywa CharacterController, najlepiej wy��czy� go na chwil�
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();

            if( characterController != null )
            {
                // Zapisz obecn� pozycj� i wy��cz CharacterController, aby zaktualizowa� pozycj�
                characterController.enabled = false;

                // Teleportuj gracza
                collision.gameObject.transform.position = PlayerTeleport.position;

                // W��cz CharacterController ponownie
                characterController.enabled = true;
            }
        }
    }
}
