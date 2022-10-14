using System.Collections;
using UnityEngine;

public class ProjectileShooter : Weapon
{
    public GameObject Prefab { get; set; }

    public float ShootCd { get; set; } = 1f;

    private bool _canShoot = true;

    private int _lvl = 1;

    private float _damage = 25f;

    private IEnumerator ShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(ShootCd);
        _canShoot = true;
    }

    public void Shoot()
    {
        var playerCombat = Player.Instance.GetComponent<PlayerCombat>();
        if (_canShoot)
        {
            var projectile = Instantiate(Prefab, (Vector2)transform.position + playerCombat.LookDirection,
                playerCombat.ShootPoint.transform.rotation).GetComponent<Projectile>();
            projectile.Damage = _damage;
            projectile.transform.localScale = Vector3.one * Mathf.Pow(1.1f, _lvl - 1);
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(playerCombat.LookDirection * projectile.Speed,
                ForceMode2D.Impulse);
            Destroy(projectile, 3f);
            StartCoroutine(ShootCooldown());
        }
        else
        {
            Debug.Log("Shoot CD!");
        }
    }

    public override void LvlUp()
    {
        _lvl++;
        _damage *= 1.1f;
    }
}
