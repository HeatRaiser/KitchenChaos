using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    
    private float timeBetweenSpawns;
    private const float maxSpawnTime = 4f;
    
    private int platesAmount = 4;
    private const int maxPlatesAmount = 5;

    private void Start()
    {
        for (int i = 0; i < platesAmount; i++)
        {
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        timeBetweenSpawns += Time.deltaTime;

        if (timeBetweenSpawns > maxSpawnTime)
        {
            timeBetweenSpawns = 0f;

            if (GameManager.Instance.IsGamePlaying() && platesAmount < maxPlatesAmount)
            {
                platesAmount++;
                
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesAmount > 0)
            {
                platesAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
