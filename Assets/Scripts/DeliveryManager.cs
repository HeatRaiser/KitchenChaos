using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class DeliveryManager : NetworkBehaviour
{
    public event EventHandler OnRecipeAdded;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeSOList recipeSOList;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int succesfulDeliveries;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!IsServer) return;
        
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                int waitingRecipeIndex = Random.Range(0, recipeSOList.recipeSOList.Count);
                
                SpawnNewWaitingRecipeClientRpc(waitingRecipeIndex);
            }
        }
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeIndex)
    {
        RecipeSO waitingRecipeSO = recipeSOList.recipeSOList[waitingRecipeIndex];
        waitingRecipeSOList.Add(waitingRecipeSO);
        
        OnRecipeAdded?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    succesfulDeliveries++;
                    
                    waitingRecipeSOList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    
                    return;
                }
            }
        }
        
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public void DisableRecipe()
    {
        // for (int i = 0; i < waitingRecipeSOList.Count; i++)
        // {
        //     waitingRecipeSOList.RemoveAt(i);
        //     
        //     OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        // }
    }

    public List<RecipeSO> GetWaitingRecipesList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccesfulDeliveries()
    {
        return succesfulDeliveries;
    }
}
