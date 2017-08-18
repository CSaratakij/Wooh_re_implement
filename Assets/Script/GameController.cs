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


    bool _isGameInit = false;
    bool _isGameStart = false;
    bool _isGameOver = true;

    
    public GameController()
    {
        _isGameInit = false;
        _isGameStart = false;
        _isGameOver = true;
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

    public void SaveScore()
    {
        //save to leaderboard here (player pref..)
    }

    public void Restart()
    {
        _Reset();
        StartCoroutine("_RestartCallBack");
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

    void _Reset()
    {
        _isGameInit = false;
        _isGameStart = false;
        _isGameOver = true;
    }

    IEnumerator _RestartCallBack()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
