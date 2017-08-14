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

    public void HandUp(float endTime)
    {
        _renderer.sprite = sprites[(int)AnimationState.HandUp];
        StartCoroutine("_HandUpCallback", endTime);
    }

    public void Hide()
    {
        _renderer.sprite = null;
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i] = null;
        }
    }

    //Nightmapre..
    IEnumerator _HandUpCallback(float delay)
    {
        yield return new WaitForSeconds(delay);
        Idle();
    }

	void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
	}
}
