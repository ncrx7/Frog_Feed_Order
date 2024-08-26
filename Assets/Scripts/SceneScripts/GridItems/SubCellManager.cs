using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCellManager : MonoBehaviour
{
    public ItemColorType SubCellColorType;
    public CellItemType CurrentSubCellItemType;
    public GridObjectItem gridObjectItem;
    public GridObjectItemData SubCellItemData;
    public MeshRenderer SubCellMeshRenderer;

    /// <summary>
    /// This is unnecessary since we will only interact with the frog in this game.
    /// But I am using an interface so that interacting with more than one object on the board will be easier programmatically in the future.
    /// </summary>
    public IGridObjectItemInteractable gridObjectItemInteractable;

    public void SetSubCellColor(GridObjectItemData subCellItemData)
    {
        SubCellItemData = subCellItemData;
        SubCellMeshRenderer.material.mainTexture = SubCellItemData.CellTexture;
        SubCellColorType = SubCellItemData.ItemColorType;
    }

    public void SetSubCellItemType(CellItemType cellItemType)
    {
        CurrentSubCellItemType = cellItemType;
        CreateSubCellItem();
    }

    private void CreateSubCellItem()
    {
        switch (CurrentSubCellItemType)
        {
            case CellItemType.FROG:
                FrogManager frogManager = FrogPoolManager.Instance.GetFrogObject();
                frogManager.SetFrogColor(SubCellItemData);
                frogManager.SetSubCellBelonging(this);
                frogManager.transform.position = transform.position;
                gridObjectItemInteractable = frogManager;
                //get frog from frog pool
                break;
            case CellItemType.GRAPE:
                GrapeManager grapeManager = GrapePoolManager.Instance.GetGrapeObject();
                grapeManager.SetGrapeColor(SubCellItemData);
                grapeManager.SetSubCellBelonging(this);
                grapeManager.transform.position = transform.position;
                gridObjectItemInteractable = grapeManager;
                //get grape from grape pool
                break;
            case CellItemType.ARROW:
                //get arrow from arrow pool
                break;
            default:
                Debug.LogWarning("Undefined cell item type");
                break;
        }
    }
}

public enum CellItemType
{
    FROG,
    GRAPE,
    ARROW,
    NULL
}