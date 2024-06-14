using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeAdded += OnWaitingRecipeListChange;
        DeliveryManager.Instance.OnRecipeCompleted += OnWaitingRecipeListChange;
    }

    private void OnWaitingRecipeListChange(object sender, EventArgs e)
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        foreach (Transform child in container)
        {
            if (child != recipeTemplate)
            {
                Destroy(child.gameObject);
            }
            
        }
        
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipesList())
        {
            var instantiatedTemplate = Instantiate(recipeTemplate, container);
            instantiatedTemplate.gameObject.SetActive(true);
            
            var deliveryManagerSingleUI = instantiatedTemplate.GetComponent<DeliveryManagerSingleUI>();
            deliveryManagerSingleUI.SetRecipeSO(recipeSO);
        }
    }
}
