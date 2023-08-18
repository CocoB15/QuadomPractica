using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public EventHandler OnPlateSpawned;
    public EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax=4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax=4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer>spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmount<platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                
                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
        
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player has no kitchenObject
            if (platesSpawnedAmount>0)
            {
                //at least 1 plate spawned
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
