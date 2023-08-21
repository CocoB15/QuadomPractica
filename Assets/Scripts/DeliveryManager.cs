using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
  public static DeliveryManager Instance { get; private set; }
  [SerializeField] private RecipeListSO recipeListSO;
  private List<RecipeSO> waitingRecipeSOList;
  private float spawnRecipeTimer;
  private float spawnRecipeTimerMax=4f;
  private int waitingrecipeMax = 4;

  private void Awake()
  {
    Instance = this;
    
    waitingRecipeSOList = new List<RecipeSO>();
  }

  private void Update()
  {
    if (spawnRecipeTimer<=0f)
    {
      spawnRecipeTimer = spawnRecipeTimerMax;
      if (waitingRecipeSOList.Count < waitingrecipeMax)
      {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
        Debug.Log(waitingRecipeSO.recipeName);
        waitingRecipeSOList.Add(waitingRecipeSO);
      }
    }
  }

  public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
  {
    for (int i = 0; i < waitingRecipeSOList.Count; i++)
    {
      RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
      if (waitingRecipeSO.KitchenObjectSOList.Count==plateKitchenObject.GetKitchenObjectSOList().Count)
      {
        //has same nr. of ingredients
        bool plateContentsMatchesRecipe = true;
        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList)
        {
          //going through all ingredients of recipe
          bool ingredientFound=false;
          foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
          {
            //going through all ingredients on plate
            if (plateKitchenObjectSO==recipeKitchenObjectSO)
            {
              //ingredient matches!
              ingredientFound = true;
              break;
            }
          }

          if (!ingredientFound)
          {
            //ingredient not found on plate
            plateContentsMatchesRecipe = false;
          }
          
        }

        if (plateContentsMatchesRecipe)
        {
          //player delivered correct recipe
          Debug.Log("player delivered correct recipe");
          waitingRecipeSOList.RemoveAt(i);
          return;
        }
      }
    }
    //no matches found
    //player did not deliver correct recipe
    Debug.Log("player did not deliver correct recipe");
  }
}
