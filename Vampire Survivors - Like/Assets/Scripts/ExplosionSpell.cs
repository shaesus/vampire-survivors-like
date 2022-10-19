using UnityEngine;
using System.Linq;
using System.Collections;

public class ExplosionSpell : Spell
{
    public float Radius { get; set; } = 3f;
    public float Damage { get; set; } = 25f;
    public float Force { get; set; } = 50f;
    public float CastCooldown { get; set; } = 3f;

    private bool _canCast = true;

    private float _manaCost = 40f;

    private int _lvl = 1;

    public override void Cast()
    {
        if (_canCast && Player.Instance.CurrentMana >= _manaCost)
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

            Player.Instance.DecreaseMana(_manaCost);
        }
        else if (Player.Instance.CurrentMana < _manaCost)
        {
            Debug.Log("Not enough mana!");
        }
        else
        {
            Debug.Log("Cast CD!");
        }
    }

    private IEnumerator StartCooldown()
    {
        _canCast = false;
        yield return new WaitForSeconds(CastCooldown);
        _canCast = true;
    }

    public override void LvlUp()
    {
        _lvl++;
        Damage += 5f;
        Radius = 3 * Mathf.Pow(1.1f, _lvl - 1);
    }
}
