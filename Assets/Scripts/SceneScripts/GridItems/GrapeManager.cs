using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeManager : MonoBehaviour, IGridObjectItemInteractable
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] Texture _frogTexture;
    private SubCellManager _subCellBelongsTo;

    public void SetGrapeColor(GridObjectItemData SubCellItemData)
    {
        if (SubCellItemData == null || _meshRenderer == null)
        {
            Debug.Log("item data or mesh renderer is null");
            return;
        }

        //Debug.Log("set frog color worjked : " + _frogTexture);
        _meshRenderer.material.mainTexture = SubCellItemData.GrapeTexture;
    }

    public void SetSubCellBelonging(SubCellManager subCellManager)
    {
        _subCellBelongsTo = subCellManager;
    }

    public SubCellManager GetSubCellBelonging()
    {
        return _subCellBelongsTo;
    }

    public void Interact()
    {
        Debug.LogWarning("You can not click the grapes!!");
    }
}
