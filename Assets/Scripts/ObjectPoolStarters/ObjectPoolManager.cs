// Assets/Scripts/ObjectPoolManager.cs
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager<T> where T : Component
{
    private Queue<T> objectPool = new Queue<T>();
    private T prefab;
    private Transform parentTransform;

    public ObjectPoolManager(T prefab, int initialSize, Transform parentTransform = null)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;

        for (int i = 0; i < initialSize; i++)
        {
            T newObject = GameObject.Instantiate(prefab, parentTransform);

            newObject.gameObject.SetActive(false);
            objectPool.Enqueue(newObject);
        }
    }

    public T GetObject()
    {
        if (objectPool.Count > 0)
        {
            T pooledObject = objectPool.Dequeue();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }
        else
        {
            T newObject = GameObject.Instantiate(prefab, parentTransform);
            return newObject;
        }
    }

    public void ReturnObject(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objectPool.Enqueue(objectToReturn);
    }
}

