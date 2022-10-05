using System.Collections;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject Prefab { get; set; }

    public float ShootCd { get; set; } = 1f;

    private bool _canShoot = true;

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
                playerCombat.ShootPoint.transform.rotation);
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(playerCombat.LookDirection * projectile.GetComponent<Projectile>().Speed,
                ForceMode2D.Impulse);
            Destroy(projectile, 3f);
            StartCoroutine(ShootCooldown());
        }
        else
        {
            Debug.Log("Shoot CD!");
        }
    }
}
