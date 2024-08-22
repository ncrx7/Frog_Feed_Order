using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GridObjectItem : MonoBehaviour
{
    //protected GameObject itemPrefab;
    [SerializeField] private List<GridObjecItemSubCell> _gridObjecItemSubCells;
    public GridObjecItemSubCell CurrentGridObjectItemCell { get; set; }
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

    public void HandleSettingSubCellsType(GridObjectItemData[] gridObjectItemDatas)
    {
        SetCell(gridObjectItemDatas);


    }

    private void SetCell(GridObjectItemData[] gridObjectItemDatas)
    {
        foreach (GridObjecItemSubCell cell in _gridObjecItemSubCells)
        {
            //TODO: ADJUST THIS METHOD TO PREVENT SAME COLOR CELL ON A GRID OBJECT ITEM
            cell.cellManager.SetSubCellColor(GetRandomGridObjectItemType(gridObjectItemDatas));
            Debug.Log("mesh process");

            if (cell.id == 0)
            {
                CurrentGridObjectItemCell = cell;
                CurrentGridObjectItemCell.cellManager.SetSubCellItemType(CellItemType.FROG);
            }
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
