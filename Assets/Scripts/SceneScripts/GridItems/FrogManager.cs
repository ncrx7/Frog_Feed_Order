using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour, IGridObjectItemInteractable
{
    [SerializeField] private FrogRotationType _frogRotationType;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] Texture _frogTexture;
    [SerializeField] private FrogTongueManager _frogTongueManager;
    private SubCellManager _subCellBelongsTo;

    private void Start()
    {
        //SetFrogRotation(FrogRotationType.UP);
    }

    private void FindAFrogDirection(GridSystem<GridObject<GridObjectItem>> grid, int x, int y)
    {
        //GridObjectItem[] gridObjectItems; //CurrentSubCellItemType
        List<GridObjectItem> gridObjectItems = new List<GridObjectItem>
        {
            grid.GetValue(x + 1, y).GetValue(),
            grid.GetValue(x - 1, y).GetValue(),
            grid.GetValue(x, y + 1).GetValue(),
            grid.GetValue(x, y -1).GetValue()
        };

        foreach (GridObjectItem item in gridObjectItems)
        {
            //to be continue
        }
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

    
    public void SetFrogRotation(FrogRotationType frogRotationType)
    {
        _frogRotationType = frogRotationType;

        Quaternion targetRotation = Quaternion.identity;

        switch (_frogRotationType)
        {
            case FrogRotationType.UP:
                targetRotation = Quaternion.Euler(0, 180, 0);
                break;

            case FrogRotationType.LEFT:
                targetRotation = Quaternion.Euler(0, 90, 0);
                break;

            case FrogRotationType.RIGHT:
                targetRotation = Quaternion.Euler(0, 270, 0);
                break;

            case FrogRotationType.DOWN:
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;

            case FrogRotationType.UP_RIGHT:
                targetRotation = Quaternion.Euler(0, 225, 0);
                break;

            case FrogRotationType.UP_LEFT:
                targetRotation = Quaternion.Euler(0, 135, 0);
                break;

            case FrogRotationType.DOWN_LEFT:
                targetRotation = Quaternion.Euler(0, 45, 0);
                break;

            case FrogRotationType.DOWN_RIGHT:
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

public enum FrogRotationType
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
