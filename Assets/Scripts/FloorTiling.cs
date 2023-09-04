using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTiling : MonoBehaviour
{
   [SerializeField] private GameObject floorTileBlack;
   [SerializeField] private GameObject floorTileWhite;
   private int gridSize = 15;

   private List<GameObject> tiles = new List<GameObject>();

   /*private void Awake()
   {
      TileFlooring();
   }*/

   [ContextMenu("TileFlooring")]
   private void TileFlooring()
   {
      for (int x = -15; x <= gridSize; x++)
      {
         for (int z = -15; z <= gridSize; z++)
         {
            Vector3 position = new Vector3(x, -.5f , z);
            if ((Math.Abs(x)+Math.Abs(z))%2!=0)
            {
               GameObject tile = Instantiate(floorTileBlack, position, Quaternion.identity,this.transform);
               tiles.Add(tile);
            }
            else
            {
              
                  GameObject tile = Instantiate(floorTileWhite, position, Quaternion.identity, this.transform);
                  tiles.Add(tile);
               
            }

            

         }
      }
   }
   
   
   
}
