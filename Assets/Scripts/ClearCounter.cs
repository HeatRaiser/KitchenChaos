using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform counterTopPoint;
    
    public void Interact()
    {
        var spawnedObject = Instantiate(objectToSpawn, counterTopPoint);
        spawnedObject.transform.localPosition = Vector3.zero;
    }
}
