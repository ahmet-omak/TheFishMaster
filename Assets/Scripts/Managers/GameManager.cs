using UnityEngine;
using System;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("Debug Values")]
    public int length;
    public int lengthCost;
    public int strength;
    public int strengthCost;
    public int offlineEarnings;
    public int offlineEarningsCost;
    public int wallet;
    public int totalGain;

    [SerializeField] Cost cost;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetCostValues();
        GetParameters();
        CalculateGainedMoney();
    }

    private void OnApplicationQuit()
    {
        SetUserQuitTime();
    }

    private void SetUserQuitTime()
    {
        DateTime currentTime = DateTime.Now;
        PlayerPrefs.SetString("Date", currentTime.ToString());
    }

    private void GetParameters()
    {
        length = PlayerPrefs.GetInt("Length", 30);
        strength = PlayerPrefs.GetInt("Strength", 3);
        offlineEarnings = PlayerPrefs.GetInt("Offline", 3);
        wallet = PlayerPrefs.GetInt("Wallet", 0);

        UpdateLengthCost();
        UpdateStrengthCost();
        UpdateOfflineEarningCost();
    }

    public void UpdateLengthCost()
    {
        lengthCost = cost.allCosts[length / 10 - 3];
    }

    public void UpdateStrengthCost()
    {
        strengthCost = cost.allCosts[strength - 3];
    }

    public void UpdateOfflineEarningCost()
    {
        offlineEarningsCost = cost.allCosts[offlineEarnings - 3];
    }

    private void CalculateGainedMoney()
    {
        string str = PlayerPrefs.GetString("Date", string.Empty);
        if (str != string.Empty)
        {
            DateTime time = DateTime.Parse(str);
            totalGain = (int)((DateTime.Now - time).TotalMinutes * offlineEarnings + 1);
        }
    }

    private void SetCostValues()
    {
        cost.allCosts = new int[cost.allCosts.Length];
        cost.allCosts[0] = cost.startCost;
        for (int i = 1; i < cost.allCosts.Length; i++)
        {
            cost.allCosts[i] = cost.allCosts[i - 1] + cost.incrementer * 2;
            cost.incrementer = UnityEngine.Random.Range(15, 30);
        }
    }

    public void OnDeleteDataButtonClicked()
    {
        PlayerPrefs.DeleteAll();
    }
}
