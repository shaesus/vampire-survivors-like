using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual void LvlUp()
    {
        Debug.Log("Weapon");
        return;
    }
}
