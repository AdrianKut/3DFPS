using UnityEngine;

public class PlayerGrenadeController : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;

    private void Update()
    {
        if( Input.GetKeyDown( KeyCode.G ) )
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate( grenadePrefab, throwPoint.position, throwPoint.rotation );
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce( throwPoint.forward * throwForce, ForceMode.VelocityChange );
    }
}
