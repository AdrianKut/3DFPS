using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public ParticleSystem VFX;
    public float rotationSpeed = 5f;
    public AudioSource audioSource;

    private void OnCollisionEnter( Collision collision )
    {
        //Kolizja z obiektem gracza
        if( collision.gameObject.tag == "Player" )
        {
            audioSource.Play();

            //Utwórz nowy particle w miejscu gracza i z jego rotacj¹
            ParticleSystem newVfx = Instantiate( VFX, transform.position, transform.rotation );
            //Usun po 2 sekundach nowy 
            Destroy( newVfx.gameObject, 2f );

            //Usun po dwoch sekundach caly gameobject, ale najpierw go ukryj
            Destroy( gameObject, 2f );
            //gameObject.SetActive( false );

            transform.position = Vector3.up * 100;
            ItemCollectManager.Instance.CollectItem();
        }
    }
    private void OnBecameInvisible()
    {
        enabled = false;
        Debug.LogWarning( "Invis" );
    }

    private void OnBecameVisible()
    {
        enabled = true;
        Debug.LogWarning( "Visible" );
    }
}
