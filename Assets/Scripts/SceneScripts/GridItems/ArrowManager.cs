using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour, IGridObjectItemInteractable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private SubCellManager _subCellBelongsTo;

    public void SetArrowColor(GridObjectItemData SubCellItemData)
    {
        if (SubCellItemData == null || _spriteRenderer == null)
        {
            Debug.Log("item data or sprite renderer is null");
            return;
        }

        _spriteRenderer.color = SubCellItemData.color;
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
        Debug.LogWarning("You can not click the arrows!!");
    }
}
