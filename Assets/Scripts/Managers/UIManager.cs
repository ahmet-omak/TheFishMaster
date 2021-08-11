using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
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
        //Set Wallet Text
        mainScreen.walletText.text = "$" + GameManager.Instance.wallet.ToString();

        //Length Button
        mainScreen.idleButtons.lengthButtonTextArea.unitLength.text = GameManager.Instance.length.ToString() + "M";
        mainScreen.idleButtons.lengthButtonTextArea.unitPrice.text = "$" + GameManager.Instance.lengthCost.ToString();

        //Strength Button
        mainScreen.idleButtons.strengthButtonTextArea.unitLength.text = GameManager.Instance.strength.ToString() + " Fishes";
        mainScreen.idleButtons.strengthButtonTextArea.unitPrice.text = "$" + GameManager.Instance.strengthCost.ToString();

        //Offline Earnings Button
        mainScreen.idleButtons.offlineButtonTextArea.unitLength.text = "$" + GameManager.Instance.offlineEarnings.ToString() + "/Min";
        mainScreen.idleButtons.offlineButtonTextArea.unitPrice.text = "$" + GameManager.Instance.offlineEarningsCost.ToString();

        //Idle Buttons
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

    public void FishingStart()
    {
        //What happens in UI when fishing starts
        mainScreen.sourceImage.gameObject.SetActive(false);
    }

    public void FishingStop(List<FishController> hookedFishes)
    {
        //What happens in UI when fishing stops
        mainScreen.sourceImage.gameObject.SetActive(true);

        for (int i = 0; i < hookedFishes.Count; i++)
        {
            GameManager.Instance.wallet += hookedFishes[i].GetComponent<FishController>().fish.price;
        }

        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);

        mainScreen.UpdateWalletText();

        UpdateIdleButtons();
    }

    public void OnLengthButtonClicked()
    {
        PlayerPrefs.SetInt("Length", PlayerPrefs.GetInt("Length", 30) + 10);

        GameManager.Instance.length = PlayerPrefs.GetInt("Length", 30);

        GameManager.Instance.UpdateLengthCost();

        mainScreen.idleButtons.UpdateLengthButton(GameManager.Instance.length, GameManager.Instance.lengthCost);

        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        mainScreen.UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnStrengthButtonClicked()
    {
        PlayerPrefs.SetInt("Strength", PlayerPrefs.GetInt("Strength", 3) + 1);

        GameManager.Instance.strength = PlayerPrefs.GetInt("Strength", 3);

        GameManager.Instance.UpdateStrengthCost();

        mainScreen.idleButtons.UpdateStrengthButton(GameManager.Instance.strength, GameManager.Instance.strengthCost);

        PlayerPrefs.SetInt("Wallet", GameManager.Instance.wallet);
        mainScreen.UpdateWalletText();
        UpdateIdleButtons();
    }

    public void OnOfflineEarningsButtonClicked()
    {

    }
}

[Serializable]
class MainScreen
{
    public GameObject sourceImage;
    public Image fishermanImage;
    public TextMeshProUGUI walletText;
    public IdleButton idleButtons;

    public void UpdateWalletText()
    {
        walletText.text = "$" + GameManager.Instance.wallet.ToString();
    }
}

[Serializable]
class IdleButton
{
    public Button lengthButton;
    public Button strengthButton;
    public Button offlineEarningsButton;

    public TextArea lengthButtonTextArea;
    public TextArea strengthButtonTextArea;
    public TextArea offlineButtonTextArea;

    public void UpdateLengthButton(int length, int lengthCost)
    {
        lengthButtonTextArea.unitLength.text = length.ToString() + "M";
        lengthButtonTextArea.unitPrice.text = "$" + lengthCost.ToString();
        GameManager.Instance.wallet -= GameManager.Instance.lengthCost;
    }

    public void UpdateStrengthButton(int strength, int strengthCost)
    {
        strengthButtonTextArea.unitLength.text = strength.ToString() + "M";
        strengthButtonTextArea.unitPrice.text = "$" + strengthCost.ToString();
        GameManager.Instance.wallet -= GameManager.Instance.strengthCost;
    }

    public void UpdateOfflineEarningsButton()
    {

    }
}

[Serializable]
class TextArea
{
    public TextMeshProUGUI unitLength;
    public TextMeshProUGUI unitPrice;

    public void UpdateUnitLength(int length)
    {
        unitLength.text = length.ToString();
    }

    public void UpdateUnitPrice(int price)
    {
        unitPrice.text = "$" + price;
    }
}