using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour, IGridObjectItemClickInteractable, IHittable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private RotationTypes _arrowRotationType;
    private SubCellManager _subCellBelongsTo;
    public Vector3 routeDirection;

    public void SetArrowColor(GridObjectItemData SubCellItemData)
    {
        if (SubCellItemData == null || _spriteRenderer == null)
        {
            Debug.Log("item data or sprite renderer is null");
            return;
        }

        _spriteRenderer.color = SubCellItemData.color;
    }

    public void SetArrowRotation(RotationTypes arrowRotationType)
    {
        _arrowRotationType = arrowRotationType;

        Quaternion targetRotation = Quaternion.identity;

        switch (_arrowRotationType)
        {
            case RotationTypes.UP:
                targetRotation = Quaternion.Euler(90, 270, 0);
                break;

            case RotationTypes.LEFT:
                targetRotation = Quaternion.Euler(90, 180, 0);
                break;

            case RotationTypes.RIGHT:
                targetRotation = Quaternion.Euler(90, 0, 0);
                break;

            case RotationTypes.DOWN:
                targetRotation = Quaternion.Euler(90, 90, 0);
                break;

            case RotationTypes.UP_RIGHT:
                targetRotation = Quaternion.Euler(90, 315, 0);
                break;

            case RotationTypes.UP_LEFT:
                targetRotation = Quaternion.Euler(90, 225, 0);
                break;

            case RotationTypes.DOWN_LEFT:
                targetRotation = Quaternion.Euler(90, 135, 0);
                break;

            case RotationTypes.DOWN_RIGHT:
                targetRotation = Quaternion.Euler(90, 45, 0);
                break;
        }

        transform.rotation = targetRotation;
        routeDirection = targetRotation * Vector3.forward;
    }

    public void SetRouteDirection(Vector3 direction)
    {
        routeDirection = direction;
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
        Debug.LogWarning("You can not click the arrows!!");
    }

    public void Hit(FrogManager frogManager, ref Vector3 direction, HashSet<GameObject> collectedObjects, LineRenderer lineRenderer, List<Vector3> tonguePath, Vector3 tongueEndPoint)
    {
        direction = routeDirection;
        collectedObjects.Add(gameObject);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, tongueEndPoint);
        tonguePath.Add(tongueEndPoint);
    }

    public SubCellManager GetSubCellManager()
    {
        if (_subCellBelongsTo == null)
            return null;

        return _subCellBelongsTo;
    }
}
