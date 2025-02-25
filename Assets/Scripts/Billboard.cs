using UnityEngine;
public class Billboard : MonoBehaviour
{

    private Camera m_Camera;
    private void Start()
    {
        //m_Camera = PlayerController.Instance.GetPlayerCamera();
       // this.m_Camera = Camera.main;
    }

    private void LateUpdate()
    {
        //transform.LookAt( transform.position + m_Camera.transform.rotation * Vector3.forward ); 
        //haha ukrylem cos
    }

}