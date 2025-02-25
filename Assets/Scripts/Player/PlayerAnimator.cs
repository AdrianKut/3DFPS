using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimator : APlayerComponent
{
    public UnityEvent<EPlayerAnimationTrigger> OnAnimFinished = new UnityEvent<EPlayerAnimationTrigger>();

    [SerializeField] private Animator m_PlayerAnimator = null;
    public override void Initialize( PlayerController playerController )
    {
        base.Initialize( playerController );
        AssignMissingComponents();
    }

    protected override void AssignMissingComponents()
    {
        if( m_PlayerAnimator == null )
        {
            Debug.LogWarning( "Missing Animator but assigned myself!" );
            m_PlayerAnimator = GetComponent<Animator>();
        }
    }

    public void SetAnimatorTrigger( EPlayerAnimationTrigger triggerID )
    {
        m_PlayerAnimator.enabled = true;
        m_PlayerAnimator.SetTrigger( triggerID.ToString() );
    }

    public void StopAnimation( EPlayerAnimationTrigger triggerID )
    {
        m_PlayerAnimator.enabled = false;
        OnAnimFinished.Invoke( triggerID );
    }
}
