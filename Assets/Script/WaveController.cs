using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    const int MAX_WAVE_PATTERN = 2;


    [SerializeField]
    CrowdController crowdController;

    [SerializeField]
    PlayerController player;

    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    GameController _gameController;


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
    /* string _previousPerformance; */

    bool _isNextWavePattern;


    public WaveController()
    {
        _patternQueueIndex = new int[50];
        _currentWavePatternIndex = 0;
        _isNextWavePattern = false;
        _nextRowTime = 0.5f;
        _handDownTime = 0.4f;
        _startTime = 0.0f;
        _badTime_1 = 0.0f;
        _goodTime_1 = 0.0f;
        _perfectTime = 0.0f;
        _badTime_2 = 0.0f;
        _goodTime_2 = 0.0f;
        _endedTime = 0.0f;
        _performance = "";
        /* _previousPerformance = ""; */
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
        if (!_gameController.IsGameOver) {
            _HandleWavePattern();
        }
	}

    void _GenerateWavePattern()
    {
        for (int i = 0; i < _patternQueueIndex.Length; i++) {
            var randNum = Random.Range(0.0f, 1.0f);
            var result = (randNum < 0.6f) ? 1 : 2;
            _patternQueueIndex[i] = result;
        }
    }

    void _HandleWavePattern()
    {
        if (_gameController.IsGameStart) {
            if (_isNextWavePattern) {
                _nextRowTime = ((_nextRowTime - 0.015f)> 0.18f) ? _nextRowTime - 0.015f : 0.18f;
                _handDownTime = ((_handDownTime - 0.015f) > 0.08f) ? _handDownTime - 0.015f : 0.08f;
            }
        }

        if (_gameController.IsGameInit) {
            
            var isNotPerfect = _performance == "Miss" || _performance == "Bad" || _performance == "Good";

            if (isNotPerfect) {
                player.ClearPerfectStack();
            }

            if (_isNextWavePattern) {
                switch (_patternQueueIndex[_currentWavePatternIndex]) {
                    case 1:
                        StartCoroutine("_WaveUpToDown");
                    break;

                    case 2:
                        StartCoroutine("_WaveLeftToRight");
                    break;

                    default:
                        StartCoroutine("_WaveUpToDown");
                    break;
                }

                if ((_currentWavePatternIndex + 1) > (_patternQueueIndex.Length - 1)) {
                    _currentWavePatternIndex = 0;
                } else {
                    _currentWavePatternIndex++;
                }

                _isNextWavePattern = false;
            }
        }
    }

    IEnumerator _WaveUpToDown()
    {
        var roundCount = 1;
        var posRow = 0;
        
        int[] posListCol = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        for (int i = 0; i < 5; i++) {

            for (int j = 0; j < posListCol.Length; j++) {

                if (_performance == "Miss") {

                    if (crowdController.SpecialCrowdPos.Contains(new Vector2(i, j))) {
                        continue;
                    }
                }

                var currentCrowd = crowdController.CrowdObjects[posRow][j].GetComponent<Crowd>();
                currentCrowd.HandUp(_handDownTime);
            }

            if (roundCount == 3) {

                _perfectTime = Time.timeSinceLevelLoad;
                _startTime = _perfectTime - _handDownTime;
                _endedTime = _perfectTime + _handDownTime;

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

        _performance = "";
        _isNextWavePattern = true;
    }

    IEnumerator _WaveLeftToRight()
    {
        var roundCount = 1;
        var posCol = 0;
        
        int[] posListRow = { 0, 1, 2, 3, 4 };

        for (int i = 0; i < 9; i++) {

            for (int j = 0; j < posListRow.Length; j++) {

                if (_performance == "Miss") {

                    if (crowdController.SpecialCrowdPos.Contains(new Vector2(j, i))) {
                        continue;
                    }
                }

                var currentCrowd = crowdController.CrowdObjects[j][i].GetComponent<Crowd>();
                currentCrowd.HandUp(_handDownTime);
            }

            if (roundCount == 5) {

                _perfectTime = Time.timeSinceLevelLoad;
                _startTime = _perfectTime - _handDownTime;
                _endedTime = _perfectTime + _handDownTime;

                StartCoroutine("_CheckScore", _handDownTime);
            }

            roundCount++;
            posCol++;

            yield return new WaitForSeconds(_nextRowTime);
        }

        _performance = "";
        _isNextWavePattern = true;
    }

    IEnumerator _CheckScore(float delayCheck)
    {
        yield return new WaitForSeconds(delayCheck);
        var interval = (float)((_perfectTime - _startTime) / 2.5);

        _badTime_1 = _startTime + interval;
        _goodTime_1 = _startTime + (interval * 2);

        _goodTime_2 = _endedTime - (interval * 2);
        _badTime_2 = _endedTime - interval;

        if (player.WaveTime < _startTime || player.WaveTime > _endedTime) {
            _performance = "Miss";
            player.ClearPerfectStack();

            if (_gameController.IsGameStart) {
                player.RemoveHealth(1);
            }

        } else {
            if ((player.WaveTime > _startTime) && (player.WaveTime <= _badTime_1)) {
                _performance = "Bad";
                player.AddScore(5);
                player.ClearPerfectStack();

            } else if ((player.WaveTime > _badTime_1) && (player.WaveTime <= _goodTime_1)) {
                _performance = "Good";
                player.AddScore(10);
                player.ClearPerfectStack();

            } else if ((player.WaveTime > _goodTime_1) && (player.WaveTime <= _perfectTime)) {
                _performance = "Perfect";
                player.AddPerfectStack(1);

                var score = (player.PerfectStack > 1) ? player.PerfectStack * 15 : 15;
                player.AddScore(score);

            } else if ((player.WaveTime > _perfectTime) && (player.WaveTime <= _goodTime_2)) {
                _performance = "Perfect";
                player.AddPerfectStack(1);

                var score = (player.PerfectStack > 1) ? player.PerfectStack * 15 : 15;
                player.AddScore(score);

            } else if ((player.WaveTime > _goodTime_2) && (player.WaveTime <= _badTime_2)) {
                _performance = "Good";
                player.AddScore(10);
                player.ClearPerfectStack();

            } else if ((player.WaveTime > _badTime_2) && (player.WaveTime <= _endedTime)) {
                _performance = "Bad";
                player.AddScore(5);
                player.ClearPerfectStack();
            }
        }

        var isFailed = _performance == "Miss" || _performance == "Bad";
        player.SetFailed(isFailed);

        var hideDelay = _handDownTime < 0.2f ? 0.2f : _handDownTime;
        uiManager.ShowPerformance(_performance,  hideDelay);

        if (_performance == "Miss") {
            foreach (GameObject obj in crowdController.SpecialCrowdObjects) {
                obj.GetComponent<SpecialCrowd>().Blame();
            }
        } 
    }
}
