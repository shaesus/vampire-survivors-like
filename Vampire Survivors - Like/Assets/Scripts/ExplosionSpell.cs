using UnityEngine;
using System.Linq;
using System.Collections;

public class ExplosionSpell : Spell
{
    public float Radius { get; set; } = 3f;
    public float Damage { get; set; } = 25f;
    public float Force { get; set; } = 50f;

    private void Awake()
    {
        _castCooldown = 3f;
        ManaCost = 40f;
        Name = "Explosion Spell";
    }

    private void Start()
    {
        SpellSprite = HUD.Instance.ExplosionSpellSprite;
    }

    public override void Cast()
    {
        var explosion = Instantiate(Player.Instance.GetComponent<PlayerCombat>().ExplosionEffect,
            Player.Instance.transform.position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * Radius;
        Destroy(explosion, 1.2f);

        var enemies = Physics2D.OverlapCircleAll(Player.Instance.transform.position, Radius)
            .Where(obj => obj.TryGetComponent(out Enemy enemy))
            .Select(obj => obj.GetComponent<Enemy>());

        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(Damage);
            enemy.gameObject.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position
                - Player.Instance.transform.position).normalized * Force, ForceMode2D.Impulse);
        }

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        CanCast = false;
        yield return new WaitForSeconds(_castCooldown);
        CanCast = true;
    }

    public override void LvlUp()
    {
        _lvl++;
        Damage += 5f;
        Radius = 3 * Mathf.Pow(1.1f, _lvl - 1);
    }
}
