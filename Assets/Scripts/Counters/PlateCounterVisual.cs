using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private float plateDistanceOffset = 0.1f;

    private List<GameObject> plateVisuals = new List<GameObject>();

    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounterVisual_SpawnPlateVisual;
        plateCounter.OnPlateRemoved += PlateCounterVisual_RemovePlateVisual;
    }

    private void PlateCounterVisual_RemovePlateVisual(object sender, EventArgs e)
    {
        GameObject plate = plateVisuals[plateVisuals.Count - 1];

        plateVisuals.Remove(plate);
        
        Destroy(plate);
    }

    private void PlateCounterVisual_SpawnPlateVisual(object sender, EventArgs e)
    {
        GameObject plate = Instantiate(plateVisual, counterTopPoint);
        
        plateVisuals.Add(plate);

        plate.transform.position = new Vector3(plate.transform.position.x,
            plate.transform.position.y + plateDistanceOffset * (plateVisuals.Count - 1), plate.transform.position.z);
    }
}
