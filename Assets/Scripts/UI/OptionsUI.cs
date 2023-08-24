using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
   [SerializeField] private Slider musicVolumeSlider;
   [SerializeField] private Slider soundVolumeSlider;

   public void Start()
   {
       
       musicVolumeSlider.onValueChanged.AddListener((value) =>
       {
           SoundManager.Instance.ChangebackgorundMusicVolume(value);
           
       });
       musicVolumeSlider.value = .5f;
       
      
      
   }
}
