using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public SpriteRect[] spriteRects;


    SpriteRenderer _renderer;


    enum AnimationState
    {
        Idle,
        HandUp
    }


    public Crowd()
    {
        spriteRects = new SpriteRect[2];
	}

    public void Idle()
    {
        _renderer.sprite = spriteRects[(int)AnimationState.Idle].Sprite;
    }

    public void HandUp()
    {
        _renderer.sprite = spriteRects[(int)AnimationState.HandUp].Sprite;
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
