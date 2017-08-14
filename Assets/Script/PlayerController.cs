using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Sprite[] _animationSprite;


    enum AnimationState
    {
        Idle,
        Perfect,
        Good,
        Bad,
        Miss
    }

    SpriteRenderer _renderer;


    public PlayerController()
    {
        _animationSprite = new Sprite[5];
    }

	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
	}

	void Start()
    {
        _renderer.sprite = _animationSprite[(int)AnimationState.Idle];
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
                   isHandUp = true;
                   isHandDown = false;
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
            _renderer.sprite = _animationSprite[(int)AnimationState.Perfect];

        } else if (isHandDown) {
            _renderer.sprite = _animationSprite[(int)AnimationState.Idle];
        }
	}
}
