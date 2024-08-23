using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueManager : MonoBehaviour
{
    [Header("Linerenderer Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform tongueStartPoint;
    [SerializeField] private float maxTongueLength = 10f;
    [SerializeField] private float tongueExtendSpeed = 5f;
    [SerializeField] private LayerMask correctLayerMask;
    [SerializeField] private LayerMask incorrectLayerMask;

    [Header("References")]
    [SerializeField] FrogManager _frogManager;

    private Vector3 tongueEndPoint;
    private float currentTongueLength;

    private bool _isProcessing = false;
    private bool isReturning = false;
    private bool _tongueMoveFinished = false;
    private HashSet<GameObject> correctObjects = new HashSet<GameObject>();

    void Start()
    {
        // Dilin başlangıç noktasını ayarla
        lineRenderer.SetPosition(0, tongueStartPoint.position);
        tongueEndPoint = tongueStartPoint.position;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_isProcessing)
        {
            StartCoroutine(HandleFrogTongueMove());
        }
    }

    public IEnumerator HandleFrogTongueMove()
    {
        _tongueMoveFinished = false;
        lineRenderer.enabled = true;
        _isProcessing = true;

        while (!_tongueMoveFinished)
        {
            if (!isReturning)
            {
                Vector3 direction = -transform.forward;
                tongueEndPoint += direction * tongueExtendSpeed * Time.deltaTime;
                currentTongueLength += tongueExtendSpeed * Time.deltaTime;

                Debug.DrawRay(tongueEndPoint, direction, Color.blue);
                RaycastHit hit;
                if (Physics.Raycast(tongueEndPoint, direction, out hit, tongueExtendSpeed * Time.deltaTime))
                {
                    if (((1 << hit.collider.gameObject.layer) & correctLayerMask) != 0)
                    {
                        correctObjects.Add(hit.collider.gameObject);
                    }
                    else if (((1 << hit.collider.gameObject.layer) & incorrectLayerMask) != 0)
                    {
                        isReturning = true;
                    }
                }

                if (currentTongueLength >= maxTongueLength)
                {
                    isReturning = true;
                }

                lineRenderer.SetPosition(1, tongueEndPoint);
            }
            else
            {
                tongueEndPoint = Vector3.MoveTowards(tongueEndPoint, tongueStartPoint.position, tongueExtendSpeed * Time.deltaTime);
                lineRenderer.SetPosition(1, tongueEndPoint);

                if (tongueEndPoint == tongueStartPoint.position)
                {
                    if (correctObjects.Count > 0)
                    {
                        CollectCorrectObjects();
                    }
                    _tongueMoveFinished = true;
                    _isProcessing = false;
                    lineRenderer.enabled = false;
        
                    ResetTongue();
                }
            }

            yield return null;
        }
    }

    private void CollectCorrectObjects()
    {
        foreach (GameObject obj in correctObjects)
        {
            // Burada objelerle ne yapacağını belirt
            Debug.Log("Collected: " + obj.name);
            // Örnek: objeyi sahneden sil

            GrapeManager grapeManager = obj.GetComponent<GrapeManager>();

            SubCellManager grapeSubCell = grapeManager.GetSubCellBelonging();
            SubCellManager frogSubCell = _frogManager.GetSubCellBelonging();

            //I am destroying top subcell. I dont use cell pool because subcells is instantiating only once in game
            Destroy(grapeSubCell.gameObject);
            Destroy(frogSubCell.gameObject);

            //TODO: SET NEW TOP CELL AND SPAWN NEW FROG OR GRAP

            GrapePoolManager.Instance.ReturnGrapeObject(grapeManager);
            FrogPoolManager.Instance.ReturnFrogObject(_frogManager);
        }
    }

    private void ResetTongue()
    {
        isReturning = false;
        currentTongueLength = 0f;
        correctObjects.Clear();
        lineRenderer.SetPosition(0, tongueStartPoint.position);
        lineRenderer.SetPosition(1, tongueStartPoint.position);
    }
}
