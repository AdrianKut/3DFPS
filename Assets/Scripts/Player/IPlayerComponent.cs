using UnityEngine;

public abstract class APlayerComponent : MonoBehaviour
{
    protected PlayerController m_Player;

    public virtual void Initialize( PlayerController playerController )
    {
        m_Player = playerController;
        AssignMissingComponents();
    }

    public virtual void DoAction() { }
    protected abstract void AssignMissingComponents();
}
