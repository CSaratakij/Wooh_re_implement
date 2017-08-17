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
        Idle_Normal,
        Idle_Failed,
        HandUp_Normal,
        HandUp_Failed,
    }

    enum AnimationState
    {
        HandUp,
        HandDown
    }

    int _score;
    int _health;

    float _waveTime;

    bool _isPressedWave;
    bool _isFailed;

    PlayerState _state;
    AnimationState _animationState;

    SpriteRenderer _renderer;

    Sprite[] _currentAnimationSprite;


    public PlayerController()
    {
        _score = 0;
        _health = 5;
        _waveTime = 0.0f;
        _state = PlayerState.Idle_Normal;
        _animationState = AnimationState.HandDown;
        _animationSprite = new Sprite[5];
        _currentAnimationSprite = new Sprite[2];
        _isFailed = false;
    }

    public void AddScore(int value)
    {
        _score += value;
    }

    public void RemoveHealth(int value)
    {
        _health = ((_health - value) > 0) ? _health - value : 0;
    }

    public void SetFailed(bool value)
    {
        _isFailed = value;

        if (_isFailed) {
            _currentAnimationSprite[0] = _animationSprite[(int)PlayerState.Idle_Failed];
            _currentAnimationSprite[1] = _animationSprite[(int)PlayerState.HandUp_Failed];
        } else {
            _currentAnimationSprite[0] = _animationSprite[(int)PlayerState.Idle_Normal];
            _currentAnimationSprite[1] = _animationSprite[(int)PlayerState.HandUp_Normal];
        }
    }


	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
	}

	void Start()
    {
        _currentAnimationSprite[0] = _animationSprite[(int)PlayerState.Idle_Normal];
        _currentAnimationSprite[1] = _animationSprite[(int)PlayerState.HandUp_Normal];

        _renderer.sprite = _currentAnimationSprite[0];
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
                       _animationState = AnimationState.HandUp;
                       break;

                   case TouchPhase.Ended:
                       isHandUp = false;
                       isHandDown = true;
                       _animationState = AnimationState.HandDown;
                       break;

                   default:
                       break;
                   }
               }

            } else {
                if (Input.GetButton("HandUp")) {
                    _animationState = AnimationState.HandUp;

                } else {
                    _animationState = AnimationState.HandDown;
                }

                isHandUp = Input.GetButtonDown("HandUp");
                isHandDown = Input.GetButtonUp("HandUp");
            }

            if (isHandUp) {
                _isPressedWave = true;
                _waveTime = Time.timeSinceLevelLoad;
                isHandUp = false;

            } else if (isHandDown) {
                _isPressedWave = false;
                isHandDown = false;
            }

            if (_animationState == AnimationState.HandUp) {
                _renderer.sprite = _currentAnimationSprite[1];

            } else {
                _renderer.sprite = _currentAnimationSprite[0];
            }
        }
    }
}
