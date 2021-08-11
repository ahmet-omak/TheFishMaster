using UnityEngine.UI;
using System;

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
    }

    public void UpdateStrengthButton(int strength, int strengthCost)
    {
        strengthButtonTextArea.unitLength.text = strength.ToString() + "M";
        strengthButtonTextArea.unitPrice.text = "$" + strengthCost.ToString();
    }

    public void UpdateOfflineEarningsButton(int unit, int cost)
    {
        offlineButtonTextArea.unitLength.text = "$" + unit.ToString() + "Min.";
        offlineButtonTextArea.unitPrice.text = "$" + cost.ToString();
    }
}