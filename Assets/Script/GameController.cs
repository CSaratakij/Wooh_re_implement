using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGameInit = false;
    public static bool isGameStart = false;

    
    public void GameInit()
    {
        GameController.isGameInit = true;
    }
}
