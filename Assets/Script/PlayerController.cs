using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Sprite[] _animationSprite;

    public int Score { get { return _score; } }
    public int Health { get { return _health; } }
    public PlayerState State { get { return _state; } }
    public float WaveTime { get { return _waveTime; } }
    public bool IsPressedWave { get { return _isPressedWave; } }


    public enum PlayerState
    {
        Idle,
        Perfect,
        Good,
        Bad,
        Miss
    }

    int _score;
    int _health;

    float _waveTime;
    bool _isPressedWave;

    PlayerState _state;
    SpriteRenderer _renderer;


    public PlayerController()
    {
        _score = 0;
        _health = 5;
        _waveTime = 0.0f;
        _state = PlayerState.Idle;
        _animationSprite = new Sprite[5];
    }

    public void AddScore(int value)
    {
        _score += value;
    }

    public void RemoveHealth(int value)
    {
        _health = ((_health - value) > 0) ? _health - value : 0;
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
                _isPressedWave = true;
                _renderer.sprite = _animationSprite[(int)PlayerState.Perfect];
                _waveTime = Time.timeSinceLevelLoad;
                isHandUp = false;

            } else if (isHandDown) {
                _isPressedWave = false;
                _renderer.sprite = _animationSprite[(int)PlayerState.Idle];
                isHandDown = false;
            }
        }
    }
}
