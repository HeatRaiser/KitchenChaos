using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeSOList recipeSOList;
    private List<RecipeSO> waitingRecipeSOList;
    private float maximumWaitingOrders = 5f;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimerMax <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            RecipeSO waitingRecipeSO = recipeSOList.recipeSOList[Random.Range(0, recipeSOList.recipeSOList.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
        }
    }
}
