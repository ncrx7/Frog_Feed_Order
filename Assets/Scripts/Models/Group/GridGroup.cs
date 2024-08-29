using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "GridGroupData", menuName = "GridGroups/GridGroupData")]
/// <summary>
/// One subcell layer data!!!
/// </summary>
public class GridGroup : ScriptableObject
{
    public List<Group> groups; // I made one list for one subcell layer. List need to contain all the groups of subcell layer
}

