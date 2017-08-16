using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Sprite[] _animationSprite;

    public int Score { get { return _score; } }
    public PlayerState State { get { return _state; } }
    public float WaveTime { get { return _waveTime; } }


    public enum PlayerState
    {
        Idle,
        Perfect,
        Good,
        Bad,
        Miss
    }

    int _score;
    float _waveTime;

    PlayerState _state;

    SpriteRenderer _renderer;


    public PlayerController()
    {
        _score = 0;
        _waveTime = 0.0f;
        _state = PlayerState.Idle;
        _animationSprite = new Sprite[5];
    }

    public void AddScore(int value)
    {
        _score += value;
    }

	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
	}

	void Start()
    {
        _renderer.sprite = _animationSprite[(int)PlayerState.Idle];
	}
	
	void Update()
    {
        if (GameController.isGameInit && !GameController.isGameOver) {
            var isHandUp = false;
            var isHandDown = false;

            var totalTouch = Input.touchCount;

            if (totalTouch > 0) {
               foreach (Touch touch in Input.touches) {
                   switch (touch.phase) {
                   case TouchPhase.Began:
                       isHandUp = true;
                       isHandDown = false;
                       break;

                   case TouchPhase.Ended:
                       isHandUp = false;
                       isHandDown = true;
                       break;

                   default:
                       break;
                   }
               }

            } else {
                isHandUp = Input.GetButtonDown("HandUp");
                isHandDown = Input.GetButtonUp("HandUp");
            }

            if (isHandUp) {
                _renderer.sprite = _animationSprite[(int)PlayerState.Perfect];
                _waveTime = Time.timeSinceLevelLoad;
                isHandUp = false;

            } else if (isHandDown) {
                _renderer.sprite = _animationSprite[(int)PlayerState.Idle];
                isHandDown = false;
            }
        }
    }
}
