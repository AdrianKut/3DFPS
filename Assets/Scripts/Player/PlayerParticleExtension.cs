using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleExtension : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_ParticleSystemJump;

    public void PlayJumpVFX()
    {
        m_ParticleSystemJump.Play();
    }
}
