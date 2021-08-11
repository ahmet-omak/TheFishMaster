using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    //TODO:
    /*
     * Code StartScreen
     * Code money earn logic while waiting
     */

    [SerializeField] MainScreen mainScreen;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        InitMainScreenParameters();
    }

    private void InitMainScreenParameters()
    {
        UpdateWalletText();
   
        mainScreen.idleButtons.UpdateLengthButton(GameManager.Instance.length, GameManager.Instance.lengthCost);
        
        mainScreen.idleButtons.UpdateStrengthButton(GameManager.Instance.strength,GameManager.Instance.strengthCost);

        mainScreen.idleButtons.UpdateOfflineEarningsButton(GameManager.Instance.offlineEarnings, GameManager.Instance.offlineEarningsCost);

        UpdateIdleButtons();
    }

    private void UpdateIdleButtons()
    {
        int money = GameManager.Instance.wallet;

        //Length Button
        if (money >= GameManager.Instance.lengthCost)
        {
            mainScreen.idleButtons.lengthButton.interactable = true;
        }
        else
        {
            mainScreen.idleButtons.lengthButton.interactable = false;
        }

        //Strength Button
        if (money > GameManager.Instance.strengthCost)
        {
            mainScreen.idleButtons.strengthButton.interactable = true;
        }
        else
        {
            mainScreen.idleButtons.strengthButton.interactable = false;
        }

        //Offline Earnings Button
        if (money > GameManager.Instance.offlineEarningsCost)
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

    public void OnFishingStarted()
    {
        //What happens in UI when fishing starts
        mainScreen.sourceImage.gameObject.SetActive(false);
    }

    public void OnFishingStopped(List<FishController> hookedFishes)
    {
        //What happens in UI when fishing stops
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
}