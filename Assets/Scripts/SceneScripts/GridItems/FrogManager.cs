using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Texture _frogTexture;

    public void SetFrogColor(GridObjectItemData SubCellItemData)
    {
         if(SubCellItemData == null || skinnedMeshRenderer == null)
         {
            Debug.Log("item data or skinned mesh renderer is null");
            return;
         }
         
        //Debug.Log("set frog color worjked : " + _frogTexture);
        skinnedMeshRenderer.material.mainTexture = SubCellItemData.FrogTexture; 
    }
}
