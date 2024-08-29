using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
