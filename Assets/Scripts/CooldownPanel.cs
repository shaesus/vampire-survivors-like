using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPanel : MonoBehaviour
{
    public Image image;

    protected IEnumerator StartCooldown(Spell spell)
    {
        var cooldownTime = spell.CastCooldown;
        image.fillAmount = 1f;
        
        while (true)
        {
            if (image.fillAmount <= 0)
            {
                yield break;
            }

            image.fillAmount -= 1f / cooldownTime * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
