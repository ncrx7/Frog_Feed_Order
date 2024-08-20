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

    private void HandleCreatingGridObjectItem(int x, int y, GridSystem<GridObject<GridObjectItem>> grid, GridObjectItemData[] gridObjectItemDatas)
    {
        GridObjectItem gridObjectItem = GridObjectItemPoolManager.Instance.GetGridObjectItem();
        gridObjectItem.transform.position = grid.GetWorldPositionCenter(x, y);
        gridObjectItem.transform.SetParent(transform);
        
        //Gem gem = Instantiate(gemPrefab, grid.GetWorldPositionCenter(x, y), Quaternion.identity, transform); //TODO: FARKLI PARENT
                                                                                                             //Debug.Log("gem position: " + grid.GetWorldPositionCenter(x, y));
        gridObjectItem.SetCellsType(gridObjectItemDatas); // Rastgele gem tipini seç
        
        var gridObject = new GridObject<GridObjectItem>(grid, x, y);
        gridObject.SetValue(gridObjectItem); // Gem'i grid nesnesine initialize et
        grid.SetValue(x, y, gridObject); // Grid matrisine grid nesnesini initialize et
    }
}
