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
    [SerializeField] private TextMeshProUGUI timeRemainingText;
    private float timeRemaining;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        timeRemainingText.text = Mathf.FloorToInt(timeRemaining).ToString();

        if (timeRemaining <= 0f)
        {
            DeliveryManager.Instance.DisableRecipe();
        }
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
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

        timeRemaining = recipeSO.kitchenObjectSOList.Count * 5 + DeliveryManager.Instance.GetWaitingRecipesList().Count;
        timeRemainingText.text = timeRemaining.ToString();
    }
}
