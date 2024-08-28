using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour, IGridObjectItemInteractable
{
    [SerializeField] private RotationTypes _frogRotationType;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] Texture _frogTexture;
    [SerializeField] private FrogTongueManager _frogTongueManager;
    private SubCellManager _subCellBelongsTo;

    private void Start()
    {
        //SetFrogRotation(FrogRotationType.UP);
    }

    public void SetFrogColor(GridObjectItemData SubCellItemData)
    {
        if (SubCellItemData == null || _skinnedMeshRenderer == null)
        {
            Debug.Log("item data or skinned mesh renderer is null");
            return;
        }

        //Debug.Log("set frog color worjked : " + _frogTexture);
        _skinnedMeshRenderer.material.mainTexture = SubCellItemData.FrogTexture;
    }

    
    public void SetFrogRotation(RotationTypes frogRotationType)
    {
        _frogRotationType = frogRotationType;

        Quaternion targetRotation = Quaternion.identity;

        switch (_frogRotationType)
        {
            case RotationTypes.UP:
                targetRotation = Quaternion.Euler(0, 180, 0);
                break;

            case RotationTypes.LEFT:
                targetRotation = Quaternion.Euler(0, 90, 0);
                break;

            case RotationTypes.RIGHT:
                targetRotation = Quaternion.Euler(0, 270, 0);
                break;

            case RotationTypes.DOWN:
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;

            case RotationTypes.UP_RIGHT:
                targetRotation = Quaternion.Euler(0, 225, 0);
                break;

            case RotationTypes.UP_LEFT:
                targetRotation = Quaternion.Euler(0, 135, 0);
                break;

            case RotationTypes.DOWN_LEFT:
                targetRotation = Quaternion.Euler(0, 45, 0);
                break;

            case RotationTypes.DOWN_RIGHT:
                targetRotation = Quaternion.Euler(0, 315, 0);
                break;
        }

        transform.rotation = targetRotation;
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
        _frogTongueManager.HandleFrogTongueMove();
    }
}

public enum RotationTypes
{
    UP,
    LEFT,
    RIGHT,
    DOWN,
    UP_RIGHT,
    UP_LEFT,
    DOWN_LEFT,
    DOWN_RIGHT
}
