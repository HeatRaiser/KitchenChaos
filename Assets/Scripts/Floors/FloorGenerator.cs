using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject blackCube, whiteCube, redCube;
    [SerializeField] private GameObject roomOne, roomTwo, roomThree, mainRoom;
    private float distanceBetweenTiles = 1.5f;

    // private List<GameObject> room2Cubes = new List<GameObject>();
    public float moveSpeed = 1.0f;  
    public float startPositionZ = 26.0f;  
    public float endPositionZ = 5.0f;

    // private void Start()
    // {
    //     GameObject[] foundCubes = GameObject.FindGameObjectsWithTag("Tile2");
    //
    //     foreach (GameObject cube in foundCubes)
    //     {
    //         room2Cubes.Add(cube);
    //     }
    // }

    // private void Update()
    // {
    //     MoveRows();
    // }
    
    [ContextMenu("GenerateMainRoom")]
    private void GenerateMainRoom()
    {
        
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3 newPosition = new Vector3(j * distanceBetweenTiles, 0, i * distanceBetweenTiles);

                GameObject cubeToInstantiate = (i % 2 == 0) ? whiteCube : blackCube;

                Instantiate(cubeToInstantiate, newPosition, Quaternion.identity, mainRoom.transform);
            }
            
        }
    }

    [ContextMenu("GenerateCubesRoom1")]
    private void GenerateCubesRoom1()
    {
        // in update move each row by spped * time.delta 
        // room 10 by 20 red and white alternating rows move on update
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3 newPosition = new Vector3(j * distanceBetweenTiles, 0, i * distanceBetweenTiles);

                GameObject cubeToInstantiate = (i % 2 == 0) ? whiteCube : redCube;

                Instantiate(cubeToInstantiate, newPosition, Quaternion.identity, roomOne.transform);
            }
        }
    }
   
    [ContextMenu("GenerateCubesRoom2")]
    private void GenerateCubesRoom2()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3 newPosition = new Vector3(j * distanceBetweenTiles, 0, i * distanceBetweenTiles);

                GameObject cubeToInstantiate = ((i + j) % 2 == 0) ? whiteCube : blackCube;

                Instantiate(cubeToInstantiate, newPosition, Quaternion.identity, roomTwo.transform);
            }
            
        }
    }
   
    [ContextMenu("GenerateCubesRoom3")]
    private void GenerateCubesRoom3()
    {
      
        // in update move each row by spped * time.delta 
        // room 10 by 20 red and black solid color fllor, cross on player row and column 
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3 newPosition = new Vector3(j * distanceBetweenTiles, 0, i * distanceBetweenTiles);

                GameObject cubeToInstantiate = ((i + j) % 2 == 0) ? redCube : blackCube;

                Instantiate(cubeToInstantiate, newPosition, Quaternion.identity, roomThree.transform);
            }
            
        }
    }

    // private void MoveRows()
    // {
    //     foreach (GameObject Tile2 in room2Cubes)
    //     {
    //         Tile2.transform.position += Vector3.back * (moveSpeed * Time.deltaTime);
    //
    //         if (Tile2.transform.position.z <= endPositionZ)
    //         {
    //             //int index = room2Cubes.IndexOf(Tile2);
    //             //float staggeredStartPositionZ = startPositionZ + index;
    //
    //             Tile2.transform.position = new Vector3(Tile2.transform.position.x, Tile2.transform.position.y,
    //                 startPositionZ);
    //         }
    //     }
    // }
}
