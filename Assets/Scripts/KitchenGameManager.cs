using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static KitchenGameManager Instance { get; private set; }

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingStartTimer;
    private float gamePlayingStartTimerMax = 65f;
    private bool isGamePaused = false;
    private float gamePlayingPenalty;
    private float gamePlayingPenaltyMax;
    public bool isRecipeDelivered = false;

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
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingStartTimer = gamePlayingStartTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GamePlaying:
                gamePlayingStartTimer -= Time.deltaTime;

                DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
                DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
                
                if (gamePlayingStartTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.GameOver:
                break;
        }
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        if (isRecipeDelivered)
        {
            gamePlayingStartTimer -= 10f;
            isRecipeDelivered = false;
        }
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        if (isRecipeDelivered)
        {
            gamePlayingStartTimer += 10f;
            isRecipeDelivered = false;
        }
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
        return 1 - (gamePlayingStartTimer / gamePlayingStartTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool GetisRecipeDelivered()
    {
        return isRecipeDelivered;
    }

    public void SetisRecipeDelivered(bool isRecipeDelivered)
    {
        this.isRecipeDelivered = isRecipeDelivered;
    }
}