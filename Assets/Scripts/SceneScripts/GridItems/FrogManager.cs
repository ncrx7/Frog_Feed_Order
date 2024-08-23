using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] Texture _frogTexture;

    public void SetFrogColor(GridObjectItemData SubCellItemData)
    {
         if(SubCellItemData == null || _skinnedMeshRenderer == null)
         {
            Debug.Log("item data or skinned mesh renderer is null");
            return;
         }
         
        //Debug.Log("set frog color worjked : " + _frogTexture);
        _skinnedMeshRenderer.material.mainTexture = SubCellItemData.FrogTexture; 
    }
}
