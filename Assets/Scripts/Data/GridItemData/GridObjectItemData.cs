using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridObjectItemData", menuName = "GridItems/GridObjectItemData")]
public class GridObjectItemData : ScriptableObject
{
    public Texture CellTexture;
    public Texture FrogTexture;
    public Texture GrapeTexture;
    public ItemColorType ItemColorType;
}
