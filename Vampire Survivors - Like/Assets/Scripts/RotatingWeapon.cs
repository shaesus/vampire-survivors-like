using System.Collections.Generic;
using UnityEngine;

public class RotatingWeapon : Weapon
{
    public int Count { get; set; } = 3;
    public int Level { get; private set; } = 1;

    public float Radius { get; set; } = 2f;

    public GameObject Prefab { get; set; }

    public GameObject CirclePrefab { get; set; }

    public List<GameObject> Projectiles { get; set; } = new List<GameObject>();

    private CircleOfProjectiles _circleOfProjectiles;

    private void Start()
    {
        _circleOfProjectiles = Instantiate(CirclePrefab, transform.position, Quaternion.identity)
            .GetComponent<CircleOfProjectiles>();
        _circleOfProjectiles.RotatingWeapon = this;

        _circleOfProjectiles.CreateCircle();
    }

    public override void LvlUp()
    {
        foreach (var projectile in Projectiles)
        {
            Destroy(projectile);
        }

        Projectiles.Clear();

        Count++;
        Radius += 0.8f;
        Level++;

        _circleOfProjectiles.CreateCircle();

        Debug.Log("COP");
    }
}
