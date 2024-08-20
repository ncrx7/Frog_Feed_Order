using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectItemPoolManager : MonoBehaviour
{
    public static GridObjectItemPoolManager Instance;
    private ObjectPoolManager<GridObjectItem> _gridObjectItemPool;
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

        _initialPoolSize = GridBoardManager.Instance.GetWidth() * GridBoardManager.Instance.GetHeight();

        CreatePool();
    }

    private void CreatePool()
    {
        _gridObjectItemPool = new ObjectPoolManager<GridObjectItem>(_gridItemPrefab.GetComponent<GridObjectItem>(), _initialPoolSize, transform);
    }

    public GridObjectItem GetGridObjectItem()
    {
        return _gridObjectItemPool.GetObject();
    }

    public void ReturnGridObjectItem(GridObjectItem gridObjectItem)
    {
        _gridObjectItemPool.ReturnObject(gridObjectItem);
    }
}
