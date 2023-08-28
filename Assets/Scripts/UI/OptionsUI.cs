using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Button closeButton;

    public void Awake()
    {
        Instance = this;
        
    }

    public void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.ChangebackgorundMusicVolume(value);
        });
        musicVolumeSlider.value = .5f;
        soundVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.UpdateSoundVolumes(value);
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
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