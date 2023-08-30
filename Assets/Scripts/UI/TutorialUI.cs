using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
   public static TutorialUI Instance { get; private set; }
   
   
   
   
   private void Awake()
   {
      Instance = this;
      

   }

   private void Start()
   {
      GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

   }
   private void GameInput_OnInteractAction(object sender, EventArgs e)
   {
     Hide();
   }

  
      
   

   public void Show()
   {
      gameObject.SetActive(true);
   }
   public void Hide()
   {
      gameObject.SetActive(false);
   }
}
