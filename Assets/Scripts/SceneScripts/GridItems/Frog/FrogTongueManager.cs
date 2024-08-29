using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueManager : MonoBehaviour
{
    [Header("Linerenderer Settings")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _tongueStartPoint;
    [SerializeField] public Vector3 direction;
    [SerializeField] private float _maxTongueLength = 10f;
    [SerializeField] private float _tongueExtendSpeed = 5f;

    [Header("References")]
    [SerializeField] FrogManager _frogManager;

    private Vector3 _tongueEndPoint;
    private float _currentTongueLength;

    private bool _isProcessing = false;
    private bool _isReturning = false;
    private bool _tongueMoveFinished = false;
    private bool _isCollectingFinishSuccess;
    private HashSet<GameObject> _grapeObjects = new HashSet<GameObject>();
    private HashSet<GameObject> _arrowObjects = new HashSet<GameObject>();
    private List<Vector3> tonguePath = new List<Vector3>();

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
        if( _isProcessing)
            return;

        StartCoroutine(HandleFrogTongueMoveCoroutine());
        ClickManager.Instance.ReduceCickAmount();
    }

    private IEnumerator HandleFrogTongueMoveCoroutine()
    {
        _tongueMoveFinished = false;
        _lineRenderer.enabled = true;
        _isProcessing = true;
        direction = -transform.forward;

        tonguePath.Clear();
        tonguePath.Add(_tongueEndPoint);

        while (!_tongueMoveFinished)
        {
            if (!_isReturning)
            {
                _tongueEndPoint += direction * _tongueExtendSpeed * Time.deltaTime;
                _currentTongueLength += _tongueExtendSpeed * Time.deltaTime;
                tonguePath.Add(_tongueEndPoint);

                Debug.DrawRay(_tongueEndPoint, direction, Color.blue);

                float sphereRadius = 0.1f;
                RaycastHit[] hits = Physics.SphereCastAll(_tongueEndPoint, sphereRadius, direction, _tongueExtendSpeed * Time.deltaTime);

                foreach (var hit in hits)
                {
                    //Debug.Log("tongue hitted - non same object");

                    if (hit.collider.TryGetComponent<GrapeManager>(out GrapeManager grapeManager))
                    {
                        //Debug.Log("tongue hitted to grape");
                        if (_frogManager.GetSubCellBelonging().SubCellColorType == grapeManager.GetSubCellBelonging().SubCellColorType)
                        {
                            _grapeObjects.Add(hit.collider.gameObject);
                        }
                        else
                        {
                            _isReturning = true;
                            _isCollectingFinishSuccess = false;
                            break;
                        }
                    }
                    else if (hit.collider.TryGetComponent<ArrowManager>(out ArrowManager arrowManager))
                    {
                        if(arrowManager.GetSubCellBelonging() == null )
                            Debug.Log("subcell null");
                                
                        if (_frogManager.GetSubCellBelonging().SubCellColorType == arrowManager.GetSubCellBelonging().SubCellColorType)
                        {
                            direction = arrowManager.routeDirection;
                            _arrowObjects.Add(hit.collider.gameObject);
                        }
                        else
                        {
                            _isReturning = true;
                            _isCollectingFinishSuccess = false;
                            break;
                        }
                        //direction = new Vector3(1, 0, 1);

                        Debug.Log("direction: " + direction);

                        _lineRenderer.positionCount++;
                        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _tongueEndPoint);

                        tonguePath.Add(_tongueEndPoint);
                    }
                }

                if (_currentTongueLength >= _maxTongueLength)
                {
                    _isReturning = true;
                    _isCollectingFinishSuccess = true;
                    foreach (var grape in _grapeObjects)
                    {
                        //grape.GetComponent<GrapeManager>().MoveToTarget(transform.position); //TODO: SET TONGUE SPEED TO MOVING OBJECT SPEED
                        if(grape.TryGetComponent<GrapeManager>(out GrapeManager grapeManager))
                        {
                            grapeManager.MoveToTarget(transform.position);
                        }
                    }
                }

                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _tongueEndPoint);
            }
            else
            {
                if (tonguePath.Count > 1)
                {
                    tonguePath.RemoveAt(tonguePath.Count - 1);
                    _tongueEndPoint = Vector3.MoveTowards(_tongueEndPoint, tonguePath[tonguePath.Count - 1], _tongueExtendSpeed * Time.deltaTime);
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _tongueEndPoint);
                }
                else
                {
                    _tongueEndPoint = Vector3.MoveTowards(_tongueEndPoint, _tongueStartPoint.position, _tongueExtendSpeed * Time.deltaTime);
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _tongueEndPoint);
                }

                if (_tongueEndPoint == _tongueStartPoint.position)
                {
                    if (_grapeObjects.Count > 0 && _isCollectingFinishSuccess)
                    {
                        OnCollectSuccessed();
                    }
                    _tongueMoveFinished = true;
                    _isProcessing = false;
                    _lineRenderer.enabled = false;

                    ResetTongue();
                }
            }

            yield return new WaitForSeconds(1 / 1200f);
        }
    }





    private void OnCollectSuccessed()
    {
        //TODO: ADD HITABLE INTERFACE GRRAPE AND ARROW OBJECT. THEN, COLLECT AND CONTROL THEM IN ONE PLACE: O(N) + O(N) -> O(N)
        foreach (GameObject obj in _grapeObjects)
        {
            Debug.Log("Collected: " + obj.name);

            GrapeManager grapeManager = obj.GetComponent<GrapeManager>();

            SubCellManager grapeSubCell = grapeManager.GetSubCellBelonging();

            //I am destroying top subcell. I dont use cell pool because subcells is instantiating only once in game

            grapeSubCell.gridObjectItem.ResetSubCellID();

            GrapePoolManager.Instance.ReturnGrapeObject(grapeManager);
            Destroy(grapeSubCell.gameObject);
            LevelManager.Instance.ReduceCellAmount();
        }

        foreach (GameObject obj in _arrowObjects)
        {
            ArrowManager arrowManager = obj.GetComponent<ArrowManager>();
            SubCellManager grapeSubCell = arrowManager.GetSubCellBelonging();
            grapeSubCell.gridObjectItem.ResetSubCellID();
            ArrowPoolManager.Instance.ReturnArrowObject(arrowManager);
            Destroy(grapeSubCell.gameObject);
            LevelManager.Instance.ReduceCellAmount();
        }

        SubCellManager frogSubCell = _frogManager.GetSubCellBelonging();
        frogSubCell.gridObjectItem.ResetSubCellID();
        FrogPoolManager.Instance.ReturnFrogObject(_frogManager);
        Destroy(frogSubCell.gameObject);
        LevelManager.Instance.ReduceCellAmount();
    }

    private void ResetTongue()
    {
        _isReturning = false;
        _currentTongueLength = 0f;
        _grapeObjects.Clear();
        _arrowObjects.Clear();
        _lineRenderer.SetPosition(0, _tongueStartPoint.position);
        _lineRenderer.SetPosition(1, _tongueStartPoint.position);
    }
}
