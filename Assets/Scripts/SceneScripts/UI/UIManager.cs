using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// CAN BE ALSO A SINGLETON CLASS INSTEAD OF CONNECTING EVENT SYSTEM
/// </summary>
 
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clickAmountText;

    private void OnEnable()
    {
        GridBoardEventSystem.ChangeText += HandleChangingText;
    }

    private void OnDisable()
    {
        GridBoardEventSystem.ChangeText -= HandleChangingText;
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
}

public enum TextType
{
    CLICK_AMOUNT
}
