using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    CrowdController crowdController;

    [SerializeField]
    WavePattern[] wavePatterns; // <- change this to just simple enum?


    public WavePattern CurrentWavePattern { get { return wavePatterns[_patternQueueIndex[_currentWavePatternIndex]]; } }

    int[] _patternQueueIndex;
    int _currentWavePatternIndex;

    float _increaseSpeedPerWave;
    float _nextRowTime;
    float _handDownTime;

    bool _isNextWavePattern;


    public WaveController()
    {
        _patternQueueIndex = new int[100];
        _currentWavePatternIndex = 0;
        _isNextWavePattern = false;
        _nextRowTime = 0.5f;
        _handDownTime = 0.4f;
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
        /* if (Input.GetButtonDown("HandUp")) { */
        /*     _nextRowTime = ((_nextRowTime - 0.015f)> 0.1f) ? _nextRowTime - 0.015f : 0.1f; */
        /*     _handDownTime = ((_handDownTime - 0.015f) > 0.1f) ? _handDownTime - 0.015f : 0.1f; */
        /* } */

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
        // Start wave the character here...
        if (GameController.isGameInit) {
            /* _WaveUpToDown(); */
            if (_isNextWavePattern) {
                StartCoroutine("_WaveUpToDown");
                _isNextWavePattern = false;
            }
        }

        if (GameController.isGameStart) {
            // if player is miss, don't make spcial crowd to wave
        }
    }

    //Need coroutine plz
    IEnumerator _WaveUpToDown()
    {
        Debug.Log("About to wave...UpToDown");
        var roundCount = 1;
        var posRow = 0;
        int[] posListCol = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        for (int i = 0; i < 5; i++) {

            for (int j = 0; j < posListCol.Length; j++) {
                var currentCrowd = crowdController.CrowdObjects[posRow][j].GetComponent<Crowd>();
                currentCrowd.HandUp(_handDownTime);
            }

            var nextFirstCol = posListCol[posListCol.Length - 1] + 1;

            for (int k = 0; k < posListCol.Length; k++) {
                posListCol[k] = nextFirstCol + k;
            }

            roundCount++;
            posRow++;
            yield return new WaitForSeconds(_nextRowTime);
        }

        //Testing
        //After finished -> next wave patter...
        _nextRowTime = ((_nextRowTime - 0.015f)> 0.1f) ? _nextRowTime - 0.015f : 0.1f;
        _handDownTime = ((_handDownTime - 0.015f) > 0.1f) ? _handDownTime - 0.015f : 0.1f;

        _isNextWavePattern = true;
/* var currentCrowd = crowdController.CrowdObjects[(int)pos.x][(int)pos.y].GetComponent<Crowd>(); */
/* currentCrowd.HandUp(0.5f); */

    }

    void _WaveLeftToRight()
    {
        Debug.Log("About to wave...");
        var pos = new Vector2(0, 0);

        for (int i = 0; i < crowdController.CrowdObjects.Length; i++) {
            pos.x = i;

            for (int j = 0; j < crowdController.CrowdObjects[i].Length; j++) {
                pos.y = j;

                var currentCrowd = crowdController.CrowdObjects[i][j].GetComponent<Crowd>();
                currentCrowd.HandUp(0.2f);
            }
        }
    }
}
