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
    private Item _cellItem;
    public bool isPainted = false;

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
        //CreateSubCellItem();
    }

    public void SetCellItem(Item item)
    {
        _cellItem = item;
    }

    public void CreateSubCellItem()
    {
        switch (CurrentSubCellItemType)
        {
            case CellItemType.FROG:
                FrogManager frogManager = FrogPoolManager.Instance.GetFrogObject();
                frogManager.SetFrogColor(SubCellItemData);
                frogManager.SetSubCellBelonging(this);
                frogManager.transform.position = transform.position;

                if(_cellItem == null)
                    frogManager.SetFrogRotation(RotationTypes.UP);
                else
                {
                    Frog frogItem = _cellItem as Frog;
                    frogManager.SetFrogRotation(frogItem.frogRotationType);
                }

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
                ArrowManager arrowManager = ArrowPoolManager.Instance.GetArrowObject();
                arrowManager.SetArrowColor(SubCellItemData);
                arrowManager.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                //rotation
                Debug.Log("cell item: " + _cellItem.cellItemType);
                Arrow arrowItem = _cellItem as Arrow;
                Debug.Log("arrow rotation: " + arrowItem.arrowRotationType);
                arrowManager.SetArrowRotation(arrowItem.arrowRotationType);
                gridObjectItemInteractable = arrowManager;
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