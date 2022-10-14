using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private GameObject _fireballRotatingPrefab;
    [SerializeField] private GameObject _circleOfProjectiles;
    [SerializeField] private GameObject _projectileShooter;

    [SerializeField] private Sprite swordPlaceholder;
    [SerializeField] private Sprite shooterPlaceholder;
    [SerializeField] private Sprite circlePlaceholder;

    private void InitializeShooter(GameObject projectilePrefab, float shootCd)
    {
        var ps = Instantiate(_projectileShooter, transform.position, Quaternion.identity);
        ps.transform.parent = Player.Instance.transform;
        var psComp = ps.GetComponent<ProjectileShooter>();
        psComp.Prefab = projectilePrefab;
        psComp.ShootCd = shootCd;
        Player.Instance.GetComponent<PlayerCombat>().OnAttack.AddListener(psComp.Shoot);
        WeaponContainers.Instance.InitializeContainer(shooterPlaceholder, "Projectile Shooter",
            "Range", projectilePrefab.GetComponent<Projectile>().Damage);
    }

    private void InitializeCircle(GameObject rotatingPrefab, int count, float radius)
    {
        var rotatingWeapon = Player.Instance.gameObject.AddComponent<RotatingWeapon>();
        rotatingWeapon.Prefab = rotatingPrefab;
        rotatingWeapon.Count = count;
        rotatingWeapon.Radius = radius;
        rotatingWeapon.CirclePrefab = _circleOfProjectiles;

        WeaponContainers.Instance.InitializeContainer(circlePlaceholder, "Projectile Orbital",
            "Orbital", rotatingPrefab.GetComponent<Projectile>().Damage);
    }

    private void InitializeSword(float damage, float attackRange, float attackCooldown)
    {
        var sword = Player.Instance.gameObject.AddComponent<SwordWeapon>();
        sword.Damage = damage;
        sword.AttackRange = attackRange;
        sword.AttackCooldown = attackCooldown;
        Player.Instance.GetComponent<PlayerCombat>().OnAttack.AddListener(sword.Attack);
        WeaponContainers.Instance.InitializeContainer(swordPlaceholder, "Sword", "Melee", damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FBPickup"))
        {
            InitializeShooter(_fireballPrefab, 1f);
        }
        else if (collision.gameObject.CompareTag("FBRPickup"))
        {
            InitializeCircle(_fireballRotatingPrefab, 3, 2f);
        }
        else if (collision.gameObject.CompareTag("SwordPickup"))
        {
            InitializeSword(30f, 2f, 1f);
        }
    }
}
