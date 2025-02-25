using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform PlayerTeleport;

    private void OnCollisionEnter( Collision collision )
    {
        //Kolizja z obiektem gracza
        if( collision.gameObject.tag == "Player" )
        {
            // Jeœli gracz u¿ywa CharacterController, najlepiej wy³¹czyæ go na chwilê
            CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();

            if( characterController != null )
            {
                // Zapisz obecn¹ pozycjê i wy³¹cz CharacterController, aby zaktualizowaæ pozycjê
                characterController.enabled = false;

                // Teleportuj gracza
                collision.gameObject.transform.position = PlayerTeleport.position;

                // W³¹cz CharacterController ponownie
                characterController.enabled = true;
            }
        }
    }
}
