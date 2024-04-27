using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundOverPage : LocalSingleton<RoundOverPage>
{
    [SerializeField] private TextMeshProUGUI _roundText;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [Space] 
    [SerializeField] private TextMeshProUGUI _player1ScoreText;
    [SerializeField] private TextMeshProUGUI _player2ScoreText;
    [SerializeField] private CanvasGroup _player1CanvasGroup;
    [SerializeField] private CanvasGroup _player2CanvasGroup;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TextMeshProUGUI _nextButtonText;


    public void OnRoundEnd(bool isLastRound)
    {
        _gameOverText.gameObject.SetActive(isLastRound);
        _roundText.text = $"Round: {GameController.Instance.GetCurrentRound()}/{SettingsPage.Instance.GetRoundCount()}";
        _nextButton.onClick.RemoveAllListeners();
        ComparePoint();
        CompareScores();
        
        GameController.Instance.ResetBoard();
        
        if (isLastRound)
        {
            _nextButtonText.text = "NEW GAME";
            _nextButton.onClick.AddListener(NewGameButton_OnClick);
        }
        else
        {
            _nextButtonText.text = "NEXT ROUND";
            _nextButton.onClick.AddListener(NextRoundButton_OnClick);
            GameController.Instance.IncreaseCurrentRound();
        }
    }

    public void CompareScores()
    {
        var player1 = GameController.Instance.GetPlayer1();
        var player2 = GameController.Instance.GetPlayer2();
        
        if (player1.GetPlayerScore() > player2.GetPlayerScore())
        {
            _player1CanvasGroup.alpha = 1.0F;
            _player2CanvasGroup.alpha = 0.5F;

            _winnerText.text = "WINNER IS PLAYER 1";
        }
        else if (player2.GetPlayerScore() > player1.GetPlayerScore())
        {
            _player1CanvasGroup.alpha = 0.5F;
            _player2CanvasGroup.alpha = 1.0F;
            
            _winnerText.text = "WINNER IS PLAYER 2";
        }
        else
        {
            _player1CanvasGroup.alpha = 1.0F;
            _player2CanvasGroup.alpha = 1.0F;
            
            _winnerText.text = "DRAW";
        }
    }
    public void ComparePoint()
    {
        var player1 = GameController.Instance.GetPlayer1();
        var player2 = GameController.Instance.GetPlayer2();

        
        
        if (player1.GetPlayerPoint() > player2.GetPlayerPoint())
        {
            _player1CanvasGroup.alpha = 1.0F;
            _player2CanvasGroup.alpha = 0.5F;
            _player1ScoreText.text = "Score: " + (player1.GetPlayerScore() + 1);
            _player2ScoreText.text = "Score: " + (player2.GetPlayerScore());
            player1.IncreaseScore(1);
            //_winnerText.text = "WINNER IS PLAYER 1";
        }
        else if (player2.GetPlayerPoint() > player1.GetPlayerPoint())
        {
            _player1CanvasGroup.alpha = 0.5F;
            _player2CanvasGroup.alpha = 1.0F;
            _player1ScoreText.text = "Score: " + (player1.GetPlayerScore());
            _player2ScoreText.text = "Score: " + (player2.GetPlayerScore() + 1);
            player2.IncreaseScore(1);
            //_winnerText.text = "WINNER IS PLAYER 2";
        }
        else
        {
            _player1CanvasGroup.alpha = 1.0F;
            _player2CanvasGroup.alpha = 1.0F;
            _player1ScoreText.text = "Score: " + (player1.GetPlayerScore() + 1);
            _player2ScoreText.text = "Score: " + (player2.GetPlayerScore() + 1);
            player1.IncreaseScore(1);
            player2.IncreaseScore(1);
            //_winnerText.text = "DRAW";
        }
    }
    
    public void NewGameButton_OnClick()
    {
        GameManager.Instance.FinishGame();
    }

    public void NextRoundButton_OnClick()
    {
        var sp = SettingsPage.Instance;
        
        GameManager.Instance.SetGamePage();
        CardsManager.Instance.CreateGameBoard(sp.GetGridType(), sp.GetRoundTime()*60, sp.GetRoundCount());
    }
    
    public void ExitButton_OnClick()
    {
        GameManager.Instance.SetHomePage();
    }
}
