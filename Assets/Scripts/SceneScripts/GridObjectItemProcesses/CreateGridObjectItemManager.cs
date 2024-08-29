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
    }

    private void OnEnable()
    {
        EventSystem.CreateGridObjectItem += HandleCreatingGridObjectItem;
    }

    private void OnDisable()
    {
        EventSystem.CreateGridObjectItem -= HandleCreatingGridObjectItem;
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
        _gridGroupsData = LevelManager.Instance.LevelDatas[LevelManager.Instance.PlayerLevel - 1].gridGroups; //TODO: COMPARE LEVEL AND LEVEL THAT INSIDE LEVEL DATA.

        foreach (var layerData in _gridGroupsData)
        {
            foreach (var item in layerData.groups)
            {
                item.AddItemsToList();
            }
        }
    }
}
