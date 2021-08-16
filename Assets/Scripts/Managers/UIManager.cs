using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [Header("Main Screen Values")]
    [SerializeField] MainScreen mainScreen;

    [Header("Start Screen Values")]
    [SerializeField] StartScreen startScreen;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        InitMainScreenParameters();
        mainScreen.sourceImage.SetActive(false);
        startScreen.sourceImage.SetActive(true);
        mainScreen.hookButton.interactable = false;
        UpdateGainedMoneyText();
    }

    private void InitMainScreenParameters()
    {
        UpdateWalletText();
        mainScreen.idleButtons.UpdateLengthButton(GameManager.Instance.length, GameManager.Instance.lengthCost);
        mainScreen.idleButtons.UpdateStrengthButton(GameManager.Instance.strength, GameManager.Instance.strengthCost);
        mainScreen.idleButtons.UpdateOfflineEarningsButton(GameManager.Instance.offlineEarnings, GameManager.Instance.offlineEarningsCost);
        UpdateIdleButtons();
    }

    private void UpdateIdleButtons()
    {
        int currentMoney = GameManager.Instance.wallet;
        if (currentMoney >= GameManager.Instance.lengthCost)
        {
            mainScreen.idleButtons.lengthButton.interactable = true;
        }
        else
        {
            mainScreen.idleButtons.lengthButton.interactable = false;
        }
        if (currentMoney > GameManager.Instance.strengthCost)
        {
            mainScreen.idleButtons.strengthButton.interactable = true;
        }
        else
        {
            mainScreen.idleButtons.strengthButton.interactable = false;
        }
        if (currentMoney > GameManager.Instance.offlineEarningsCost)
        {
            mainScreen.idleButtons.offlineEarningsButton.interactable = true;

        }
        else
        {
            mainScreen.idleButtons.offlineEarningsButton.interactable = false;
        }
    }

    private void UpdateWalletText()
    {
        mainScreen.walletText.text = "$" + GameManager.Instance.wallet.ToString();
    }

    private void UpdateGainedMoneyText()
    {
        startScreen.gainedMoneyText.text = "$" + (GameManager.Instance.totalGain + " gained while waiting");
    }

    public void OnFishingStarted()
    {
        mainScreen.sourceImage.gameObject.SetActive(false);
    }

    public void OnFishingStopped(List<FishController> hookedFishes)
    {
        mainScreen.sourceImage.gameObject.SetActive(true);
        for (int i = 0; i < hookedFishes.Count; i++)
        {
            GameManager.Instance.wallet += hookedFishes[i].GetComponent<FishController>().fish.price;
        }
        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnLengthButtonClicked()
    {
        GameManager.Instance.wallet -= GameManager.Instance.lengthCost;
        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        PlayerPrefs.SetInt("Length", PlayerPrefs.GetInt("Length", 30) + 10);
        GameManager.Instance.length = PlayerPrefs.GetInt("Length", 30);
        GameManager.Instance.UpdateLengthCost();
        mainScreen.idleButtons.UpdateLengthButton(GameManager.Instance.length, GameManager.Instance.lengthCost);
        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnStrengthButtonClicked()
    {
        GameManager.Instance.wallet -= GameManager.Instance.strengthCost;
        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        PlayerPrefs.SetInt("Strength", PlayerPrefs.GetInt("Strength", 3) + 1);
        GameManager.Instance.strength = PlayerPrefs.GetInt("Strength", 3);
        GameManager.Instance.UpdateStrengthCost();
        mainScreen.idleButtons.UpdateStrengthButton(GameManager.Instance.strength, GameManager.Instance.strengthCost);
        UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnOfflineEarningsButtonClicked()
    {
        GameManager.Instance.wallet -= GameManager.Instance.offlineEarningsCost;
        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        PlayerPrefs.SetInt("Offline", PlayerPrefs.GetInt("Offline", 3) + 1);
        GameManager.Instance.offlineEarnings = PlayerPrefs.GetInt("Offline", 3);
        GameManager.Instance.UpdateOfflineEarningCost();
        mainScreen.idleButtons.UpdateOfflineEarningsButton(GameManager.Instance.offlineEarnings, GameManager.Instance.offlineEarningsCost);
        UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnCollectButtonClicked()
    {
        GameManager.Instance.wallet += GameManager.Instance.totalGain;
        startScreen.sourceImage.SetActive(false);
        mainScreen.sourceImage.SetActive(true);
        mainScreen.hookButton.interactable = true;
        UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnCollect2XButtonClicked()
    {
        GameManager.Instance.wallet += GameManager.Instance.totalGain * 2;
        startScreen.sourceImage.SetActive(false);
        mainScreen.sourceImage.SetActive(true);
        mainScreen.hookButton.interactable = true;
        UpdateWalletText();
        UpdateIdleButtons();
    }
}