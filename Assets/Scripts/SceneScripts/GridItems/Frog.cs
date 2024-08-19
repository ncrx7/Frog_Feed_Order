using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : GridObjectItem
{
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    public FrogData frogData;
    public FrogDirection frogDirection;

    private void Start()
    {
        itemType = ItemType.FROG;
    }

    public void SetFrogType(FrogData frogData)
    {
        this.frogData = frogData;
        if (_skinnedMeshRenderer != null)
        {
            Texture frogTexture = frogData.texture;
            _skinnedMeshRenderer.material.mainTexture = frogTexture;
        }
    }

    public FrogData GetFrogType()
    {
        return frogData;
    }

}

public enum FrogDirection
{
    UP,
    LEFT,
    RIGHT,
    DOWN,
    UP_LEFT,
    UP_RIGHT,
    DOWN_LEFT,
    DOWN_RIGHT
}
