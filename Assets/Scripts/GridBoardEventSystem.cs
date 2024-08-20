using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoardEventSystem
{
    public static Action<int, int, GridSystem<GridObject<GridObjectItem>>, GridObjectItemData[]> CreateGridObjectItem;
}
