using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridObjectItemManager : MonoBehaviour
{
    [SerializeField] private List<GridGroup> _gridGroupsData = new List<GridGroup>();

    private void Awake()
    {
        InitializeGridGroupData();

        Debug.Log("fasdklşasdas: " + _gridGroupsData[0].groups[0].items[1].gridPosition.x + _gridGroupsData[0].groups[0].items[1].cellItemType);
    }

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
                                                                                                             
        gridObjectItem.HandleSettingSubCellsType(grid, gridObjectItemDatas, x, y, _gridGroupsData); 
        //callback?.Invoke(gridObjectItemDatas);

        var gridObject = new GridObject<GridObjectItem>(grid, x, y);
        gridObject.SetValue(gridObjectItem); 
        grid.SetValue(x, y, gridObject); 
    }

    private void InitializeGridGroupData()
    {
        foreach (var layerData in _gridGroupsData)
        {
            foreach (var item in layerData.groups)
            {
                item.AddItemsToList();
            }
        }
    }
}
