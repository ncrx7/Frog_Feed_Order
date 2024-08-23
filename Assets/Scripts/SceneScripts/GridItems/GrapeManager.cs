using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeManager : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] Texture _frogTexture;

    public void SetGrapeColor(GridObjectItemData SubCellItemData)
    {
        if (SubCellItemData == null || _meshRenderer == null)
        {
            Debug.Log("item data or mesh renderer is null");
            return;
        }

        //Debug.Log("set frog color worjked : " + _frogTexture);
        _meshRenderer.material.mainTexture = SubCellItemData.GrapeTexture;
    }
}
