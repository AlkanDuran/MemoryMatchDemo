using System;
using UnityEngine;
using TMPro;

public class UIManager : LocalSingleton<UIManager>
{
    [SerializeField] private GameObject _homePage;
    [SerializeField] private GameObject _settingsPage;
    [SerializeField] private GameObject _gamePage;
    [SerializeField] private GameObject _roundOverPage;
    [Space]
    [SerializeField] private GameObject _cardsParent;
    [SerializeField] private TextMeshProUGUI _roundText;

    public void Init()
    {
        GameManager.OnHomePageActive += Show_HomePage;
        GameManager.OnHomePageActive += Hide_SettingsPage;
        GameManager.OnHomePageActive += Hide_GamePage;
        GameManager.OnHomePageActive += Hide_RoundOverPage;
        
        GameManager.OnSettingsPageActive += Hide_HomePage;
        GameManager.OnSettingsPageActive += Show_SettingsPage;
        GameManager.OnSettingsPageActive += Hide_GamePage;
        GameManager.OnSettingsPageActive += Hide_RoundOverPage;
        
        GameManager.OnGamePageActive += Hide_HomePage;
        GameManager.OnGamePageActive += Hide_SettingsPage;
        GameManager.OnGamePageActive += Show_GamePage;
        GameManager.OnGamePageActive += Hide_RoundOverPage;
        
        GameManager.OnRoundOverPageActive += Hide_HomePage;
        GameManager.OnRoundOverPageActive += Hide_SettingsPage;
        GameManager.OnRoundOverPageActive += Hide_GamePage;
        GameManager.OnRoundOverPageActive += Show_RoundOverPage;
        
        GameManager.Instance.SetHomePage();
    }

    public void OnDestroy()
    {
        GameManager.OnHomePageActive -= Show_HomePage;
        GameManager.OnHomePageActive -= Hide_SettingsPage;
        GameManager.OnHomePageActive -= Hide_GamePage;
        GameManager.OnHomePageActive -= Hide_RoundOverPage;
        
        GameManager.OnSettingsPageActive -= Hide_HomePage;
        GameManager.OnSettingsPageActive -= Show_SettingsPage;
        GameManager.OnSettingsPageActive -= Hide_GamePage;
        GameManager.OnSettingsPageActive -= Hide_RoundOverPage;
        
        GameManager.OnGamePageActive -= Hide_HomePage;
        GameManager.OnGamePageActive -= Hide_SettingsPage;
        GameManager.OnGamePageActive -= Show_GamePage;
        GameManager.OnGamePageActive -= Hide_RoundOverPage;
        
        GameManager.OnRoundOverPageActive -= Hide_HomePage;
        GameManager.OnRoundOverPageActive -= Hide_SettingsPage;
        GameManager.OnRoundOverPageActive -= Hide_GamePage;
        GameManager.OnRoundOverPageActive -= Show_RoundOverPage;
    }

    public void Show_SettingsPage() => _settingsPage.SetActive(true);
    public void Hide_SettingsPage() => _settingsPage.SetActive(false);
    public void Show_HomePage() => _homePage.SetActive(true);
    public void Hide_HomePage() => _homePage.SetActive(false);
    public void Show_GamePage() => _gamePage.SetActive(true);
    public void Hide_GamePage() => _gamePage.SetActive(false);
    public void Show_RoundOverPage() => _roundOverPage.SetActive(true);
    public void Hide_RoundOverPage() => _roundOverPage.SetActive(false);
    public void SetRoundCountText(int amount) => _roundText.text = $"Round : {GameController.Instance.GetCurrentRound()}/{amount}";
}
