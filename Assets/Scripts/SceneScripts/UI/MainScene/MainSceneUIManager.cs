using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    public void StartButton()
    {
        EventSystem.ChangeScene?.Invoke(1);
    }
}
