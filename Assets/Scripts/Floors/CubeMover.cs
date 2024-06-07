using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float initialZPosition = 26f;
    [SerializeField] private float thresholdZPosition = 5f;

    private void Update()
    {
        transform.Translate(Vector3.back * (speed * Time.deltaTime));

        if (transform.localPosition.z <= thresholdZPosition)
        {
            transform.localPosition =
                new Vector3(transform.localPosition.x, transform.localPosition.y, initialZPosition);
        }
    }
}
