using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altInteractButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    public void Awake()
    {
        Instance = this;
        moveUpButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Move_Up);});
    }

    public void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.ChangebackgorundMusicVolume(value);
        });
        musicVolumeSlider.value = .5f;
        soundVolumeSlider.onValueChanged.AddListener((value) => { SoundManager.Instance.UpdateSoundVolumes(value); });
        soundVolumeSlider.value = .5f;
        closeButton.onClick.AddListener(() => { Hide(); });
        UpdateVisual();
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide();
        HidePressToRebindKey();
    }

    private void UpdateVisual()
    {
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
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

    public void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    public void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindingBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.Rebinding(binding,()=>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}