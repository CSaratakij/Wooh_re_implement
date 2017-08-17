using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Remove this class..
public class WavePattern : MonoBehaviour
{
    [SerializeField]
    int roundToCalScore;


    enum Pattern
    {
        rowUpDown,
        colLeftRight
    }


    int _currentRound;

    float _startTime;
    float _currentTime;
    float _currentWaveLength;

    bool _isStart;
    bool _isEnded;

    
    public int CurrentRound { get { return _currentRound; } }
    public int RoundToCalScore { get { return roundToCalScore; } }
    public bool IsStart { get { return _isStart; } }
    public bool IsEnded { get { return _isEnded; } }


    public WavePattern()
    {
        _currentRound = 0;
        _startTime = 0.0f;
        _currentTime = 0.0f;
        _currentWaveLength = 1.0f;
        _isStart = false;
        _isEnded = false;
    }

    public void StartWave(GameObject[][] crowds) {
        _isEnded = false;
        _isStart = true;
        _startTime = Time.timeSinceLevelLoad;
        StartCoroutine("Wave(crowds)");
    }

    public void Wave(GameObject[][] crowds)
    {
        //state machine here..
        for (int i = 1; i <= roundToCalScore; i++) {

            if (i == roundToCalScore) {
                _currentTime = Time.timeSinceLevelLoad;

                var delta = _currentTime - _startTime;
                Debug.Log("Delta time: " + delta);
            }

            _currentRound++;
        }

        _isEnded = true;
        _isStart = false;
    }

    //Test only
    void Start()
    {
        _currentRound = 3;
    }

    void Update()
    {
        if (GameController.isGameInit && !_isStart && !_isEnded) {
            StartWave(new GameObject[1][]);
        }

        Debug.Log("Start time : " + _startTime);
        Debug.Log("Current time : " + _currentTime);
    }
}
