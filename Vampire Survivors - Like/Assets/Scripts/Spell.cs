using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public virtual void LvlUp() { }

    public virtual void Cast() { }
}
