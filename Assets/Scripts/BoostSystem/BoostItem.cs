using UnityEngine;

public enum EBoostType
{
    None = 0,
    SpeedBoost = 1,
    JumpBoost = 2,
    GravityBoost = 3
}

public class BoostItem : MonoBehaviour
{
    public EBoostType BoostType; 
    public float BoostValue;
    public float BoostDuration;

    private void OnCollisionEnter( Collision collision )
    {
        if( collision.gameObject.tag == "Player" )
        {
            var player = PlayerController.Instance;
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.ApplyBoost( BoostType, BoostValue, BoostDuration );
            Destroy( this.gameObject );
        }
    }
}
