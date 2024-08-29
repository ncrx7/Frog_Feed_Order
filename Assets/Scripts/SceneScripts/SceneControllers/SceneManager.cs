using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.ChangeScene += HandleChangingScene;
    }

    private void OnDisable()
    {
        EventSystem.ChangeScene -= HandleChangingScene;
    }

    private void HandleChangingScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
