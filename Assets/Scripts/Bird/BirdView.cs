using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BirdView : MonoBehaviour
{
    GameObject _loseObject;
    BackgroundComponent _skyBackground;
    BackgroundComponent _grassBackground;
    ColumnController _columnController;
    BirdController _birdController;
    Text _scoreText;
    int _score;
    bool _isLose;

    void Awake()
    {
        _loseObject = transform.Find("LosePanel").gameObject;
        _skyBackground = transform.Find("SkyBackground").GetComponent<BackgroundComponent>();
        _grassBackground = transform.Find("GrassBackground").GetComponent<BackgroundComponent>();
        _columnController = transform.Find("ColumnController").GetComponent<ColumnController>();
        _birdController = transform.Find("BirdController").GetComponent<BirdController>();
        _scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    void Update()
    {
        var go = _birdController.GetColliderObject();
        if (_isLose || go == null || "CeilCollider" == go.name)
        {
            return;
        }
        else if ("FloorCollider" == go.name || "ColumnCollider" == go.name)
        {
            LoseGame();
        }
        else if ("ScoreCollider" == go.name)
        {
            AddScore();
        }
    }

    void AddScore()
    {
        _score++;
        _scoreText.text = string.Format("SCORE: {0}", _score);
        if (0 == _score % 5 && _score < 31)
        {
            _columnController.AddSpeed();
        }
    }

    void LoseGame()
    {
        _isLose = true;
        _birdController.Die();
        _columnController.enabled = false;
        _skyBackground.enabled = false;
        _grassBackground.enabled = false;
        _loseObject.SetActive(true);
    }

    public void OnMaskClick()
    {
        if (!_isLose)
        {
            _birdController.Flap();
        }
    }

    public void OnReplayClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene(0);
    }
}
