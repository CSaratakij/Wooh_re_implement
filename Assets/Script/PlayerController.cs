using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Sprite[] _animationSprite;

    public int Score { get { return _score; } }
    public PlayerState State { get { return _state; } }


    public enum PlayerState
    {
        Idle,
        Perfect,
        Good,
        Bad,
        Miss
    }

    int _score;
    PlayerState _state;

    SpriteRenderer _renderer;


    public PlayerController()
    {
        _score = 0;
        _state = PlayerState.Idle;
        _animationSprite = new Sprite[5];
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

        } else if (isHandDown) {
            _renderer.sprite = _animationSprite[(int)PlayerState.Idle];
        }
	}
}
