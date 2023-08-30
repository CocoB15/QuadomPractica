using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button tutorialButton;

    private void Awake()
    {
        
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        tutorialButton.onClick.AddListener(() =>
        {
            TutorialUI.Instance.Show();
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

    private void Start()
    {
        TutorialUI.Instance.Hide();
        
    }

   


    private void PlayClick()
    {
        
    }
}
