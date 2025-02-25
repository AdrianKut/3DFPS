using System.Collections;
using UnityEngine;

public class PlayerMovement : APlayerComponent
{
    [SerializeField] private CharacterController m_CharacterController = null;

    [Header( "Move" )]
    [SerializeField] private float m_MoveSpeed = 6.0f;
    [SerializeField][Range( 0.0f, 0.5f )] private float m_MoveSmoothTime = 0.3f;

    [Header( "Jump" )]
    [SerializeField] private float m_JumpForce = 5f;
    [SerializeField] private float m_Gravity = -30f;
    [SerializeField] private Transform m_GroundCheck = null;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private LayerMask m_GroundLayerBoost;
    [SerializeField] private float m_GroundRayDistance = 0.2f;

    private bool m_IsGrounded;
    private bool m_IsGroundedBoost;

    private Vector2 m_CurrentDirection = new Vector2();
    private Vector2 m_CurrentDirectionVelocity = new Vector2();
    private Vector3 m_Velocity = new Vector2();

    [Header( "Wall Mechanics" )]
    [SerializeField] private Transform m_WallCheck = null;
    [SerializeField] private LayerMask m_WallLayer;
    [SerializeField] private float m_WallRayDistance = 0.5f;
    [SerializeField] private float m_WallJumpForce = 7f;
    [SerializeField] private float m_WallJumpPush = 5f;
    [SerializeField] private float m_WallSlideSpeed = 2f;

    private bool m_IsTouchingWall;
    private bool m_IsWallSliding;

    [Header( "Boost" )]
    private float boostDuration;
    private float startSpeed;
    private float startJumpForce;
    private float startGravity;

    public override void Initialize( PlayerController playerController )
    {
        startSpeed = m_MoveSpeed;
        startJumpForce = m_JumpForce;
        startGravity = m_Gravity;
    }

    public override void DoAction()
    {
        Move();
    }

    private void Move()
    {
        if( m_CharacterController == null )
        {
            Debug.LogError( "Missing character controller" );
            return;
        }

        // Sprawdzenie czy postaæ jest na ziemi
        m_IsGrounded = Physics.Raycast( m_GroundCheck.position, Vector3.down, m_GroundRayDistance, m_GroundLayer );
        m_IsGroundedBoost = Physics.Raycast( m_GroundCheck.position, Vector3.down, m_GroundRayDistance, m_GroundLayerBoost );

        // Sprawdzenie czy dotykamy œciany
        m_IsTouchingWall = Physics.Raycast( m_WallCheck.position, transform.right, m_WallRayDistance, m_WallLayer ) ||
                           Physics.Raycast( m_WallCheck.position, -transform.right, m_WallRayDistance, m_WallLayer );

        // Pobranie wejœcia gracza
        Vector2 targetDirection = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) ).normalized;
        m_CurrentDirection = Vector2.SmoothDamp( m_CurrentDirection, targetDirection, ref m_CurrentDirectionVelocity, m_MoveSmoothTime );

        // Przemieszczanie siê
        Vector3 velocity = (transform.forward * m_CurrentDirection.y + transform.right * m_CurrentDirection.x);
        m_Velocity.y += m_Gravity * Time.deltaTime;

        // Skok z ziemi
        if( Input.GetButtonDown( "Jump" ) && m_IsGrounded )
        {
            m_Velocity.y = m_JumpForce;
        }

        // Wzmocniony skok
        if( Input.GetButtonDown( "Jump" ) && m_IsGroundedBoost )
        {
            m_Velocity.y = m_JumpForce * 2;
        }

        // Wall Slide – spowolnienie opadania przy œcianie
        if( m_IsTouchingWall && !m_IsGrounded && m_Velocity.y < 0 )
        {
            m_IsWallSliding = true;
            m_Velocity.y = -m_WallSlideSpeed;
        }
        else
        {
            m_IsWallSliding = false;
        }

        // Wall Jump – odbicie od œciany
        if( Input.GetButtonDown( "Jump" ) && m_IsWallSliding )
        {
            m_Velocity.y = m_WallJumpForce;
            float pushDirection = transform.right.x * -Mathf.Sign( m_CurrentDirection.x ); // Odbija w przeciwn¹ stronê
            m_Velocity.x = m_WallJumpPush * pushDirection;

            m_IsWallSliding = false; // Wy³¹czenie wall slide po skoku
        }

        // Wykonanie ruchu
        velocity.y = m_Velocity.y;
        m_CharacterController.Move( velocity * Time.deltaTime * m_MoveSpeed );

        // Sprint
        if( Input.GetKey( KeyCode.LeftShift ) )
        {
            m_CharacterController.Move( velocity * Time.deltaTime * m_MoveSpeed * 2 );
        }
    }

    private void OnDrawGizmos()
    {
        // Rysowanie checków
        Gizmos.color = Color.red;
        Gizmos.DrawLine( m_GroundCheck.position, m_GroundCheck.position + Vector3.down * m_GroundRayDistance );

        Gizmos.color = Color.blue;
        Gizmos.DrawLine( m_WallCheck.position, m_WallCheck.position + transform.right * m_WallRayDistance );
        Gizmos.DrawLine( m_WallCheck.position, m_WallCheck.position - transform.right * m_WallRayDistance );
    }

    public void ApplyExplosionForce( Vector3 explosionPosition, float force )
    {
        Vector3 explosionDir = transform.position - explosionPosition;
        explosionDir.y = Mathf.Abs( explosionDir.y );
        explosionDir.Normalize();

        m_Velocity += explosionDir * (force / 10f);
    }

    public void ApplyBoost( EBoostType boostType, float force, float duration )
    {
        boostDuration = duration;

        switch( boostType )
        {
            case EBoostType.None:
                break;
            case EBoostType.SpeedBoost:
                m_MoveSpeed = force;
                StartCoroutine( StopSpeedBoost() );
                break;
            case EBoostType.JumpBoost:
                m_JumpForce = force;
                StartCoroutine( StopJumpBoost() );
                break;
            case EBoostType.GravityBoost:
                m_Gravity = force;
                StartCoroutine( StopGravityBoost() );
                break;
        }
    }

    private IEnumerator StopSpeedBoost()
    {
        yield return new WaitForSeconds( boostDuration );
        m_MoveSpeed = startSpeed;
    }

    private IEnumerator StopJumpBoost()
    {
        yield return new WaitForSeconds( boostDuration );
        m_JumpForce = startJumpForce;
    }

    private IEnumerator StopGravityBoost()
    {
        yield return new WaitForSeconds( boostDuration );
        m_Gravity = startGravity;
    }

    protected override void AssignMissingComponents()
    {
        //throw new System.NotImplementedException();
    }
}
