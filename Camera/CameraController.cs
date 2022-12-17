using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public GameObject freeLookCamera;
    public static CinemachineFreeLook freeLookCameraComponent;
    private void Awake()
    {
        freeLookCameraComponent = freeLookCamera.GetComponent<CinemachineFreeLook>();
        
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            freeLookCameraComponent.m_XAxis.Value = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            freeLookCameraComponent.m_XAxis.Value = -1;
        }
        
    }
}
