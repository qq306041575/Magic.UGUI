using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchView : MonoBehaviour
{
    public void OnBirdClick()
    {
        SceneManager.LoadScene("Bird");
    }

    public void OnCircusClick()
    {
        SceneManager.LoadScene("Circus");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
