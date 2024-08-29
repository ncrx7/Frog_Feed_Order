using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoardManager : MonoBehaviour
{
    public static GridBoardManager Instance { get; private set; }

    [Header("Grid Settings")]
    GridSystem<GridObject<GridObjectCellManager>> _grid;
    [SerializeField] int _width = 5;
    [SerializeField] int _height = 5;
    [SerializeField] float _cellSize = 1f;
    [SerializeField] Vector3 _originPosition = Vector3.zero;
    [SerializeField] bool _debug = true;

    [Header("Grid Object Item Settings")]
    [SerializeField] public GridObjectItemData[] _gridObjectItemDatas;
    //[SerializeField] Gem _gemPrefab;
    //[SerializeField] public GemType[] GemTypes;

    [Header("FX")]
    [SerializeField] GameObject _explosion;

    private bool _isProcessing;
    public bool IsPausedGame { get; set; }
    Vector2Int selectedGem = Vector2Int.one * -1;
    private int _gemPoolId;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        InitializeGridAndGems();

    }

    private void InitializeGridAndGems()
    {
        _grid = GridSystem<GridObject<GridObjectCellManager>>.HorizontalGrid(_width, _height, _cellSize, _originPosition, _debug);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                EventSystem.CreateGridObjectItem?.Invoke(x, y, _grid, _gridObjectItemDatas, EventSystem.SetGridObjectItemType);
            }
        }
    }

    public GridSystem<GridObject<GridObjectCellManager>> GetGridBoard()
    {
        return _grid;
    }

    public int GetWidth() //TODO: MAKE PROPERTY HERE
    {
        return _width;
    }

    public int GetHeight() //TODO: MAKE PROPERTY HERE
    {
        return _height;
    }

    public bool IsValidPosition(Vector2 gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < _width && gridPosition.y >= 0 && gridPosition.y < _height;
    }

    public bool IsEmptyPosition(Vector2Int gridPosition) => _grid.GetValue(gridPosition.x, gridPosition.y) == null;
}
