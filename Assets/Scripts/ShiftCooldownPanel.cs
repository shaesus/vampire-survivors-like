using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCooldownPanel : CooldownPanel
{
    private void Start()
    {
        PlayerCombat.OnShiftSpellCast.AddListener((spell)=>StartCoroutine(StartCooldown(spell)));
    }
}
