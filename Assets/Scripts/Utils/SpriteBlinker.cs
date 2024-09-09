using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteBlinker
{
    public static event Action OnBlinkEnded;
    
    public static IEnumerator StartBlinkSprite(SpriteRenderer sr)
    {
        var color = sr.color;
        var defaultAlpha = color.a;
        color.a = 0f;
        sr.color = color;

        yield return new WaitForSeconds(0.2f);

        color.a = defaultAlpha;
        sr.color = color;

        OnBlinkEnded?.Invoke();
    }
}
