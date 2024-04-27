using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TimerPanel : LocalSingleton<TimerPanel>
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _startTime;
    private float _currentTime;
    private Tweener _countTween;

    private void OnEnable()
    {
        GameManager.OnRoundFinished += ResetCountDown;
        GameManager.OnGameplayFinished += ResetCountDown;
    }

    private void OnDestroy()
    {
        GameManager.OnRoundFinished -= ResetCountDown;
        GameManager.OnGameplayFinished -= ResetCountDown;
    }

    void UpdateTimerUI()
    {
        var minutes = Mathf.FloorToInt(_currentTime / 60F);
        var seconds = Mathf.FloorToInt(_currentTime - minutes * 60);
        _timerText.text = "Time: " + $"{minutes:00}:{seconds:00}";
    }

    public void StartCountdown()
    {
        _countTween = _fillImage.DOFillAmount(0f, _startTime).SetEase(Ease.Linear).OnUpdate(() =>
            {
                _currentTime = _fillImage.fillAmount * _startTime;
                UpdateTimerUI();
            })
            .OnComplete(() =>
            {
                _currentTime = 0;
                UpdateTimerUI();
                
                GameManager.Instance.FinishRound();
                
            });
    }

    public void SetStartCountDownTime(int startTime)
    {
        _fillImage.fillAmount = 1;
        _startTime = startTime;
        _currentTime = startTime;
        UpdateTimerUI();
    }

    public void ResetCountDown()
    {
        _countTween?.Kill(false);
    }
}