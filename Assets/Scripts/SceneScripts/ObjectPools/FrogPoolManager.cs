using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPoolManager : MonoBehaviour
{
    public static FrogPoolManager Instance;
    private ObjectPoolManager<FrogManager> _frogPool;
    [SerializeField] private int _initialPoolSize = 7;
    [SerializeField] private GameObject _frogPrefab;

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

    private void Start()
    {
        //CreatePool();
    }

    private void CreatePool()
    {
        _frogPool = new ObjectPoolManager<FrogManager>(_frogPrefab.GetComponent<FrogManager>(), _initialPoolSize, transform);
    }

    public FrogManager GetFrogObject()
    {
        return _frogPool.GetObject();
    }

    public void ReturnFrogObject(FrogManager frogObject)
    {
        _frogPool.ReturnObject(frogObject);
    }
}
