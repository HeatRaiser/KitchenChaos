using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeginningTrigger : MonoBehaviour
{
    [SerializeField] private GameObject playerChefHat;
    [SerializeField] private TextMeshProUGUI equipHatText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            if (playerChefHat.activeSelf)
            {
                Destroy(gameObject);

                return;
            }

            equipHatText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            equipHatText.enabled = false;
        }
    }
}
