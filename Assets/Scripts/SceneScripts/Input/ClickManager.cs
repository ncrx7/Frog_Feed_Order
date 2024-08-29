using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }
    [Header("Player Control Capacity")]
    [SerializeField] private int _clickAmount;

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
        EventSystem.ChangeText?.Invoke(TextType.CLICK_AMOUNT, _clickAmount.ToString());
        EventSystem.ClickEvent += HandleClick;
    }

    private void OnDisable()
    {
        EventSystem.ClickEvent -= HandleClick;
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
        if (!_isProcessing && !IsPausedGame && _clickAmount > 0)
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

            //ReduceCickAmount();
        }
    }

    public void ReduceCickAmount()
    {
        //TODO: DETECTED BUG, WHEN A FROG PROCCES, IT CAN BE CLICKED AND REDUCING CLICK
        _clickAmount--;

        if(_clickAmount <= 0)
        {
            _clickAmount = 0;
            EventSystem.SwitchDefeatHudDisplay?.Invoke(HudType.DEFEAT_HUD);
            //OnClickAmountRunnedOut
        }

        EventSystem.ChangeText?.Invoke(TextType.CLICK_AMOUNT, _clickAmount.ToString());
    }


}
