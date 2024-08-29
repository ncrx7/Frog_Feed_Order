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
    public List<Group> groups; // one list for one subcell layer. List need to contain all the groups of subcell layer
}

[Serializable]

public class Group
{
    /*     public RotationTypes frogRotationType; // each group has a frog
        public GridPosition frogPositionOnGrid;
        public List<GridPosition> groupPosition = new List<GridPosition>();
        public GridObjectItemData gridObjectItemData; */

    public List<Item> items;
    public GridObjectItemData gridObjectItemData;

    public List<Frog> frogs;
    public List<Grape> grapes;
    public List<Arrow> arrows;

    public void AddItemsToList()
    {
        items.Clear();

        foreach (var item in frogs)
        {
            items.Add(item);
        }

        foreach (var item in grapes)
        {
            items.Add(item);
        }

        foreach (var item in arrows)
        {
            items.Add(item);
        }
    }
}

[Serializable]
public class Item
{
    public GridPosition gridPosition;
    public CellItemType cellItemType;
}

[Serializable]
public class Frog : Item
{
    public RotationTypes frogRotationType;
}

[Serializable]
public class Arrow : Item
{
    public RotationTypes arrowRotationType;
    public Vector3 routeDirection;
}

[Serializable]
public class Grape : Item
{

}

[Serializable]
public class GridPosition
{
    public int x;
    public int y;
}
