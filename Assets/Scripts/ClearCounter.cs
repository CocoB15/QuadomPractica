using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
   
    
    public override void  Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //no KitchenObject
            if (player.HasKitchenObject())
            {
                //player holding something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player has nothing
            }
        }
        else
        {
            //has KitchenObject
            if (player.HasKitchenObject())
            {
                //player is holding smth
            }
            else
            {
                //give player the kitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
       
    }

   
}
