using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongueManager : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform tongueStartPoint;
    [SerializeField] private float maxTongueLength = 10f;
    [SerializeField] private float tongueExtendSpeed = 5f;
    [SerializeField] private LayerMask correctLayerMask;
    [SerializeField] private LayerMask incorrectLayerMask;

    private Vector3 tongueEndPoint;
    private float currentTongueLength;
    private bool _isProcessing = false;
    private bool isReturning = false;
    private bool _tongueMoveFinished = false;
    private List<GameObject> correctObjects = new List<GameObject>();

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
       /*  foreach (var item in correctObjects)
        {
            Debug.Log("correct objects name: " + item.name);
        }
        if (!isReturning)
        {
            // Dilin uzama yönü (kurbağanın baktığı yön)
            Vector3 direction = -transform.forward;
            tongueEndPoint += direction * tongueExtendSpeed * Time.deltaTime;
            currentTongueLength += tongueExtendSpeed * Time.deltaTime;

            // Raycast ile çarpışma kontrolü
            Debug.DrawRay(tongueEndPoint, direction, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(tongueEndPoint, direction, out hit, tongueExtendSpeed * Time.deltaTime))
            {
                // Doğru bir objeye çarptıysa
                if (((1 << hit.collider.gameObject.layer) & correctLayerMask) != 0)
                {
                    correctObjects.Add(hit.collider.gameObject);
                }
                // Yanlış bir objeye çarptıysa
                else if (((1 << hit.collider.gameObject.layer) & incorrectLayerMask) != 0)
                {
                    isReturning = true;
                }
            }

            // Maksimum uzunluğa ulaştıysa veya yanlış objeye çarptıysa geri dönmeye başla
            if (currentTongueLength >= maxTongueLength)
            {
                isReturning = true;
            }

            // Dilin bitiş noktasını güncelle
            lineRenderer.SetPosition(1, tongueEndPoint);
        }
        else
        {
            // Dil geri dönüyor
            tongueEndPoint = Vector3.MoveTowards(tongueEndPoint, tongueStartPoint.position, tongueExtendSpeed * Time.deltaTime);
            lineRenderer.SetPosition(1, tongueEndPoint);

            if (tongueEndPoint == tongueStartPoint.position)
            {
                // Eğer dil geri döndüyse ve doğru objelere çarptıysa onları topla
                if (correctObjects.Count > 0)
                {
                    CollectCorrectObjects();
                }

                // Her şeyi sıfırla
                ResetTongue();
            }
        } */
    }

    public IEnumerator HandleFrogTongueMove()
    {
        _tongueMoveFinished = false;
        lineRenderer.enabled = true;
        _isProcessing = true;

        while (!_tongueMoveFinished)
        {
            foreach (var item in correctObjects)
            {
                Debug.Log("correct objects name: " + item.name);
            }
            if (!isReturning)
            {
                // Dilin uzama yönü (kurbağanın baktığı yön)
                Vector3 direction = -transform.forward;
                tongueEndPoint += direction * tongueExtendSpeed * Time.deltaTime;
                currentTongueLength += tongueExtendSpeed * Time.deltaTime;

                // Raycast ile çarpışma kontrolü
                Debug.DrawRay(tongueEndPoint, direction, Color.blue);
                RaycastHit hit;
                if (Physics.Raycast(tongueEndPoint, direction, out hit, tongueExtendSpeed * Time.deltaTime))
                {
                    // Doğru bir objeye çarptıysa
                    if (((1 << hit.collider.gameObject.layer) & correctLayerMask) != 0)
                    {
                        correctObjects.Add(hit.collider.gameObject);
                    }
                    // Yanlış bir objeye çarptıysa
                    else if (((1 << hit.collider.gameObject.layer) & incorrectLayerMask) != 0)
                    {
                        isReturning = true;
                    }
                }

                // Maksimum uzunluğa ulaştıysa veya yanlış objeye çarptıysa geri dönmeye başla
                if (currentTongueLength >= maxTongueLength)
                {
                    isReturning = true;
                }

                // Dilin bitiş noktasını güncelle
                lineRenderer.SetPosition(1, tongueEndPoint);
            }
            else
            {
                // Dil geri dönüyor
                tongueEndPoint = Vector3.MoveTowards(tongueEndPoint, tongueStartPoint.position, tongueExtendSpeed * Time.deltaTime);
                lineRenderer.SetPosition(1, tongueEndPoint);

                if (tongueEndPoint == tongueStartPoint.position)
                {
                    // Eğer dil geri döndüyse ve doğru objelere çarptıysa onları topla
                    if (correctObjects.Count > 0)
                    {
                        CollectCorrectObjects();
                    }
                    _tongueMoveFinished = true;
                    _isProcessing = false;
                    lineRenderer.enabled = false;
                    // Her şeyi sıfırla
                    ResetTongue();
                }
            }

            yield return null;
        }
    }

    // Doğru objeleri toplama işlemi
    private void CollectCorrectObjects()
    {
        foreach (GameObject obj in correctObjects)
        {
            // Burada objelerle ne yapacağını belirt
            Debug.Log("Collected: " + obj.name);
            // Örnek: objeyi sahneden sil
            Destroy(obj);
        }
    }

    // Dili sıfırlama işlemi
    private void ResetTongue()
    {
        isReturning = false;
        currentTongueLength = 0f;
        correctObjects.Clear();
        lineRenderer.SetPosition(0, tongueStartPoint.position);
        lineRenderer.SetPosition(1, tongueStartPoint.position);
    }
}
