using DG.Tweening;
using System.Collections;
using UnityEngine;

public static class Killer
{
    public static void KillCoroutine( this MonoBehaviour coroutineSource, ref Coroutine coroutine )
    {
        //DoTween.Instance.IsSilentSlapAcitve = false;
        if( coroutine == null || coroutineSource == null ) return;

        coroutineSource.StopCoroutine( coroutine );
        coroutine = null;
    }
}


public class ObstacleManager : MonoBehaviour
{
    [Header( "Delay To Start Animation" )]
    [SerializeField] private bool m_IsRandomDelay = false;
    [SerializeField] private float m_DelayToStart = 0f;

    private Ease m_Ease = Ease.Linear;

    [Header( "Scale" )]
    [SerializeField] bool m_IsScaling = false;
    [SerializeField] private Vector3 m_ScaleAmplitude;
    [SerializeField] private float m_DurationScaleIn;
    [SerializeField] private float m_DurationScaleOut;
    [SerializeField] private float m_DelayScale;
    private Vector3 m_OriginalScale = Vector3.one;
    private Coroutine m_ScaleCoroutine = null;

    [Header( "Move" )]
    [SerializeField] bool m_IsMoving = false;
    [SerializeField] private Vector3 m_MoveAmplitude;
    [SerializeField] private float m_DurationMoveIn;
    [SerializeField] private float m_DurationMoveOut;
    [SerializeField] private float m_DelayMove;
    private Vector3 m_OriginPos = Vector2.zero;
    private Coroutine m_MoveCoroutine = null;

    [Header( "Rotation" )]
    [SerializeField] bool m_IsRotating = false;
    [SerializeField] Vector3 m_RotateAmplitude;
    [SerializeField] private float m_DurationRotateIn;
    [SerializeField] private float m_DurationRotateOut;
    [SerializeField] private float m_DelayRotate;
    private Vector3 m_OriginRot;
    private Coroutine m_RotateCoroutine = null;

    private Coroutine[] m_Coroutines = null;
    private bool m_IsInitialized = false;

    private void OnEnable()
    {
        StartCoroutine( Start() );
    }

    private IEnumerator Start()
    {
        m_Coroutines = new[] { m_ScaleCoroutine, m_MoveCoroutine, m_RotateCoroutine };

        if( m_IsInitialized == false )
        {
            m_OriginalScale = transform.localScale;
            m_OriginPos = transform.localPosition;
            m_OriginRot = transform.localEulerAngles;

            m_IsInitialized = true;
        }

        yield return new WaitForSeconds( m_DelayToStart );

        if( m_IsScaling )
        {
            this.KillCoroutine( ref m_ScaleCoroutine );
            m_ScaleCoroutine = StartCoroutine( OnScale() );
        }

        if( m_IsMoving )
        {
            this.KillCoroutine( ref m_MoveCoroutine );
            m_MoveCoroutine = StartCoroutine( OnMoveXY() );
        }

        if( m_IsRotating )
        {
            this.KillCoroutine( ref m_RotateCoroutine );
            m_RotateCoroutine = StartCoroutine( OnRotate() );
        }
    }

    private void OnDisable()
    {
        KillCoroutines();
    }

    private void OnDestroy()
    {
        KillCoroutines();
    }

    private void KillCoroutines()
    {
        for( int coroutineIndex = 0; coroutineIndex < m_Coroutines.Length; coroutineIndex++ )
        {
            this.KillCoroutine( ref m_Coroutines[coroutineIndex] );
        }
    }

    private IEnumerator OnScale()
    {
        if( m_IsScaling == false )
        {
            yield break;
        }

        yield return transform.DOScale( m_ScaleAmplitude, m_DurationScaleIn )
            .SetEase( m_Ease )
            .WaitForCompletion();

        yield return new WaitForSeconds( m_DelayScale );

        yield return transform.DOScale( m_OriginalScale, m_DurationScaleOut )
           .SetEase( m_Ease )
           .WaitForCompletion();

        this.KillCoroutine( ref m_ScaleCoroutine );
        m_ScaleCoroutine = StartCoroutine( OnScale() );
    }

    private IEnumerator OnMoveXY()
    {
        Vector3 targetPosition = m_OriginPos + m_MoveAmplitude;

        yield return transform.DOLocalMove( targetPosition, m_DurationMoveIn )
            .SetEase( m_Ease )
            .WaitForCompletion();

        yield return new WaitForSeconds( m_DelayMove );

        yield return transform.DOLocalMove( m_OriginPos, m_DurationMoveOut )
            .SetEase( m_Ease )
            .WaitForCompletion();

        this.KillCoroutine( ref m_MoveCoroutine );
        m_MoveCoroutine = StartCoroutine( OnMoveXY() );
    }

    private IEnumerator OnRotate()
    {
        Vector3 targetRotIn = m_OriginRot + m_RotateAmplitude;
        Vector3 targetRotOut = m_OriginRot - m_RotateAmplitude;
        float durationA = m_DurationRotateIn;
        float durationB = m_DurationRotateOut;

        yield return transform.DORotate( targetRotIn, durationA )
            .SetEase( m_Ease )
            .WaitForCompletion();

        yield return new WaitForSeconds( m_DelayRotate );

        yield return transform.DORotate( targetRotOut, durationB )
            .SetEase( m_Ease )
            .WaitForCompletion();

        this.KillCoroutine( ref m_RotateCoroutine );
        m_RotateCoroutine = StartCoroutine( OnRotate() );
    }

    public void ChangeScaleState( bool state )
    {
        m_IsScaling = state;
        if( m_IsScaling )
        {
            this.KillCoroutine( ref m_ScaleCoroutine );
            m_ScaleCoroutine = StartCoroutine( OnScale() );
        }
    }
}
