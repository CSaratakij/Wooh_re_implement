using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChecker : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    [SerializeField]
    WaveController waveController;


    void Update()
    {
        _CheckScore();
    }
    
    void _CheckScore()
    {
        if (GameController.isGameInit) {
            // typical add score to player. to 15 point each..

            if (player.Score > 45) {
                GameController.isGameStart = true;
            }

            if (GameController.isGameStart) {
                // if misss -> remove player's health
            }
        }
    }
}
