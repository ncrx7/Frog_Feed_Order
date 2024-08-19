using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectItem : MonoBehaviour
{
    //protected GameObject itemPrefab;
    public ItemType itemType;
    public ItemColorType itemColorType;

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

public enum ItemType
{
    FROG,
    GRAPE
}

public enum ItemColorType
{
    YELLOW,
    RED,
    BLUE,
    GREEN,
    PURPLE
}
