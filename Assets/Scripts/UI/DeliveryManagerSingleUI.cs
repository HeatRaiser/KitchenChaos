using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            Debug.Log(child + child.gameObject.activeSelf.ToString());
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            var instantiatedIcon = Instantiate(iconTemplate, iconContainer);
            instantiatedIcon.gameObject.SetActive(true);
            var iconImage = instantiatedIcon.transform.GetComponent<Image>();

            iconImage.sprite = kitchenObjectSO.sprite;
        }
        
    }
}
