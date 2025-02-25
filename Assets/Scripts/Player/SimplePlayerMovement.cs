using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : APlayerComponent
{
    [SerializeField] private CharacterController m_CharacterController = null;

    [Header( "Move" )]
    [SerializeField] float m_MoveSpeed = 6.0f;
    [SerializeField][Range( 0.0f, 0.5f )] float m_MoveSmoothTime = 0.3f;

    [Header( "Jump" )]
    [SerializeField] private float m_JumpForce = 5f;
    [SerializeField] float m_Gravity = -30f;
    [SerializeField] private Transform m_GroundCheck = null;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private LayerMask m_GroundLayerBoost;
    [SerializeField] private float m_GroundRayDistance = 0.2f;
    private bool m_IsGrounded;
    private bool m_IsGroundedBoost;

    private Vector2 m_CurrentDirection = new Vector2();
    private Vector2 m_CurrentDirectionVelocity = new Vector2();
    private Vector3 m_Velocity = new Vector2();


    [Header( "Boost" )]
    private float boostDuration;
    private float startSpeed;
    private float startJumpForce;
    private float startGravity;

    public override void Initialize( PlayerController playerController )
    {
        //NEW
        startSpeed = m_MoveSpeed;
        startJumpForce = m_JumpForce;
        startGravity = m_Gravity;

        base.Initialize( playerController );
        AssignMissingComponents();
    }

    protected override void AssignMissingComponents()
    {
        if( m_CharacterController == null )
        {
            Debug.LogWarning( "Missing CharacterController but assigned myself!" );
            m_CharacterController = GetComponent<CharacterController>();
        }
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

        m_IsGrounded = Physics.Raycast( m_GroundCheck.position, Vector3.down, m_GroundRayDistance, m_GroundLayer );
        m_IsGroundedBoost = Physics.Raycast( m_GroundCheck.position, Vector3.down, m_GroundRayDistance, m_GroundLayerBoost );
        Vector2 targetDirection = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) ).normalized;

        m_CurrentDirection = Vector2.SmoothDamp( m_CurrentDirection, targetDirection, ref m_CurrentDirectionVelocity, m_MoveSmoothTime );
        Vector3 velocity = (transform.forward * m_CurrentDirection.y + transform.right * m_CurrentDirection.x);
        m_Velocity.y += m_Gravity * Time.deltaTime;

        if( Input.GetButtonDown( "Jump" ) && m_IsGrounded )
        {
            m_Velocity.y = m_JumpForce;
        }

        if( Input.GetButtonDown( "Jump" ) && m_IsGroundedBoost )
        {
            m_Velocity.y = m_JumpForce * 2;
        }

        velocity.y = m_Velocity.y;
        m_CharacterController.Move( velocity * Time.deltaTime * m_MoveSpeed );

        if( Input.GetKey( KeyCode.LeftShift ) )
        {
            m_CharacterController.Move( velocity * Time.deltaTime * m_MoveSpeed * 2 );
        }
    }

}
