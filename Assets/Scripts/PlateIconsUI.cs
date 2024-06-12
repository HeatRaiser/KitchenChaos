using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplatePrefab;
    [SerializeField] private Image ingredientIcon;

    private GameObject[] spawnedTemplateArray;
    private List<GameObject> spawnedTemplateList = new List<GameObject>();

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnPlateIngredientAdded;
    }

    private void OnPlateIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        spawnedTemplateArray = new GameObject[spawnedTemplateList.Count];
    
        for (int i = 0; i < spawnedTemplateArray.Length; i++)
        {
            spawnedTemplateArray[i] = spawnedTemplateList.ElementAt(i);
        }
        
        for (int i = 0; i < spawnedTemplateArray.Length; i++)
        {
            Destroy(spawnedTemplateArray[i]);
        }
        
        spawnedTemplateList.Clear();

        foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList())
        {
            
            ingredientIcon.sprite = kitchenObjectSo.sprite;

            var instantiated = Instantiate(iconTemplatePrefab, transform);
            
            spawnedTemplateList.Add(instantiated.gameObject);
        }
    }
}
