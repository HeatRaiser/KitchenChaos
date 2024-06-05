using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeHighlight : MonoBehaviour
{
    private Renderer renderer;
    private Material defaultMaterial;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material highlightMaterial;
    private List<GameObject> highlightedCubes = new List<GameObject>();

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        defaultMaterial = renderer.material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Highlight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestoreMaterial();
        }
    }

    private void Highlight()
    {
        //renderer.material = highlightMaterial;

        GameObject[] cubes = GameObject.FindGameObjectsWithTag("TileRoom3");

        foreach (GameObject cube in cubes)
        {
            if (Mathf.Approximately(cube.transform.localPosition.x, transform.localPosition.x) ||
                Mathf.Approximately(cube.transform.localPosition.z, transform.localPosition.z))
            {
                cube.GetComponent<Renderer>().material = highlightMaterial;
                highlightedCubes.Add(cube);
            }
        }
    }

    private void RestoreMaterial()
    {
        //renderer.material = defaultMaterial;

        foreach (GameObject cube in highlightedCubes)
        {
            if (cube.gameObject.name.Contains("Red"))
            {
                cube.GetComponent<Renderer>().material = redMaterial;
            }
            else
            {
                cube.GetComponent<Renderer>().material = blackMaterial;
            }
            
        }

        highlightedCubes.Clear();
    }
}
