using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCellManager : MonoBehaviour
{
    public ItemColorType SubCellColorType;
    public CellItemType CurrentSubCellItemType;
    public GridObjectItemData SubCellItemData;
    public MeshRenderer SubCellMeshRenderer;

    public void SetSubCellColor(GridObjectItemData subCellItemData)
    {
        SubCellItemData = subCellItemData;
        SubCellMeshRenderer.material.mainTexture = SubCellItemData.CellTexture;
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
                frogManager.transform.position = transform.position;
                //get frog from frog pool
                break;
            case CellItemType.GRAPE:
                GrapeManager grapeManager = GrapePoolManager.Instance.GetGrapeObject();
                grapeManager.SetGrapeColor(SubCellItemData);
                grapeManager.transform.position = transform.position;
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