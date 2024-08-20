using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectItem : MonoBehaviour
{
    //protected GameObject itemPrefab;
    [SerializeField] private List<GridObjecItemCell> _gridObjecItemCell;
    public GridObjectItemData CurrentGridObjectItemData;
    //public ItemType itemType;
    public ItemColorType ColorType;

    public void SetCellsType(GridObjectItemData[] gridObjectItemDatas)
    {
        foreach (GridObjecItemCell cell in _gridObjecItemCell)
        {
            cell.meshRenderer.material.mainTexture = GetRandomItem(gridObjectItemDatas).CellTexture;
            Debug.Log("mesh process");
        }
        /*      this.frogData = frogData;
                if (_skinnedMeshRenderer != null)
                {
                    Texture frogTexture = frogData.texture;
                    _skinnedMeshRenderer.material.mainTexture = frogTexture;
                } */
    }

    private GridObjectItemData GetRandomItem(GridObjectItemData[] gridObjectItemDatas)
    {
        if (gridObjectItemDatas == null || gridObjectItemDatas.Length == 0)
        {
            Debug.LogError("The array is empty or not assigned.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, gridObjectItemDatas.Length);
        return gridObjectItemDatas[randomIndex];
    }
}

[Serializable]
public class GridObjecItemCell
{
    public int id;
    public GameObject cell;
    public MeshRenderer meshRenderer;
}

public enum ItemColorType
{
    YELLOW,
    RED,
    BLUE,
    GREEN,
    PURPLE
}

/* public enum ItemType
{
    FROG,
    GRAPE
} */
