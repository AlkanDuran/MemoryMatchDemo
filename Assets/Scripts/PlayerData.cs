using System;
using TMPro;
using UnityEngine;

public enum PlayerOrder { Player1, Player2 }
public class PlayerData : MonoBehaviour
{
    [SerializeField] private PlayerOrder _playerOrder;
    [SerializeField] private int _playerPoint;
    [SerializeField] private int _playerScore;
    [Space]
    [SerializeField] private TextMeshProUGUI _playerPointText;
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private CanvasGroup _canvasGroup;


    private void Start()
    {
        GameManager.OnNewGameStarted += ResetScore;
        GameManager.OnNewGameStarted += ResetPoint;
        GameManager.OnRoundFinished += ResetPoint;
    }

    private void OnDestroy()
    {
        GameManager.OnNewGameStarted -= ResetScore;
        GameManager.OnNewGameStarted -= ResetPoint;
        GameManager.OnRoundFinished -= ResetPoint;
    }

    public void IncreaseScore(int increaseAmount)
    {
        _playerScore += increaseAmount;
        _playerScoreText.text = $"Score: {_playerScore}";
    } 
    public void IncreasePoint(int increaseAmount)
    {
        _playerPoint += increaseAmount;
        _playerPointText.text = $"Point: {_playerPoint}";
    }

    public void ResetPoint()
    {
        _playerPoint = 0;
        _playerPointText.text = $"Point: {_playerPoint}";
    }
    public void ResetScore()
    {
        _playerScore = 0;
        _playerScoreText.text = $"Score: {_playerScore}";
    }
    public int GetPlayerPoint() => _playerPoint;
    public int GetPlayerScore() => _playerScore;
    public PlayerOrder GetPlayerOrder() => _playerOrder;
    public CanvasGroup GetCanvasGroup() => _canvasGroup;
}
