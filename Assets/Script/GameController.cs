using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerController player;


    public bool IsGameInit { get { return _isGameInit; } }
    public bool IsGameStart { get { return _isGameStart; } }
    public bool IsGameOver { get { return _isGameOver; } }
    public bool IsGamePause { get { return _isGamePause; } }


    bool _isGameInit;
    bool _isGameStart;
    bool _isGameOver;
    bool _isGamePause;

    
    public GameController()
    {
        _isGameInit = false;
        _isGameStart = false;
        _isGameOver = true;
        _isGamePause = false;
    }
    
    public void GameInit()
    {
        _isGameOver = false;
        _isGameInit = true;
    }

    public void GameStart()
    {
        _isGameStart = true;
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void Reset()
    {
        _isGameInit = false;
        _isGameStart = false;
        _isGameOver = true;
    }

    public void SaveScore()
    {
        //save to leaderboard here (player pref..)
    }

    public void Restart()
    {
        Reset();
        StartCoroutine("_RestartCallBack");
    }

    public void TogglePause()
    {
        Time.timeScale = (Time.timeScale > 0.0f) ? 0.0f : 1.0f;
        _isGamePause = (Time.timeScale == 0.0f) ? true : false;
    }


    void Update()
    {
        if (player.Score >= 45) {
            GameStart();

            if (player.Health <= 0) {
                GameOver();
            }
        }
    }

    IEnumerator _RestartCallBack()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
