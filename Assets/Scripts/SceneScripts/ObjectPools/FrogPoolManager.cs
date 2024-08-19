using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPoolManager : MonoBehaviour
{
    public static FrogPoolManager Instance;
    private ObjectPoolManager<Frog> _frogPool;
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
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _frogPool = new ObjectPoolManager<Frog>(_frogPrefab.GetComponent<Frog>(), _initialPoolSize, transform);
    }
}
