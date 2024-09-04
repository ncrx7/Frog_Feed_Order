using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void Hit(FrogManager frogManager, ref Vector3 direction, HashSet<GameObject> collectedObjects, LineRenderer lineRenderer, List<Vector3> tonguePath, Vector3 tongueEndPoint);
    SubCellManager GetSubCellManager();
}
