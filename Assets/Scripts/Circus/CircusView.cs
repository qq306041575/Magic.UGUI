using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircusView : MonoBehaviour
{
    BackgroundComponent _circusBackground;
    BarrierController _barrierController;
    JokerController _jokerController;
    GameObject _loseObject;
    Text _scoreText;
    bool _canJump;
    bool _isLose;
    int _coin;

    void Awake()
    {
        _circusBackground = transform.Find("CircusBackground").GetComponent<BackgroundComponent>();
        _barrierController = transform.Find("BarrierController").GetComponent<BarrierController>();
        _jokerController = transform.Find("JokerController").GetComponent<JokerController>();
        _loseObject = transform.Find("LosePanel").gameObject;
        _scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    void Update()
    {
        var go = _jokerController.GetColliderObject();
        if (go == null || _isLose)
        {
            return;
        }
        if ("FireCollider" == go.name)
        {
            LoseGame();
        }
        else if ("CoinCollider" == go.name)
        {
            AddCoin(go);
        }
        else if ("FloorCollider" == go.name)
        {
            _canJump = true;
        }
    }

    void LoseGame()
    {
        _isLose = true;
        _circusBackground.enabled = false;
        _barrierController.enabled = false;
        _jokerController.enabled = false;
        _loseObject.SetActive(true);
    }

    void AddCoin(GameObject go)
    {
        _coin++;
        if (0 == _coin % 20 && _coin < 150)
        {
            _barrierController.AddSpeed();
        }
        go.SetActive(false);
        _scoreText.text = string.Format("SCORE: {0}", _coin * 100);
    }

    public void OnMaskClick()
    {
        if (!_canJump || _isLose)
        {
            return;
        }
        _canJump = false;
        _jokerController.Jump();
    }

    public void OnReplayClick()
    {
        _circusBackground.enabled = true;
        _barrierController.enabled = true;
        _jokerController.enabled = true;
        _isLose = false;
        _coin = 0;
        _scoreText.text = string.Format("SCORE: {0}", _coin * 100);
        _loseObject.SetActive(false);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene(0);
    }
}
