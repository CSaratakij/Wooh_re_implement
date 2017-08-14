using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    CrowdController crowdController;


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

    void _HandleWavePattern()
    {
        Debug.Log("About to wave...");
        var pos = new Vector2(0, 0);

        for (int i = 0; i < crowdController.CrowdObjects.Length; i++) {
            pos.x = i;

            for (int j = 0; j < crowdController.CrowdObjects[i].Length; j++) {
                pos.y = j;

                if (crowdController.HideSpritePos.Contains(pos)) {
                    continue;
                }

                var currentCrowd = crowdController.CrowdObjects[i][j].GetComponent<Crowd>();
                currentCrowd.HandUp();
            }
        }
    }
}
