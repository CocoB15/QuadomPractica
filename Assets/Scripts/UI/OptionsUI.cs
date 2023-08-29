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
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadAltInteractButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadAltInteractText;
    [SerializeField] private Transform pressToRebindKeyTransform;
    private Action onCloseButtonAction;

    public void Awake()
    {
        Instance = this;
        
        moveUpButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Move_Right);});
        interactButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Interact);});
        altInteractButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.InteractAlternate);});
        pauseButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Pause);});
        gamepadInteractButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Gamepad_Interact);});
        gamepadAltInteractButton.onClick.AddListener(() =>{RebindingBinding(GameInput.Binding.Gamepad_InteractAlternate);});
    }

    public void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.ChangebackgorundMusicVolume(value);
        });
        //save changes to music slider
        musicVolumeSlider.value = PlayerPrefs.GetFloat(SoundManager.PLAYER_PREFS_MUSIC_VOLUME, 1f);
        //save changes to SFX slider
        soundVolumeSlider.value = PlayerPrefs.GetFloat(SoundManager.PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        
        soundVolumeSlider.onValueChanged.AddListener((value) => { SoundManager.Instance.UpdateSoundVolumes(value); });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });
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
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        
        musicVolumeSlider.Select();
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