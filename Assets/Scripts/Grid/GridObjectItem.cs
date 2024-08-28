using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GridObjectItem : MonoBehaviour
{
    //protected GameObject itemPrefab;
    [SerializeField] private List<GridObjecItemSubCell> _gridObjecItemSubCells;
    public GridObjecItemSubCell TopGridObjectItemCell { get; set; }
    public GridObjectItemData CurrentGridObjectItemData { get; set; }
    HashSet<int> _dataUsedIndices = new HashSet<int>();

    private int _gridObjectItemXPosition;
    private int _gridObjectItemYPosition;
    //public ItemType itemType;

    private void OnEnable()
    {
        //GridBoardEventSystem.SetGridObjectItemType += HandleSettingSubCellsType;
    }

    private void OnDisable()
    {
        //GridBoardEventSystem.SetGridObjectItemType -= HandleSettingSubCellsType;
    }

    public void HandleSettingSubCellsType(GridSystem<GridObject<GridObjectItem>> grid, GridObjectItemData[] gridObjectItemDatas, int x, int y, List<GridGroup> gridGroupData)
    {
        _gridObjectItemXPosition = x;
        _gridObjectItemYPosition = y;

        SetSubCells(grid, gridObjectItemDatas, _gridObjectItemXPosition, _gridObjectItemYPosition, gridGroupData);
    }

    private void SetSubCells(GridSystem<GridObject<GridObjectItem>> grid, GridObjectItemData[] gridObjectItemDatas, int x, int y, List<GridGroup> gridGroupData)
    {
        foreach (GridObjecItemSubCell subCell in _gridObjecItemSubCells)
        {
            //TODO: ADJUST THIS METHOD TO PREVENT SAME COLOR CELL ON A GRID OBJECT ITEM
            if (subCell.id == 0) // Toppest Cells
            {
                subCell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas));
                SetInitialTopSubCell(grid, subCell, gridObjectItemDatas, x, y);
            }
            else if (subCell.id == 1)// other cells
            {
                subCell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas)); //TODO: IMPROVE RANDOM ALGORITHM
                SetOtherSubCellWithGroup(subCell, subCell.id - 1, gridGroupData, x, y, gridObjectItemDatas);
            }
            else if (subCell.id == 2)
            {
                SetOtherSubCellWithGroup(subCell, subCell.id - 1, gridGroupData, x, y, gridObjectItemDatas);
            }

            subCell.cellManager.gridObjectItem = this;
        }
    }

    private void SetOtherSubCellWithGroup(GridObjecItemSubCell subCell, int subCellLayer, List<GridGroup> gridGroupData, int x, int y, GridObjectItemData[] gridObjectItemDatas)
    {
        GridGroup layerGroups = gridGroupData[subCellLayer];

        foreach (var group in layerGroups.groups)
        {
            //List<GridPosition> groupPosition = group.groupPosition;

            foreach (var item in group.items)
            {
                if (x == item.gridPosition.x && y == item.gridPosition.y)
                {
                    subCell.cellManager.SetSubCellColor(group.gridObjectItemData);
                    subCell.cellManager.SetSubCellItemType(item.cellItemType);
                    subCell.cellManager.SetCellItem(item);
                }
            }

            /*             foreach (var position in groupPosition)
                        {
                            if (x == position.x && y == position.y)
                            {
                                //_groupBelongsTo = group;

                                subCell.cellManager.SetSubCellColor(group.gridObjectItemData);
                                subCell.cellManager.SetSubCellItemType(position.cellItemType);
                                subCell.cellManager.SetCellGroup(group);
                            }
                        } */
        }
    }

    private void SetInitialTopSubCell(GridSystem<GridObject<GridObjectItem>> grid, GridObjecItemSubCell subCell, GridObjectItemData[] gridObjectItemDatas, int x, int y)
    {
        TopGridObjectItemCell = subCell;

        if (y == 0) //I assigned a color to the bottom cells and placed a frog object in all of them to start with.
        {
            TopGridObjectItemCell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas));
            TopGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.FROG);
            TopGridObjectItemCell.cellManager.CreateSubCellItem();
        }
        else // I initially assigned the color of the top cell of the grid object below it to the top cell of each grid object except the bottom one.
        {
            GridObjectItemData previousGridObjectTopSubCellItemData = null;

            GridObject<GridObjectItem> previousGridObject = grid.GetValue(x, y - 1);
            GridObjectItem previousGridObjectItem = previousGridObject.GetValue();
            List<GridObjecItemSubCell> previousGridObjecItemSubCells = previousGridObjectItem.GetGridObjecItemSubCells();
            foreach (var previousGridObjectItemSubCell in previousGridObjecItemSubCells)
            {
                if (previousGridObjectItemSubCell.id == 0)
                {
                    SubCellManager previousGridObjectItemTopSubCell = previousGridObjectItemSubCell.cellManager;
                    previousGridObjectTopSubCellItemData = previousGridObjectItemTopSubCell.SubCellItemData;
                }
            }

            TopGridObjectItemCell.cellManager.SetSubCellColor(previousGridObjectTopSubCellItemData);
            TopGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.GRAPE);

            TopGridObjectItemCell.cellManager.CreateSubCellItem();
        }
    }

    public void ResetSubCellID()
    {
        _gridObjecItemSubCells.RemoveAt(0);

        for (int i = 0; i < _gridObjecItemSubCells.Count; i++)
        {
            _gridObjecItemSubCells[i].id = i;

            if (i == 0)
                ChangeTopGridObjectItemCell(_gridObjecItemSubCells[i]);
        }

        if (_gridObjecItemSubCells.Count <= 0)
            return;

        TopGridObjectItemCell.cellManager.CreateSubCellItem();
        /*         if (CheckIsBoundaryGridObject())
                {
                    float randomValue = UnityEngine.Random.Range(0f, 1f);
                    if (randomValue <= 0.4f)
                    {
                        //TopGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.FROG);
                    }
                    else
                    {
                        //TopGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.GRAPE);
                    }
                } */
        //else
        //TopGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.GRAPE);
    }

    private void ChangeTopGridObjectItemCell(GridObjecItemSubCell topGridObjectItemCell)
    {
        TopGridObjectItemCell = topGridObjectItemCell;
        //Debug.Log("new top subcell: " + topGridObjectItemCell);
    }

    private GridObjectItemData GetRandomGridObjectItemType(GridObjectItemData[] gridObjectItemDatas)
    {
        if (gridObjectItemDatas == null || gridObjectItemDatas.Length == 0)
        {
            Debug.LogError("The array is empty or not assigned.");
            return null;
        }

        if (_dataUsedIndices.Count >= gridObjectItemDatas.Length)
        {
            Debug.LogError("All items have been used.");
            return null;
        }

        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, gridObjectItemDatas.Length);
        } while (_dataUsedIndices.Contains(randomIndex));

        _dataUsedIndices.Add(randomIndex);
        return gridObjectItemDatas[randomIndex];
    }

    public List<GridObjecItemSubCell> GetGridObjecItemSubCells()
    {
        return _gridObjecItemSubCells;
    }

    private bool CheckIsBoundaryGridObject()
    {
        return _gridObjectItemXPosition == 0 || _gridObjectItemYPosition == 0
        || _gridObjectItemXPosition == GridBoardManager.Instance.GetWidth() - 1 || _gridObjectItemYPosition == GridBoardManager.Instance.GetHeight() - 1;
    }

}

[Serializable]
public class GridObjecItemSubCell
{
    public int id;
    public GameObject cell;
    public SubCellManager cellManager;
}

public enum ItemColorType
{
    YELLOW,
    RED,
    BLUE,
    GREEN,
    PURPLE
}
