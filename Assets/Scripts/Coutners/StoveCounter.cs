using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
   [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

   private float fryingTimer;
   private FryingRecipeSO fryingrecipeSO;
   private void Update()
   {
      if (HasKitchenObject())
      {
         FryingRecipeSO fryingrecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
         fryingTimer += Time.deltaTime;
         if (fryingTimer>fryingrecipeSO.fryingTimerMax)
         {
            //Fried
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingrecipeSO.output,this);
         }
      }
   }
   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         //no KitchenObject
         if (player.HasKitchenObject())
         {
            if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
            {
               //player holding something that can be cut
               player.GetKitchenObject().SetKitchenObjectParent(this);
               fryingrecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
            }
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
   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
      return fryingRecipeSO != null;
   }

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
   {
      FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
      if (fryingRecipeSO != null)
      {
         return fryingRecipeSO.output;
      }
      else
      {
         return null;
      }
        

        
   }

   private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
      {
         if (fryingRecipeSO.input==inputKitchenObjectSO)
         {
            return fryingRecipeSO;
         }
      }

      return null;
   }
}
