using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoardManager : MonoBehaviour
{
    public static GridBoardManager Instance { get; private set; }

    [Header("Grid Settings")]
    GridSystem<GridObject<GridObjectItem>> _grid;
    [SerializeField] int _width = 8;
    [SerializeField] int _height = 8;
    [SerializeField] float _cellSize = 1f;
    [SerializeField] Vector3 _originPosition = Vector3.zero;
    [SerializeField] bool _debug = true;

    //[Header("Gem Settings")]
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
        _grid = GridSystem<GridObject<GridObjectItem>>.VerticalGrid(_width, _height, _cellSize, _originPosition, _debug);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //Match3Events.CreateGemObject?.Invoke(x, y, _grid, GemTypes, _gemPoolId);
                //CreateGem(x, y);
            }
        }
    }
}
