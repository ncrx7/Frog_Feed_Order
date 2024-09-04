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
    private HashSet<GameObject> _hittableObjects = new HashSet<GameObject>();
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
        if (_isProcessing)
            return;

        StartCoroutine(HandleFrogTongueMoveCoroutine());
        ClickManager.Instance.ReduceCickAmount();
    }

    //TODO: USE UNITASK INSTEAD OF COROUTINE TO CREATE FASTER ASYNC CODE SNAPPETS
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
                TongueForward();
            }
            else
            {
                TongueBack();
            }

            yield return new WaitForSeconds(1 / 1200f);
        }
    }

    private void TongueForward()
    {
        _tongueEndPoint += direction * _tongueExtendSpeed * Time.deltaTime;
        _currentTongueLength += _tongueExtendSpeed * Time.deltaTime;
        tonguePath.Add(_tongueEndPoint);

        Debug.DrawRay(_tongueEndPoint, direction, Color.blue);

        float sphereRadius = 0.1f;
        RaycastHit[] hits = Physics.SphereCastAll(_tongueEndPoint, sphereRadius, direction, _tongueExtendSpeed * Time.deltaTime);

        foreach (var hit in hits)
        {
            //TODO: CREATE GRAPEMANAGER AND ARROW MANAGET IN A BASE CLASS AND AVOID CODE REPEATING AND MEMEROY ALLOCATION
            if (hit.collider.TryGetComponent<IHittable>(out IHittable hittableObject))
            {
                if (_frogManager.GetSubCellBelonging().SubCellColorType == hittableObject.GetSubCellManager().SubCellColorType)
                {
                    hittableObject.Hit(_frogManager, ref direction, _hittableObjects, _lineRenderer, tonguePath, _tongueEndPoint);
                }
                else
                {
                    _isReturning = true;
                    _isCollectingFinishSuccess = false;
                    break;
                }
            }
        }

        CheckTongueLenght();
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _tongueEndPoint);
    }

    private void TongueBack()
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
            if (_hittableObjects.Count > 0 && _isCollectingFinishSuccess)
            {
                OnCollectSuccessed();
            }
            _tongueMoveFinished = true;
            _isProcessing = false;
            _lineRenderer.enabled = false;

            ResetTongue();
        }
    }

    private void CheckTongueLenght()
    {
        if (_currentTongueLength >= _maxTongueLength)
        {
            _isReturning = true;
            _isCollectingFinishSuccess = true;
            foreach (var grape in _hittableObjects)
            {
                //grape.GetComponent<GrapeManager>().MoveToTarget(transform.position); //TODO: SET TONGUE SPEED TO MOVING OBJECT SPEED
                if (grape.TryGetComponent<GrapeManager>(out GrapeManager grapeManager))
                {
                    grapeManager.MoveToTarget(transform.position);
                }
            }
        }
    }

    private void OnCollectSuccessed()
    {
        HandleHittableObjectData();
        HandleFrogObjectData();
    }

    private void HandleHittableObjectData()
    {
        //TODO: ADD HITABLE INTERFACE GRRAPE AND ARROW OBJECT. THEN, COLLECT AND CONTROL THEM IN ONE PLACE: O(N) + O(N) -> O(N)
        foreach (GameObject obj in _hittableObjects)
        {
            Debug.Log("Collected: " + obj.name);

            if (obj.TryGetComponent<IHittable>(out IHittable hittableObjectManager))
            {
                SubCellManager grapeSubCell = hittableObjectManager.GetSubCellManager();
                grapeSubCell.gridObjectItem.ResetSubCellID();
                Destroy(grapeSubCell.gameObject);
                LevelManager.Instance.ReduceCellAmount();

                if (hittableObjectManager is GrapeManager grapeManager)
                {
                    GrapePoolManager.Instance.ReturnGrapeObject(grapeManager);
                }
                else if (hittableObjectManager is ArrowManager arrowManager)
                {
                    ArrowPoolManager.Instance.ReturnArrowObject(arrowManager);
                }
            }
            //I am destroying top subcell. I dont use cell pool because subcells is instantiating only once in game
        }
    }

    private void HandleFrogObjectData()
    {
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
        _hittableObjects.Clear();
        _lineRenderer.SetPosition(0, _tongueStartPoint.position);
        _lineRenderer.SetPosition(1, _tongueStartPoint.position);
    }
}
