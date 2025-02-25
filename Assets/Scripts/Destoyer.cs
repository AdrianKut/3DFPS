using UnityEngine;

public class Destoyer : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_Renderer;
    [SerializeField] private Material m_DestroyedMaterial = null;

    [SerializeField] private float m_DestroyDelay = 0f;
    [SerializeField] private string m_Tag;

    [SerializeField] private ParticleSystem m_DestroyVFX;
    [SerializeField] private Vector3 m_SpawnPos;
    
    private void OnCollisionEnter( Collision collision )
    {
        if ( collision.transform.CompareTag(m_Tag) )
        {
            ParticleSystem particleSystem = Instantiate<ParticleSystem>( m_DestroyVFX,  transform.position ,Quaternion.identity );
            particleSystem.Play();
            m_Renderer.material = m_DestroyedMaterial;  
            Destroy( gameObject, m_DestroyDelay );
        }
    }
}
