using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }
    GridSystem<GridObject<GridObjectItem>> grid;

    [Header("Click flags")]
    private bool _isProcessing = false;
    private bool IsPausedGame { get; set; } = false;
    public float SwapAmount { get; set; } = 15;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GridBoardEventSystem.ClickEvent += HandleClick;
    }

    private void OnDisable()
    {
        GridBoardEventSystem.ClickEvent -= HandleClick;
    }

    private void Start()
    {
        grid = GridBoardManager.Instance.GetGridBoard();
    }

    /// <summary>
    /// I could do this by sending rays to the scene directly with the mouse.
    /// But I wanted to use the features of the grid system I programmed.
    /// </summary>

    private void HandleClick()
    {
        Debug.Log("clicked");
        if (!_isProcessing && !IsPausedGame && SwapAmount > 0)
        {
            var gridPos = grid.GetXY(Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition));
            if (!GridBoardManager.Instance.IsValidPosition(gridPos) || GridBoardManager.Instance.IsEmptyPosition(gridPos)) return;
            //Debug.Log("gridpos: " + gridPos);

            var gridObject = grid.GetValue(gridPos.x, gridPos.y);
            var gridObjectItemObject = gridObject.GetValue();

            GridObjectItem gridObjectItem = gridObjectItemObject.GetComponent<GridObjectItem>();
            SubCellManager gridTopSubCellManager = gridObjectItem.TopGridObjectItemCell.cellManager;

            if (gridTopSubCellManager.gridObjectItemInteractable == null)
                return;

            gridTopSubCellManager.gridObjectItemInteractable.Interact();
            //Debug.Log("gridobjectitem: " + gridObjectItem);


        }
    }


}
