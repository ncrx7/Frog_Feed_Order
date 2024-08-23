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
    //public ItemType itemType;

    private void OnEnable()
    {
        //GridBoardEventSystem.SetGridObjectItemType += HandleSettingSubCellsType;
    }

    private void OnDisable()
    {
        //GridBoardEventSystem.SetGridObjectItemType -= HandleSettingSubCellsType;
    }

    public void HandleSettingSubCellsType(GridSystem<GridObject<GridObjectItem>> grid, GridObjectItemData[] gridObjectItemDatas, int x, int y)
    {
        SetSubCells(grid, gridObjectItemDatas, x, y);
    }

    private void SetSubCells(GridSystem<GridObject<GridObjectItem>> grid, GridObjectItemData[] gridObjectItemDatas, int x, int y)
    {
        foreach (GridObjecItemSubCell subCell in _gridObjecItemSubCells)
        {
            //TODO: ADJUST THIS METHOD TO PREVENT SAME COLOR CELL ON A GRID OBJECT ITEM
            Debug.Log("mesh process");

            if (subCell.id == 0) // Toppest Cells
            {
                //subCell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas));
                SetInitialTopSubCell(grid, subCell, gridObjectItemDatas, x, y);
            }
            else // other cells
            {
                subCell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas));
            }
        }
    }

    private void SetInitialTopSubCell(GridSystem<GridObject<GridObjectItem>> grid, GridObjecItemSubCell subCell, GridObjectItemData[] gridObjectItemDatas, int x, int y)
    {
        TopGridObjectItemCell = subCell;

        if (y == 0) //I assigned a color to the bottom cells and placed a frog object in all of them to start with.
        {
            TopGridObjectItemCell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas));
            TopGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.FROG);
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
        }
    }

    private void ResetSubCellID()
    {
        for (int i = 0; i < _gridObjecItemSubCells.Count; i++)
        {
            _gridObjecItemSubCells[i].id = i;
        }
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
