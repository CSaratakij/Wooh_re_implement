﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    CrowdController crowdController;

    [SerializeField]
    PlayerController player;


    public string Performance { get { return _performance; } }


    int[] _patternQueueIndex;
    int _currentWavePatternIndex;

    float _increaseSpeedPerWave;

    float _nextRowTime;
    float _handDownTime;

    float _startTime;
    float _badTime_1;
    float _goodTime_1;
    float _perfectTime;
    float _badTime_2;
    float _goodTime_2;
    float _endedTime;

    string _performance;

    bool _isNextWavePattern;
    bool _isWaveEnded;


    public WaveController()
    {
        _patternQueueIndex = new int[100];
        _currentWavePatternIndex = 0;
        _isNextWavePattern = false;
        _isWaveEnded = false;
        _nextRowTime = 0.6f;
        _handDownTime = 0.5f;
        _startTime = 0.0f;
        _badTime_1 = 0.0f;
        _goodTime_1 = 0.0f;
        _perfectTime = 0.0f;
        _badTime_2 = 0.0f;
        _goodTime_2 = 0.0f;
        _endedTime = 0.0f;
    }


    void Awake()
    {
        _GenerateWavePattern();
    }

    void Start()
    {
        _isNextWavePattern = true;
    }

	void Update()
    {
        if (player.Score == 45) {
            GameController.isGameStart = true;
        }

        _HandleWavePattern();
	}

    void _GenerateWavePattern()
    {
        for (int i = 0; i < _patternQueueIndex.Length; i++) {
            //Random pattern index here..
        }
    }

    void _HandleWavePattern()
    {
        if (GameController.isGameStart) {

            if (_isNextWavePattern) {
                _nextRowTime = ((_nextRowTime - 0.015f)> 0.1f) ? _nextRowTime - 0.015f : 0.1f;
                _handDownTime = ((_handDownTime - 0.015f) > 0.1f) ? _handDownTime - 0.015f : 0.1f;
            }

            // if player is miss, don't make spcial crowd to wave
            // maybe move this to the wave itself..
            if (_performance == "Miss") {

            }

        }

        // Start wave the character here...
        if (GameController.isGameInit) {
            if (_isNextWavePattern) {
                StartCoroutine("_WaveUpToDown");
                _isNextWavePattern = false;
            }
        }
    }

    //Need coroutine plz
    IEnumerator _WaveUpToDown()
    {
        var roundCount = 1;
        var posRow = 0;

        int[] posListCol = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < posListCol.Length; j++) {
                var currentCrowd = crowdController.CrowdObjects[posRow][j].GetComponent<Crowd>();
                currentCrowd.HandUp(_handDownTime);
            }

            _perfectTime = Time.timeSinceLevelLoad;
            _startTime = _perfectTime - _handDownTime;
            _endedTime = _perfectTime + _handDownTime;

            if (roundCount == 3) {
                StartCoroutine("_CheckScore", _handDownTime);
            }

            var nextFirstCol = posListCol[posListCol.Length - 1] + 1;

            for (int k = 0; k < posListCol.Length; k++) {
                posListCol[k] = nextFirstCol + k;
            }

            roundCount++;
            posRow++;

            yield return new WaitForSeconds(_nextRowTime);
        }

        _isNextWavePattern = true;
    }

    IEnumerator _CheckScore(float delayCheck)
    {
        yield return new WaitForSeconds(delayCheck);
        Debug.Log("Checked..");


        //change c to perfectInvertal and normalInterval after use 2.5f
        var c = (_perfectTime - _startTime) / 3;

        _badTime_1 = _startTime + c;
        _goodTime_1 = _startTime + (c * 2);

        _goodTime_2 = _perfectTime + c;
        _badTime_2 = _perfectTime + (c * 2);

        if (player.WaveTime < _startTime || player.WaveTime > _endedTime) {
            _performance = "Miss";
            // remote player health here..

        } else {
            if ((player.WaveTime > _startTime) && (player.WaveTime <= _badTime_1)) {
                _performance = "Bad";
                player.AddScore(5);

            } else if ((player.WaveTime > _badTime_1) && (player.WaveTime <= _goodTime_1)) {
                _performance = "Good";
                player.AddScore(10);

            } else if ((player.WaveTime > _goodTime_1) && (player.WaveTime <= _perfectTime)) {
                _performance = "Perfect";
                player.AddScore(15);

            } else if ((player.WaveTime > _perfectTime) && (player.WaveTime <= _goodTime_2)) {
                _performance = "Perfect";
                player.AddScore(15);

            } else if ((player.WaveTime > _goodTime_2) && (player.WaveTime <= _badTime_2)) {
                _performance = "Good";
                player.AddScore(10);

            } else if ((player.WaveTime > _badTime_2) && (player.WaveTime <= _endedTime)) {
                _performance = "Bad";
                player.AddScore(5);
            }
        }
    }
}
