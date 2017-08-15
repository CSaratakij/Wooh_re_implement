using System.Collections;
using System.Collections.Generic;
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
    }
}
