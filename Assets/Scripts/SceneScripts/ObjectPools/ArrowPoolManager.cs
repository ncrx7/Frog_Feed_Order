using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolManager : MonoBehaviour
{
    public static ArrowPoolManager Instance;
    private ObjectPoolManager<ArrowManager> _arrowPool;
    [SerializeField] private int _initialPoolSize = 7;
    [SerializeField] private GameObject _arrowPrefab;

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

        CreatePool();
    }

    private void CreatePool()
    {
        _arrowPool = new ObjectPoolManager<ArrowManager>(_arrowPrefab.GetComponent<ArrowManager>(), _initialPoolSize, transform);
    }

    public ArrowManager GetArrowObject()
    {
        return _arrowPool.GetObject();
    }

    public void ReturnArrowObject(ArrowManager arrowObject)
    {
        _arrowPool.ReturnObject(arrowObject);
    }
}
