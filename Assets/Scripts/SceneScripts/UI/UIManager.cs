using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// CAN BE ALSO A SINGLETON CLASS INSTEAD OF CONNECTING EVENT SYSTEM
/// </summary>
 
public class UIManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TextMeshProUGUI _clickAmountText;
    [SerializeField] private GameObject _defeatHUD;

    private void OnEnable()
    {
        EventSystem.ChangeText += HandleChangingText;
        EventSystem.SwitchDefeatHudDisplay += HandleSwitchingDefeatHudDisplay;
    }

    private void OnDisable()
    {
        EventSystem.ChangeText -= HandleChangingText;
        EventSystem.SwitchDefeatHudDisplay -= HandleSwitchingDefeatHudDisplay;
    }

    private void HandleChangingText(TextType textType, string text)
    {
        switch (textType)
        {
            case TextType.CLICK_AMOUNT:
                _clickAmountText.text = text + " MOVES";
                break;
            default:
                Debug.LogWarning("Undefined Text Type!!");
                break;
        }
    }

    private void HandleSwitchingDefeatHudDisplay()
    {
        _defeatHUD.SetActive(!_defeatHUD.activeSelf);
    }
}

public enum TextType
{
    CLICK_AMOUNT
}
