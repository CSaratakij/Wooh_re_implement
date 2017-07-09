using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRect : MonoBehaviour
{
    [SerializeField]
    Texture2D texture2D;

    [SerializeField]
    Rect rect;

    [SerializeField]
    [Range(0, 100)]
    float pixelPerUnit;

    [SerializeField]
    Vector2 pivot;


    Sprite _newSprite;


    public Sprite Sprite
    {
        get {
            return _newSprite;
        }
    }

    public SpriteRect()
    {
        pixelPerUnit = 100;
        pivot = new Vector2(0.5f, 0.5f);
        _newSprite = null;
    }

	void Awake()
    {
        _newSprite = Sprite.Create(texture2D, rect, pivot, pixelPerUnit);
	}
}
