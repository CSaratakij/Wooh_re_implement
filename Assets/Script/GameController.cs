using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    public static bool isGameInit = false;
    public static bool isGameStart = false;
    public static bool isGameOver = true;

    
    public void GameInit()
    {
        GameController.isGameOver = false;
        GameController.isGameInit = true;
    }


    void Update()
    {
        if (player.Score >= 45) {
            GameController.isGameStart = true;

            if (player.Health <= 0) {
                GameController.isGameOver = true;
            }
        }
    }
}
