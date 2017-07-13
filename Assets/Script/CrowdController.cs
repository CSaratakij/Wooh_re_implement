using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    GameObject[] crowds;

    [SerializeField]
    GameObject[] speacialCrowds;

    [SerializeField]
    Vector2[] specialCrowdPos;

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

                var pos = new Vector2(i, n);
                var selectCrowdIndex = (int)Random.Range(0, crowds.Length);

                if (specialCrowdPos.Contains(pos)) {
                    _crowdObjects[i][n] = Instantiate(speacialCrowds[0], transform);

                } else {
                    _crowdObjects[i][n] = Instantiate(crowds[selectCrowdIndex], transform);
                }

                var currentCrowd = _crowdObjects[i][n].GetComponent<Crowd>();
                var currentCrowdSpriteRenderer = _crowdObjects[i][n].GetComponent<SpriteRenderer>();

                currentCrowd.Idle();

                _crowdObjects[i][n].transform.position = currentPos;
                currentPos.x += intervalOfSeat.x;
                currentCrowdSpriteRenderer.sortingOrder = currentOrder;

                if (hideSpritePos.Contains(pos)) {
                    currentCrowd.Hide();
                }

            }

            currentPos.x = startPoint.x;
            currentPos.y -= intervalOfSeat.y;
            currentOrder += 1;
        }
    }

    void _HandleWavePattern()
    {

    }
}
