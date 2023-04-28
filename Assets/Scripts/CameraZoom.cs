using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    /*YOU HAVE TO PUT THIS SCRIPT TO VIRTUAL CAMERA IN EVERY SCENE*/
    public CinemachineVirtualCamera virtualCamera;
    public float speed; // Camera zoom in/out speed

    // Update is called once per frame
    private void LateUpdate()
    {
        if (GameplayManager.instance.currentHealth < 1) // If player is dead
        { virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1.25f, speed); }
        else // If player is not dead
        { virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 2.5f, speed); }
    }
}