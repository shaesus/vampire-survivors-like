using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public Sprite SpellSprite { get; set; }

    public float ManaCost { get; protected set; }

    public string Name { get; protected set; }
    
    public bool CanCast { get; protected set; } = true;

    public float CastCooldown => _castCooldown;
    protected float _castCooldown;

    protected int _lvl = 1;

    public virtual void LvlUp() { }

    public virtual void Cast() { }

    public virtual string GetDescription()
    {
        return "";
    }
}
