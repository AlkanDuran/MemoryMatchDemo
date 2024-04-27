using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : LocalSingleton<GameController>
{
    [SerializeField] private PlayerData _player1;
    [SerializeField] private PlayerData _player2;
    [Space]
    [SerializeField] private List<Card> _cardsList = new();
    [SerializeField] private Sprite[] _fourRowCardSprites;
    [SerializeField] private Sprite[] _sixRowCardSprites;
    [SerializeField] private List<Sprite> _gamePuzzles = new();
    [SerializeField] private PlayerOrder _activePlayerOrder = PlayerOrder.Player1;
    [Space] 
    [SerializeField] private float _turnChangeDuration = 0.5f;

    private int _currentRound = 1;
    
    private bool _firstGuess, _secondGuess;
    private int _countCorrectGuesses;
    private int _gameGuesses;

    private int _firstGuessIndex, _secondGuessIndex;
    private string _firstGuessCard, _secondGuessCard;

    public void Init(int rowCount)
    {
        GameManager.OnNewGameStarted += ResetCurrentRound;
        //GameManager.OnRoundFinished += ResetBoard;
        //GameManager.OnGameplayFinished += ResetBoard;
        
        AddGamePuzzles(rowCount);
        ShuffleCards(_gamePuzzles);
        SetGameGuessesCount((rowCount*rowCount)/2);
    }

    private void OnDestroy()
    {
        GameManager.OnNewGameStarted -= ResetCurrentRound;
        //GameManager.OnRoundFinished -= ResetBoard;
        //GameManager.OnGameplayFinished -= ResetBoard;
    }

    private void OnPicked()
    {
        if (!_firstGuess)
        {
            _firstGuess = true;
            _firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            _firstGuessCard = _gamePuzzles[_firstGuessIndex].name;
            _cardsList[_firstGuessIndex].IsSelected = true;
            _cardsList[_firstGuessIndex].SetFrontImage(_gamePuzzles[_firstGuessIndex],false);
        }
        else if (!_secondGuess)
        {
            _secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            _secondGuessCard = _gamePuzzles[_secondGuessIndex].name;

            if (!_cardsList[_secondGuessIndex].IsSelected)
            {
                _secondGuess = true;
                _cardsList[_secondGuessIndex].SetFrontImage(_gamePuzzles[_secondGuessIndex],false);
                StartCoroutine(CheckIfTheCardsMatch());
            }
        }
    }

    private IEnumerator CheckIfTheCardsMatch()
    {
        yield return new WaitForSeconds(_turnChangeDuration*2);
        if (_firstGuessCard == _secondGuessCard)
        {
            yield return new WaitForSeconds(_turnChangeDuration);

            _cardsList[_firstGuessIndex].GetComponent<Button>().interactable = false;
            _cardsList[_secondGuessIndex].GetComponent<Button>().interactable = false;
            
            if (_activePlayerOrder is PlayerOrder.Player1)
            {
                _player1.IncreasePoint(1);
            }
            else
            {
                _player2.IncreasePoint(1);
            }

            CheckRoundIsFinished();
        }
        else
        {
            _cardsList[_firstGuessIndex].SetFrontImage(CardsManager.Instance.GetBackgroundSprite(), true);
            _cardsList[_secondGuessIndex].SetFrontImage(CardsManager.Instance.GetBackgroundSprite(), true);
            
            _cardsList[_firstGuessIndex].IsSelected = false;

            if (_activePlayerOrder is PlayerOrder.Player1)
            {
                _activePlayerOrder = PlayerOrder.Player2;
                _player2.GetCanvasGroup().alpha = 1.0f;
                _player1.GetCanvasGroup().alpha = 0.5f;
            }
            else if (_activePlayerOrder is PlayerOrder.Player2)
            {
                _activePlayerOrder = PlayerOrder.Player1;
                _player2.GetCanvasGroup().alpha = 0.5f;
                _player1.GetCanvasGroup().alpha = 1.0f;
            }
        }
        yield return new WaitForSeconds(_turnChangeDuration);
        
        _firstGuess = _secondGuess = false;
    }

    private void CheckRoundIsFinished()
    {
        _countCorrectGuesses++;
        if (_countCorrectGuesses == _gameGuesses)
        {
            GameManager.Instance.FinishRound();
        }
    }
    public void AddCardToList(Card card)
    {
        _cardsList.Add(card);
        card.GetComponent<Button>().onClick.AddListener(OnPicked);
    }
    
    public void AddGamePuzzles(int rowCount)
    {
        var looper = _cardsList.Count;
        var index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            _gamePuzzles.Add(rowCount is 4 ? _fourRowCardSprites[index] : _sixRowCardSprites[index]);
            index++;
        }
    }

    public void ShuffleCards(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public void ResetBoard()
    {
        foreach (var card in _cardsList)
        {
            // if(card != null)
            PoolManager.Instance.DespawnCard(card.gameObject);
        }
        
        _gamePuzzles.Clear();
        _cardsList.Clear();
        _firstGuess = _secondGuess = false;
        _countCorrectGuesses = 0;
        _activePlayerOrder = PlayerOrder.Player1;
        _player1.GetCanvasGroup().alpha = 1.0f;
        _player2.GetCanvasGroup().alpha = 0.5f;
    } 
    public void SetGameGuessesCount(int value) => _gameGuesses = value;
    public int GetCurrentRound() => _currentRound;
    public void ResetCurrentRound() => _currentRound = 1;
    public void IncreaseCurrentRound() => _currentRound++;
    public PlayerData GetPlayer1() => _player1;
    public PlayerData GetPlayer2() => _player2;
    
}
