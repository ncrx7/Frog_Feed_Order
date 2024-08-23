using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapePoolManager : MonoBehaviour
{
    public static GrapePoolManager Instance;
    private ObjectPoolManager<GrapeManager> _grapePool;
    [SerializeField] private int _initialPoolSize = 7;
    [SerializeField] private GameObject _grapePrefab;

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
        _grapePool = new ObjectPoolManager<GrapeManager>(_grapePrefab.GetComponent<GrapeManager>(), _initialPoolSize, transform);
    }

    public GrapeManager GetGrapeObject()
    {
        return _grapePool.GetObject();
    }

    public void ReturnGrapeObject(GrapeManager grapeObject)
    {
        _grapePool.ReturnObject(grapeObject);
    }
}
