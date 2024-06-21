using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Netcode;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            InteractLogicServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc()
    {
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
