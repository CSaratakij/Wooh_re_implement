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
        if (Input.GetButtonDown("HandUp")) {
            _renderer.sprite = _animationSprite[(int)AnimationState.Perfect];

        } else if (Input.GetButtonUp("HandUp")) {
            _renderer.sprite = _animationSprite[(int)AnimationState.Idle];
        }
	}
}
