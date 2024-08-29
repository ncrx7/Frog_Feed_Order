using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectItemPoolManager : MonoBehaviour
{
    public static GridObjectItemPoolManager Instance;
    private ObjectPoolManager<GridObjectCellManager> _gridObjectItemPool;
    [SerializeField] private int _initialPoolSize;
    [SerializeField] private GameObject _gridItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _initialPoolSize = GridBoardManager.Instance.GetWidth() * GridBoardManager.Instance.GetHeight();
        CreatePool();
    }

    private void CreatePool()
    {
        _gridObjectItemPool = new ObjectPoolManager<GridObjectCellManager>(_gridItemPrefab.GetComponent<GridObjectCellManager>(), _initialPoolSize, transform);
    }

    public GridObjectCellManager GetGridObjectItem()
    {
        return _gridObjectItemPool.GetObject();
    }

    public void ReturnGridObjectItem(GridObjectCellManager gridObjectItem)
    {
        _gridObjectItemPool.ReturnObject(gridObjectItem);
    }
}
