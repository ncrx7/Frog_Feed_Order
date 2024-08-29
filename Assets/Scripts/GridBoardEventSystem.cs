using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoardEventSystem
{
    public static Action<int, int, GridSystem<GridObject<GridObjectItem>>, GridObjectItemData[], Action<GridObjectItemData[]>> CreateGridObjectItem;
    public static Action<GridObjectItemData[]> SetGridObjectItemType;

    public static Action ClickEvent;

    #region UI Events
    public static Action<TextType, string> ChangeText;
    public static Action SwitchDefeatHudDisplay;
    #endregion
}
