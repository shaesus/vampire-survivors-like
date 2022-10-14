using UnityEngine;

public class LvlUpButton : MonoBehaviour
{
    public void LvlUpAllWeapons()
    {
        var weapons = Player.Instance.GetComponents<Weapon>();
        foreach (var weapon in weapons)
        {
            weapon.LvlUp();
        }

        var childWeapons = Player.Instance.GetComponentsInChildren<Weapon>();
        foreach (var weapon in childWeapons)
        {
            weapon.LvlUp();
        }

        var spells = Player.Instance.GetComponents<Spell>();
        foreach (var spell in spells)
        {
            spell.LvlUp();
        }
    }
}
