using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject<T>
{
     //I made this class generic so that it would be more dynamic and convenient for us to put another object.
     //The object class that exists on any grid object.
    GridSystem<GridObject<T>> grid;
    private int _x;
    private int _y;
    private T _gridObjectItem;

    public GridObject(GridSystem<GridObject<T>> grid, int x, int y)
    {
        this.grid = grid;
        this._x = x;
        this._y = y;
    }

    public void SetValue(T gridObject)
    {
        this._gridObjectItem = gridObject;
    }

    public T GetValue()
    {
        return _gridObjectItem;
    }
}
