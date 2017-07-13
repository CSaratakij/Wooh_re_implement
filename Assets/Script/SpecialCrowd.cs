using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCrowd : Crowd
{
    public SpecialCrowd()
    {
        sprites = new Sprite[3];
    }

    public void Blame()
    {
        _renderer.sprite = sprites[3];
    }
}
