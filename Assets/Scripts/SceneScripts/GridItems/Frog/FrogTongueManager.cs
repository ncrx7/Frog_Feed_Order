using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueManager : MonoBehaviour
{
    [Header("Linerenderer Settings")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _tongueStartPoint;
    [SerializeField] private float _maxTongueLength = 10f;
    [SerializeField] private float _tongueExtendSpeed = 5f;
    [SerializeField] private LayerMask _correctLayerMask;
    [SerializeField] private LayerMask _incorrectLayerMask;

    [Header("References")]
    [SerializeField] FrogManager _frogManager;

    private Vector3 _tongueEndPoint;
    private float _currentTongueLength;

    private bool _isProcessing = false;
    private bool _isReturning = false;
    private bool _tongueMoveFinished = false;
    private bool _isCollectingFinishSuccess;
    private HashSet<GameObject> _correctObjects = new HashSet<GameObject>();

    void Start()
    {
        _lineRenderer.SetPosition(0, _tongueStartPoint.position);
        _tongueEndPoint = _tongueStartPoint.position;
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isProcessing)
        {
            HandleFrogTongueMove();
        }
    }

    public void HandleFrogTongueMove()
    {
        StartCoroutine(HandleFrogTongueMoveCoroutine());
    }

    private IEnumerator HandleFrogTongueMoveCoroutine()
    {
        _tongueMoveFinished = false;
        _lineRenderer.enabled = true;
        _isProcessing = true;

        while (!_tongueMoveFinished)
        {
            if (!_isReturning)
            {
                Vector3 direction = -transform.forward;
                _tongueEndPoint += direction * _tongueExtendSpeed * Time.deltaTime;
                _currentTongueLength += _tongueExtendSpeed * Time.deltaTime;

                Debug.DrawRay(_tongueEndPoint, direction, Color.blue);
                RaycastHit hit;
                if (Physics.Raycast(_tongueEndPoint, direction, out hit, _tongueExtendSpeed * Time.deltaTime))
                {
/*                     if (((1 << hit.collider.gameObject.layer) & _correctLayerMask) != 0)
                    {
                        _correctObjects.Add(hit.collider.gameObject);
                    }
                    else if (((1 << hit.collider.gameObject.layer) & _incorrectLayerMask) != 0)
                    {
                        _isReturning = true;
                    } */


                    if (hit.collider.TryGetComponent<GrapeManager>(out GrapeManager grapeManager))
                    {
                        if (_frogManager.GetSubCellBelonging().SubCellColorType == grapeManager.GetSubCellBelonging().SubCellColorType)
                        {
                            _correctObjects.Add(hit.collider.gameObject);
                        }
                        else
                        {
                            _isReturning = true;
                            _isCollectingFinishSuccess = false;
                        }
                    }
                }

                if (_currentTongueLength >= _maxTongueLength)
                {
                    _isReturning = true;
                    _isCollectingFinishSuccess = true;
                }

                _lineRenderer.SetPosition(1, _tongueEndPoint);
            }
            else
            {
                _tongueEndPoint = Vector3.MoveTowards(_tongueEndPoint, _tongueStartPoint.position, _tongueExtendSpeed * Time.deltaTime);
                _lineRenderer.SetPosition(1, _tongueEndPoint);

                if (_tongueEndPoint == _tongueStartPoint.position)
                {
                    if (_correctObjects.Count > 0 && _isCollectingFinishSuccess)
                    {
                        CollectCorrectObjects();
                    }
                    _tongueMoveFinished = true;
                    _isProcessing = false;
                    _lineRenderer.enabled = false;

                    ResetTongue();
                }
            }

            yield return null;
        }
    }

    private void CollectCorrectObjects()
    {
        foreach (GameObject obj in _correctObjects)
        {
            Debug.Log("Collected: " + obj.name);

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
        _isReturning = false;
        _currentTongueLength = 0f;
        _correctObjects.Clear();
        _lineRenderer.SetPosition(0, _tongueStartPoint.position);
        _lineRenderer.SetPosition(1, _tongueStartPoint.position);
    }
}
