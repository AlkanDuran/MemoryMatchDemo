using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GridType { FourXFour, SixXSix }
public class SettingsPage : LocalSingleton<SettingsPage>
{
    [SerializeField] private GridType _gridType;
    [SerializeField] private int _roundTime = 1;
    [SerializeField] private int _roundCount = 1;
    [Space]
    [SerializeField] private TextMeshProUGUI _roundTimeText;
    [SerializeField] private TextMeshProUGUI _roundCountText;
    [SerializeField] private TextMeshProUGUI _fourXFourButtonText;
    [SerializeField] private TextMeshProUGUI _sixXSixButtonText;
    [SerializeField] private Button _fourXFourBtn;
    [SerializeField] private Button _sixXSixBtn;

    public void Init()
    {
        CheckAllSettings();
    }

    private void OnEnable()
    {
        CheckAllSettings();
    }

    private void CheckAllSettings()
    {
        var gridType = PlayerPrefs.GetString("GRID_TYPE", "FourXFour");
        if (gridType == "FourXFour")
        {
            _gridType = GridType.FourXFour;
            _fourXFourBtn.image.enabled = false;
            _sixXSixBtn.image.enabled = true;
            _fourXFourButtonText.color = Color.black;
            _sixXSixButtonText.color = Color.white;
        }
        else if (gridType == "SixXSix")
        {
            _gridType = GridType.SixXSix;
            _fourXFourBtn.image.enabled = true;
            _sixXSixBtn.image.enabled = false;
            _fourXFourButtonText.color = Color.white;
            _sixXSixButtonText.color = Color.black;
        }

        _roundTime = PlayerPrefs.GetInt("ROUND_TIME", 1);
        _roundCount = PlayerPrefs.GetInt("ROUND_COUNT", 1);
        
        _roundTimeText.text = _roundTime.ToString();
        _roundCountText.text = _roundCount.ToString();
    }

    public void ChangeGridTypeToFourXFour()
    {
        _gridType = GridType.FourXFour;
        _fourXFourBtn.image.enabled = false;
        _sixXSixBtn.image.enabled = true;
        PlayerPrefs.SetString("GRID_TYPE", "FourXFour");
        _fourXFourButtonText.color = Color.black;
        _sixXSixButtonText.color = Color.white;
    }
    public void ChangeGridTypeToSixXSix()
    {
        _gridType = GridType.SixXSix;
        _fourXFourBtn.image.enabled = true;
        _sixXSixBtn.image.enabled = false;
        PlayerPrefs.SetString("GRID_TYPE", "SixXSix");
        _fourXFourButtonText.color = Color.white;
        _sixXSixButtonText.color = Color.black;
    }

    public void IncreaseRoundTime(int increaseAmount)
    {
        _roundTime = Mathf.Clamp(_roundTime += increaseAmount, 1, 99);
        _roundTimeText.text = _roundTime.ToString();
        PlayerPrefs.SetInt("ROUND_TIME", _roundTime);
    }

    public void IncreaseRoundCount(int increaseAmount)
    {
        _roundCount = Mathf.Clamp(_roundCount += increaseAmount, 1, 99);
        _roundCountText.text = _roundCount.ToString();
        PlayerPrefs.SetInt("ROUND_COUNT", _roundCount);
    }

    public void StartButton_OnClick()
    {
        GameManager.Instance.StartGame();
        CardsManager.Instance.CreateGameBoard(_gridType, _roundTime*60, _roundCount);
    }

    public void CancelButton_OnClick()
    {
        GameManager.Instance.SetHomePage();
    }

    public void SettingsButton_OnClick()
    {
        GameManager.Instance.SetSettingsPage();
    }

    public GridType GetGridType() => _gridType;
    public int GetRoundTime() => _roundTime;
    public int GetRoundCount() => _roundCount;
}
