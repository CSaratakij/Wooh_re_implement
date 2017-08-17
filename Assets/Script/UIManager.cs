using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    [SerializeField]
    Text txtScore;

    [SerializeField]
    Image imgTutorial;

    [SerializeField]
    WaveController waveController;

    [SerializeField]
    Sprite[] imgHealthStates;

    [SerializeField]
    Image[] imgHealths;

    [SerializeField]
    Image[] imgPerformanceStates;


    Image _currentPerformanceState;


    void Update()
    {
        if (player && txtScore) {
            if (GameController.isGameInit) {
                txtScore.text = player.Score.ToString();
            }
        }

        if (imgTutorial) {
            if (GameController.isGameStart) {
                imgTutorial.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < imgHealths.Length; i++) {
            if (player.Health >= (i + 1)) {
                imgHealths[i].sprite = imgHealthStates[0];
            } else {
                imgHealths[i].sprite = imgHealthStates[1];
            }
        }
    }

    public void ShowPerformance(string value, float hideDelay)
    {
        switch (value) {

            case "Miss":
                _currentPerformanceState = imgPerformanceStates[0];
            break;

            case "Bad":
                _currentPerformanceState = imgPerformanceStates[1];
            break;

            case "Good":
                _currentPerformanceState = imgPerformanceStates[2];
            break;

            case "Perfect":
                _currentPerformanceState = imgPerformanceStates[3];
            break;

            default:
            break;
        }

        foreach (Image img in imgPerformanceStates) {
            img.enabled = false;
        }

        _currentPerformanceState.enabled = true;
        StartCoroutine("_ShowPerformanceCallBack", hideDelay);
    }

    IEnumerator _ShowPerformanceCallBack(float hideDelay)
    {
        yield return new WaitForSeconds(hideDelay);
        _currentPerformanceState.enabled = false;
    }
}
