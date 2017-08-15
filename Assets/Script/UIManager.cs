using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainmenuUI;

    [SerializeField]
    GameObject gameplayUI;

    [SerializeField]
    Image imgTutorial;


    void Awake()
    {
    }

    void Update()
    {
        if (imgTutorial) {
            if (GameController.isGameStart) {
                imgTutorial.gameObject.SetActive(false);
            }
        }
    }
}
