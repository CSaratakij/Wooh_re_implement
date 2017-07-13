using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] sprites;


    enum AnimationState
    {
        Idle,
        HandUp
    }

    protected SpriteRenderer _renderer;


    public Crowd()
    {
        sprites = new Sprite[2];
	}

    public void Idle()
    {
        _renderer.sprite = sprites[(int)AnimationState.Idle];
    }

    public void HandUp()
    {
        _renderer.sprite = sprites[(int)AnimationState.HandUp];
    }

    public void Hide()
    {
        _renderer.sprite = null;
    }

	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
	}
}
