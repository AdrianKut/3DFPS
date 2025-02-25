using UnityEngine;

[RequireComponent( typeof( Camera ) )]
public class PlayerCamera : APlayerComponent
{
    [SerializeField][Range( 0.0f, 0.5f )] private float m_MouseSmoothTime = 0.03f;
    [SerializeField] private Camera m_PlayerCamera = null;
    [SerializeField] float m_MouseSensitivity = 3.5f;
    [SerializeField] bool m_CursorLockMode = true;

    private float m_CameraCap = 0f;
    private Vector2 m_CurrentMouseDelta = new Vector2();
    private Vector2 m_CurrentMouseDeltaVelocity = new Vector2();

    public override void Initialize( PlayerController playerController )
    {
        base.Initialize( playerController );
        InitalizeCursorState();
        AssignMissingComponents();
    }

    protected override void AssignMissingComponents()
    {
        if( m_PlayerCamera == null )
        {
            Debug.LogWarning( "Missing Camera but assigned myself!" );
            m_PlayerCamera = GetComponent<Camera>();
        }
    }

    private void InitalizeCursorState()
    {
        if( m_CursorLockMode )
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public override void DoAction()
    {
        MouseInput();
        RotateCamera();
    }

    private void RotateCamera()
    {
        if( m_PlayerCamera == null )
        {
            Debug.LogError( "Missing Player Camera! " );
            return;
        }

        m_CameraCap -= m_CurrentMouseDelta.y * m_MouseSensitivity;
        m_CameraCap = Mathf.Clamp( m_CameraCap, -90.0f, 90.0f );

        m_PlayerCamera.transform.localEulerAngles = new Vector3( m_CameraCap, 0, 0 );
        m_Player.transform.Rotate( Vector3.up, m_CurrentMouseDelta.x * m_MouseSensitivity );
    }

    private void MouseInput()
    {
        Vector2 targetMouseDelta = new Vector2( Input.GetAxis( "Mouse X" ), Input.GetAxis( "Mouse Y" ) );
        m_CurrentMouseDelta = Vector2.SmoothDamp( m_CurrentMouseDelta, targetMouseDelta, ref m_CurrentMouseDeltaVelocity, m_MouseSmoothTime );
    }

    public Camera GetCamera()
    {
        return m_PlayerCamera;
    }
}
