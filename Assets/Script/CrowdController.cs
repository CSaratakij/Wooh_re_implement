using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    [SerializeField]
    int column;

    [SerializeField]
    int row;

    [SerializeField]
    Vector2 startPoint;

    [SerializeField]
    Vector2 intervalOfSeat;

    [SerializeField]
    GameObject crowdObject;

    [SerializeField]
    GameObject[] crowds;

    [SerializeField]
    SpriteRect[] speacialSprite;

    [SerializeField]
    Vector2[] speacialSpritePos;

    [SerializeField]
    Vector2[] hideSpritePos;


    GameObject[][] _crowdObjects;


	void Awake()
    {
        _Initialize();
	}

	void Update()
    {
        _HandleWavePattern();
	}

    void _HandleWavePattern()
    {

    }

    void _Initialize()
    {
        _InitCrowdHolder();
        _RandomCrowd();
    }

    void _InitCrowdHolder()
    {
        _crowdObjects = new GameObject[row][];

        for (int i = 0; i < row; i++) {
            _crowdObjects[i] = new GameObject[column];
        }
    }

    void _RandomCrowd()
    {
        var currentPos = startPoint;
        var currentOrder = 0;

        for (int i = 0; i < row; i++) {
            for (int n = 0; n < column; n++) {

                var selectSpriteIndex = (int)Random.Range(0, crowds.Length);
                _crowdObjects[i][n] = Instantiate(crowds[selectSpriteIndex], transform);

                var currentCrowd = _crowdObjects[i][n].GetComponent<Crowd>();
                var currentCrowdSpriteRenderer = _crowdObjects[i][n].GetComponent<SpriteRenderer>();

                //need to fix this... also..
                //what special sprite in each pos?
                /* foreach (Vector2 pos in speacialSpritePos) { */
                    /* if (i == pos.x && n == pos.y) { */
                        /* currentCrowdSpriteRects[0] = speacialSprite[0]; */
                    /* } */
                /* } */

                currentCrowd.Idle();

                _crowdObjects[i][n].transform.position = currentPos;
                currentPos.x += intervalOfSeat.x;
                currentCrowdSpriteRenderer.sortingOrder = currentOrder;

                foreach (Vector2 pos in hideSpritePos) {
                    if (i == pos.x && n == pos.y) {
                        currentCrowd.Hide();
                    }
                }
            }

            currentPos.x = startPoint.x;
            currentPos.y -= intervalOfSeat.y;
            currentOrder += 1;
        }
    }
}
