using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridObjectItemManager : MonoBehaviour
{
    private void OnEnable()
    {
        GridBoardEventSystem.CreateGridObjectItem += HandleCreatingGridObjectItem;
    }

    private void OnDisable()
    {
        GridBoardEventSystem.CreateGridObjectItem -= HandleCreatingGridObjectItem;
    }

    private void HandleCreatingGridObjectItem(int x, int y, GridSystem<GridObject<GridObjectItem>> grid, GridObjectItemData[] gridObjectItemDatas, Action<GridObjectItemData[]> callback)
    {
        GridObjectItem gridObjectItem = GridObjectItemPoolManager.Instance.GetGridObjectItem();
        gridObjectItem.transform.position = grid.GetWorldPositionCenter(x, y);
        gridObjectItem.transform.SetParent(transform);
        
        //TODO: FARKLI PARENT
                                                                                                             
        gridObjectItem.HandleSettingSubCellsType(grid, gridObjectItemDatas, x, y); 
        //callback?.Invoke(gridObjectItemDatas);

        var gridObject = new GridObject<GridObjectItem>(grid, x, y);
        gridObject.SetValue(gridObjectItem); 
        grid.SetValue(x, y, gridObject); 
    }
}
