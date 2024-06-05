using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraTrigger : MonoBehaviour
{
    [SerializeField] private GameObject oppositeSideTrigger;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private int cameraIndex;

    private void Start()
    {
        oppositeSideTrigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraManager.SwitchCamera(cameraIndex);
            oppositeSideTrigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
