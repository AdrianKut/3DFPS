using UnityEngine;
using UnityEngine.SceneManagement;

public enum EPlayerAnimationTrigger
{
    None = 0,
    TableIn = 1,
    TableOut = 2,
}

public class PlayerController : MonoBehaviour
{
    //TWO

    [SerializeField] private PlayerCamera m_PlayerCamera = null;
    [SerializeField] private PlayerMovement m_PlayerMovement = null;
    [SerializeField] private PlayerParticleExtension m_PlayerParticleExtension = null;
    private bool m_ComponentsInitialized = false;

    public static PlayerController Instance;
    [SerializeField] private bool m_CanMove = true;
    [SerializeField] private bool m_CanCameraMove = true;

    private GameObject MainCamera;
    private GameObject AdditionalCamera;

    private void Awake()
    {
        Instance = this;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if( m_PlayerCamera == null )
        {
            Debug.LogWarning( "Missing PlayerCamera but assigned myself!" );
            m_PlayerCamera = GetComponent<PlayerCamera>();
        }

        if( m_PlayerMovement == null )
        {
            Debug.LogWarning( "Missing PlayerMovement but assigned myself!" );
            m_PlayerMovement = GetComponent<PlayerMovement>();
        }

        if( m_PlayerParticleExtension == null )
        {
            Debug.LogWarning( "Missing PlayerParticleExtension but assigned myself!" );
            m_PlayerParticleExtension = GetComponent<PlayerParticleExtension>();
        }

        m_PlayerMovement.Initialize( this );
        m_PlayerCamera.Initialize( this );

        m_ComponentsInitialized = true;
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.R) )
        {
            SceneManager.LoadScene( 0 );
        }

        if( m_ComponentsInitialized )
        {
            if( m_CanCameraMove )
            {
                m_PlayerCamera.DoAction();
            }

            if( m_CanMove )
            {
                m_PlayerMovement.DoAction();
            }
        }
    }

    #region GET/SET
    public void SetMoveState( bool state )
    {
        m_CanMove = state;
    }

    public void SetCameraState( bool state )
    {
        m_CanCameraMove = state;
    }

    public Camera GetPlayerCamera()
    {
        return m_PlayerCamera.GetCamera();
    }

    public PlayerParticleExtension GetPlayerParticleExtension()
    {
        return m_PlayerParticleExtension;
    }
    #endregion
}