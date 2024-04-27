using System;
using UnityEngine;

public class GameManager : LocalSingleton<GameManager>
{
    public static event Action OnNewGameStarted;
    public static event Action OnGameplayFinished;
    public static event Action OnRoundFinished;
    public static event Action OnHomePageActive, OnSettingsPageActive, OnGamePageActive, OnRoundOverPageActive;

    protected override void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        OnNewGameStarted += SetGamePage;
        OnRoundFinished += RoundEndControl;
        OnGameplayFinished += SetSettingsPage;
        
        Init();
    }
    private void OnDestroy()
    {
        OnNewGameStarted -= SetGamePage;
        OnRoundFinished -= RoundEndControl;
        OnGameplayFinished -= SetSettingsPage;
    }
    private void Init()
    {
        UIManager.Instance.Init();
        SettingsPage.Instance.Init();
    }
    public void RoundEndControl()
    {
        SetRoundOverPage();
            
        if (GameController.Instance.GetCurrentRound() != SettingsPage.Instance.GetRoundCount()) // not last round
        {
            RoundOverPage.Instance.OnRoundEnd(false);
        }
        else // last round
        {
            RoundOverPage.Instance.OnRoundEnd(true);
        }
    }

    public void StartGame() => OnNewGameStarted?.Invoke();
    public void FinishGame() => OnGameplayFinished?.Invoke();
    public void FinishRound() => OnRoundFinished?.Invoke();
    public void SetHomePage() => OnHomePageActive?.Invoke();
    public void SetSettingsPage() => OnSettingsPageActive?.Invoke();
    public void SetGamePage() => OnGamePageActive?.Invoke();
    private void SetRoundOverPage() => OnRoundOverPageActive?.Invoke();
}
