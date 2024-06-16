using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHatCounter : BaseCounter
{
    [SerializeField] private GameObject playerChefHat;
    [SerializeField] private GameObject counterChefHat;
    public override void Interact(Player player)
    {
        playerChefHat.SetActive(true);
        counterChefHat.SetActive(false);
    }
}
