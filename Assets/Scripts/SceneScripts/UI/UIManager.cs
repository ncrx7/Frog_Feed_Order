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
    [SerializeField] private GameObject _victoryHUD;

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

    private void HandleSwitchingDefeatHudDisplay(HudType hudType)
    {
        switch (hudType)
        {
            case HudType.DEFEAT_HUD:
                _defeatHUD.SetActive(!_defeatHUD.activeSelf);
                break;
            case HudType.VICTORY_HUD:
                _victoryHUD.SetActive(!_defeatHUD.activeSelf);
                break;
            default:
                Debug.LogWarning("Undefined Hud type");
                break;
        }
        
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.IncreaseLevel();
    }
}

public enum TextType
{
    CLICK_AMOUNT
}

public enum HudType
{
    DEFEAT_HUD,
    VICTORY_HUD
}
