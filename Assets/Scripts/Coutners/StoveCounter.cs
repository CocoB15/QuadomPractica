using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
   [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
   [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

   private enum State
   {
      Idle,
      Frying,
      Fried,
      Burned,
   }
   private float fryingTimer;
   private FryingRecipeSO fryingRecipeSO;
   private State state;
   private float burningTimer;
   private BurningRecipeSO burningRecipeSO;
   private void Start()
   {
      state=State.Idle;
   }
   private void Update()
   {
   
      if (HasKitchenObject())
      {
       switch (state)
       {
         case State.Idle:
            break;
         case State.Frying:
             
            fryingTimer += Time.deltaTime;
            if (fryingTimer>fryingRecipeSO.fryingTimerMax)
            {
               //Fried
               GetKitchenObject().DestroySelf();
               KitchenObject.SpawnKitchenObject(fryingRecipeSO.output,this);
               
               
               burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
               state = State.Fried;
               burningTimer = 0f;
            }
            break;
         case State.Fried:
            burningTimer += Time.deltaTime;
            if ( burningTimer>burningRecipeSO.burningTimerMax)
            {
               //Fried
               GetKitchenObject().DestroySelf();
               KitchenObject.SpawnKitchenObject(burningRecipeSO.output,this);
               
               state = State.Burned;
            }
            break;
         case State.Burned:
            break;
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
               fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSo());
               state = State.Frying;
               fryingTimer = 0f;
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
            state = State.Idle;
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
   private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
      {
         if (burningRecipeSO.input==inputKitchenObjectSO)
         {
            return burningRecipeSO;
         }
      }

      return null;
   }
}
