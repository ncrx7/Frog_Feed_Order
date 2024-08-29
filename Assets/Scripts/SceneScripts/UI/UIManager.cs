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
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject _defeatHUD;
    [SerializeField] private GameObject _victoryHUD;
    [SerializeField] private GameObject _finishedGameHUD;

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

    private void Start()
    {
        EventSystem.ChangeText?.Invoke(TextType.LEVEL, LevelManager.Instance.PlayerLevel.ToString());
    }

    private void HandleChangingText(TextType textType, string text)
    {
        switch (textType)
        {
            case TextType.CLICK_AMOUNT:
                _clickAmountText.text = text + " MOVES";
                break;
            case TextType.LEVEL:
                _levelText.text = "LEVEL " + text;
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
            case HudType.FINISHEDGAME_HUD:
                _finishedGameHUD.SetActive(!_defeatHUD.activeSelf);
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

    public void MainMenuButton()
    {
        EventSystem.ChangeScene?.Invoke(0);
    }

    public void RestartLevelButton()
    {
        EventSystem.ChangeScene?.Invoke(1);
    }
}

public enum TextType
{
    CLICK_AMOUNT,
    LEVEL
}

public enum HudType
{
    DEFEAT_HUD,
    VICTORY_HUD,
    FINISHEDGAME_HUD
}
