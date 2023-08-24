using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.U2D;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
   public event EventHandler OnStateChanged;
   public static KitchenGameManager Instance { get; private set; }
   private enum State
   {
      WaitingToStart,
      CountdownToStart,
      GamePlaying,
      GameOver,
   }

   private State state;
   private float waitingToStartTimer=1f;
   private float countdownToStartTimer=3f;
   private float gamePlayingStartTimer;
   private float gamePlayingStartTimerMax=10f;
   private bool isGamePaused=false;
   private void Awake()
   {
      Instance = this;
      state = State.WaitingToStart;
   }

   private void Start()
   {
      GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
   }

   private void GameInput_OnPauseAction(object sender, EventArgs e)
   {
      TogglePauseGame();
   }

   private void Update()
   {
      switch (state)
      {
         case State.WaitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if (waitingToStartTimer<0f)
            {
               state = State.CountdownToStart;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.CountdownToStart:
            countdownToStartTimer -= Time.deltaTime;
            if (countdownToStartTimer<0f)
            {
               state = State.GamePlaying;
               gamePlayingStartTimer = gamePlayingStartTimerMax;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.GamePlaying:
            gamePlayingStartTimer -= Time.deltaTime;
            if (gamePlayingStartTimer<0f)
            {
               state = State.GameOver;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case State.GameOver:
            break;
      }
      Debug.Log(state);
   }

   public bool IsGamePlaying()
   {
      return state == State.GamePlaying;
   }

   public bool IsCountdownActive()
   {
      return state == State.CountdownToStart;
   }

   public float CountdownToStartTimer()
   {
      return countdownToStartTimer;
   }

   public bool IsGameOver()
   {
      return state == State.GameOver;
   }

   public float GetGamePlayingTimerNormalized()
   {
      return 1-(gamePlayingStartTimer / gamePlayingStartTimerMax);
   }

   private void TogglePauseGame()
   {
      isGamePaused = !isGamePaused;
      if (isGamePaused)
      {
         Time.timeScale = 0f;
      }
      else
      {
         Time.timeScale = 1f;
      }
   }
}
