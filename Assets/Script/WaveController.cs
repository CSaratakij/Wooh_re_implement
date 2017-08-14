using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    CrowdController crowdController;

    
    //By column or row?
    enum WavePattern
    {
        None,
        LeftToRight,
        UpToDown,
    }

    WavePattern[] patternQueue;


    public WaveController()
    {
        patternQueue = new WavePattern[100];
    }


    void Awake()
    {
        _GenerateWavePattern();
    }

    void Start()
    {
        if (crowdController) {
            GameController.isGameStart = true;
        }
    }

	void Update()
    {
        if (GameController.isGameStart) {
            _HandleWavePattern();
        }
	}

    void _GenerateWavePattern()
    {
        for (int i = 0; i < patternQueue.Length; i++) {
            //Random pattern here..
        }
    }

    void _HandleWavePattern()
    {
        if (Input.GetKeyDown("space")) {
            _WaveLeftToRight();
        }
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
