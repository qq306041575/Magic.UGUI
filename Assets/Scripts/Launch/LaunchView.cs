using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchView : MonoBehaviour
{
    Transform _menuPanel;

    void Awake()
    {
        _menuPanel = transform.Find("MenuPanel");
        _menuPanel.localScale = Vector3.zero;
        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        var scale = _menuPanel.localScale;
        while (scale.x < 0.99f)
        {
            yield return new WaitForEndOfFrame();
            scale = Vector3.Lerp(scale, Vector3.one, 0.1f);
            _menuPanel.localScale = scale;
        }
        _menuPanel.localScale = Vector3.one;
    }

    IEnumerator ZoomOut(string scene)
    {
        var scale = _menuPanel.localScale;
        while (scale.x > 0.01f)
        {
            scale = Vector3.Lerp(scale, Vector3.zero, 0.2f);
            _menuPanel.localScale = scale;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(scene);
    }

    public void OnBirdClick()
    {
        StartCoroutine(ZoomOut("Bird"));
    }

    public void OnCircusClick()
    {
        StartCoroutine(ZoomOut("Circus"));
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
