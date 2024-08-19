using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrogType", menuName = "GridItems/FrogType")]
public class FrogData : ScriptableObject
{
    public Texture texture;
    public ItemColorType itemColorType;
}
