using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrashed;
    
    new public static void ResetStaticData()
    {
        OnObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().transform.SetParent(null);
            
            StartCoroutine(MoveToTrashAndDestroy(player));
            
            OnObjectTrashed?.Invoke(this,EventArgs.Empty);
        }
    }
    
    private IEnumerator MoveToTrashAndDestroy(Player player)
    {
        KitchenObject playerKitchenObject = player.GetKitchenObject();
        Transform trashCounterTransform = GetKitchenObjectFollowTransform();
        Transform objectTransform = playerKitchenObject.transform;
        Vector3 targetScale = new Vector3(0.8f, 0.8f, 0.8f);

        while (Vector3.Distance(objectTransform.position, trashCounterTransform.position) > 0.01f)
        {
            objectTransform.position = Vector3.Lerp(objectTransform.position, trashCounterTransform.position, Time.deltaTime * 5);
            objectTransform.localScale = Vector3.Lerp(objectTransform.localScale, targetScale, Time.deltaTime * 5);
            yield return null;
        }

        objectTransform.position = trashCounterTransform.position;

        player.GetKitchenObject().DestroySelf();
    }
}
