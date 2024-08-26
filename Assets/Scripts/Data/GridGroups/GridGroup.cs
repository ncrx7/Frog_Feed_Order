using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "GridGroupData", menuName = "GridGroups/GridGroupData")]
/// <summary>
/// One subcell layer data!!!
/// </summary>
public class GridGroup : ScriptableObject
{
    public List<Group> group; // one list for one subcell layer. List need to contain all the groups of subcell layer
}

[Serializable]

public class Group
{
    public FrogRotationType frogRotationType; // each group has a frog
    public GridPosition frogPositionOnGrid;
    public List<GridPosition> groupPosition = new List<GridPosition>();
    public GridObjectItemData gridObjectItemData;
}

[Serializable]
public class GridPosition
{
    public int x;
    public int y;
    public CellItemType cellItemType;
}
