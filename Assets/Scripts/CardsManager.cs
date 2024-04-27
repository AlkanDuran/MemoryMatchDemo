using UnityEngine;
using UnityEngine.UI;

public class CardsManager : LocalSingleton<CardsManager>
{
    [SerializeField] private Transform _cardsParent;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private Sprite _bgImage;

    public void CreateGameBoard(GridType gridType, int roundTime, int roundCount)
    {
        var rowCount = gridType is GridType.FourXFour ? 4 : 6;
        
        UIManager.Instance.SetRoundCountText(roundCount);
        TimerPanel.Instance.SetStartCountDownTime(roundTime);
        TimerPanel.Instance.StartCountdown();
        
        if (gridType is GridType.FourXFour)
        {
            rowCount = 4;
            _cardsParent.localScale = Vector3.one * 0.75f;
            _gridLayoutGroup.constraintCount = 4;
            _gridLayoutGroup.padding.bottom = 50;
        }
        else if (gridType is GridType.SixXSix)
        {
            rowCount = 6;
            _cardsParent.localScale = Vector3.one * 0.5f;
            _gridLayoutGroup.constraintCount = 6;
            _gridLayoutGroup.padding.bottom = 0;
        }
        
        for (var i = 0; i < rowCount*rowCount; i++)
        {
            var card = PoolManager.Instance.SpawnCard().GetComponent<Card>();
            card.name = "" + i;
            card.transform.SetParent(_cardsParent, false);
            card.SetFrontImage(_bgImage,true);
            //card.GetComponent<Button>().image.sprite = _bgImage;
            GameController.Instance.AddCardToList(card);
        }
        
        GameController.Instance.Init(rowCount);
    }

    public Sprite GetBackgroundSprite() => _bgImage;
}
