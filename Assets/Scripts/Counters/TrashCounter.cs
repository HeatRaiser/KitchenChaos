using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().transform.SetParent(null);
            
            StartCoroutine(MoveToTrashAndDestroy(player));
        }
    }
    
    private IEnumerator MoveToTrashAndDestroy(Player player)
    {
        KitchenObject playerKitchenObject = player.GetKitchenObject();
        Transform trashCounterTransform = GetKitchenObjectFollowTransform();
        Transform objectTransform = playerKitchenObject.transform;

        while (Vector3.Distance(objectTransform.position, trashCounterTransform.position) > 0.01f)
        {
            objectTransform.position = Vector3.Lerp(objectTransform.position, trashCounterTransform.position, Time.deltaTime * 5);
            yield return null;
        }

        objectTransform.position = trashCounterTransform.position;

        player.GetKitchenObject().DestroySelf();
    }
}
