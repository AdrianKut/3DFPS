using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionDelay = 1f;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    public int damage = 50;
    public GameObject explosionEffect;

    private void Start()
    {
        Invoke( "Explode", explosionDelay );
    }

    void Explode()
    {
        GameObject newExplosion = null;
        // Efekt wizualny eksplozji
        if( explosionEffect )
        {
            newExplosion =  Instantiate( explosionEffect, transform.position, Quaternion.identity );
            //Zniszczenie efektu
            Destroy( newExplosion, explosionDelay );
        }

        // Znalezienie obiektów w promieniu eksplozji
        Collider[] colliders = Physics.OverlapSphere( transform.position, explosionRadius );

        foreach( Collider nearbyObject in colliders )
        {
            // Dodanie si³y do obiektów z komponentem Rigidbody
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if( rb != null )
            {
                rb.AddExplosionForce( explosionForce, transform.position, explosionRadius );
            }

            PlayerMovement player = nearbyObject.GetComponent<PlayerMovement>();
            if( player != null )
            {
                player.ApplyExplosionForce( transform.position, explosionForce );
            }
        }

        // Zniszczenie granatu
        Destroy( gameObject );
    }
}
