using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrapeManager : MonoBehaviour, IGridObjectItemClickInteractable, IHittable
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] Texture _frogTexture;
    public float moveDuration = 1f;

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

    public void ClickInteract()
    {
        Debug.LogWarning("You can not click the grapes!!");
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, moveDuration).SetEase(Ease.Linear);
    }

    public void Hit(FrogManager frogManager, ref Vector3 direction, HashSet<GameObject> collectedObjects, LineRenderer lineRenderer, List<Vector3> tonguePath, Vector3 tongueEndPoint)
    {
        collectedObjects.Add(gameObject);
    }

    public SubCellManager GetSubCellManager()
    {
        if (_subCellBelongsTo == null)
            return null;

        return _subCellBelongsTo;
    }


    /*     public ItemColorType? GetSubCellColor()
        {
            if(_subCellBelongsTo == null)
                return null;

            return _subCellBelongsTo.SubCellColorType;
        } */
}
