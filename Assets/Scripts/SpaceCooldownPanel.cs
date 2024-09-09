using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCooldownPanel : CooldownPanel
{
    private void Start()
    {
        PlayerCombat.OnSpaceSpellCast.AddListener((spell)=>StartCoroutine(StartCooldown(spell)));
    }
}
