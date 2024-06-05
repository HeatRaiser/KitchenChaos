using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private void Start()
    {
        SwitchCamera(0);
    }

    public void SwitchCamera(int cameraIndex)
    {
        for(int i = 0; i < cameras.Length; i++)
        {
            if (i == cameraIndex)
            {
                cameras[i].Priority = 20;
            }

            if (i != cameraIndex)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}
