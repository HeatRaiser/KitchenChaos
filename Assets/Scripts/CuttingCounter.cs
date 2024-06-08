using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;
    
    [SerializeField] private CuttinRecipeSO[] cutKitchenObjectsSO;

    private int cuttingProgress;
    private int maxCuts;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    CuttinRecipeSO cuttinRecipeSo = GetCuttinRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttinRecipeSo.cuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //Debug
            }
            else
            {
                if (cuttingProgress < maxCuts) return;
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttinRecipeSO cuttinRecipeSo = GetCuttinRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttinRecipeSo.cuttingProgressMax
            });

            maxCuts = cuttinRecipeSo.cuttingProgressMax;

            if (cuttingProgress >= cuttinRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttinRecipeSO cuttinRecipeSo = GetCuttinRecipeSOWithInput(inputKitchenObjectSO);
        return cuttinRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
    {
        CuttinRecipeSO cuttinRecipeSo = GetCuttinRecipeSOWithInput(inputKitchenObject);
        
        if (cuttinRecipeSo != null)
        {
            return cuttinRecipeSo.output;
        }
        else
        {
            return null;
        }

    }

    private CuttinRecipeSO GetCuttinRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var cuttinRecipeSo in cutKitchenObjectsSO)
        {
            if (cuttinRecipeSo.input == inputKitchenObjectSO)
            {
                return cuttinRecipeSo;
            }
        }

        return null;
    }
}
