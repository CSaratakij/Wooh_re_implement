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
    Text txtPerformance;

    [SerializeField]
    Sprite[] imgHealthStates;

    [SerializeField]
    Image[] imgHealths;


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

        if (waveController && txtPerformance) {
            txtPerformance.text = waveController.Performance;
        }

        for (int i = 0; i < imgHealths.Length; i++) {
            if (player.Health >= (i + 1)) {
                imgHealths[i].sprite = imgHealthStates[0];
            } else {
                imgHealths[i].sprite = imgHealthStates[1];
            }
        }
    }
}
