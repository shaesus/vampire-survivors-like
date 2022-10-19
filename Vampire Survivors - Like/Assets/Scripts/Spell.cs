using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Sprite SpellSprite { get; set; }

    protected float _castCooldown;
    protected float _manaCost;

    protected bool _canCast = true;

    protected int _lvl = 1;

    public virtual void LvlUp() { }

    public virtual void Cast() { }
}
