using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    public static Action<int, int, GridSystem<GridObject<GridObjectItem>>, GridObjectItemData[], Action<GridObjectItemData[]>> CreateGridObjectItem;
    public static Action<GridObjectItemData[]> SetGridObjectItemType;
    public static Action ClickEvent;
    

    #region Level UI Actions
    public static Action<TextType, string> ChangeText;
    public static Action SwitchDefeatHudDisplay;
    #endregion
    

    #region Main Scene UI Actions
    public static Action<int> ChangeScene;
    #endregion
}
