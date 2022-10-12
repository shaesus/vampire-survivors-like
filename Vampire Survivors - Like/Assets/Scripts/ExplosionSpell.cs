using UnityEngine;
using System.Linq;
using System.Collections;

public class ExplosionSpell : MonoBehaviour
{
    public Sprite SpellSprite { get; set; }

    public float Radius { get; set; } = 5f;
    public float Damage { get; set; } = 35f;
    public float Force { get; set; } = 50f;
    public float CastCooldown { get; set; } = 3f;

    private bool _canCast = true;

    private float _manaCost = 40f;

    public void Cast()
    {
        if (_canCast && Player.Instance.CurrentMana >= _manaCost)
        {
            var explosion = Instantiate(Player.Instance.GetComponent<PlayerCombat>().ExplosionEffect,
                Player.Instance.transform.position, Quaternion.identity);
            Destroy(explosion, 1.2f);

            var enemies = Physics2D.OverlapCircleAll(Player.Instance.transform.position, Radius)
                .Where(obj => obj.TryGetComponent(out Enemy enemy))
                .Select(obj => obj.GetComponent<Enemy>());
            foreach (var enemy in enemies)
            {
                if (enemy.TryGetComponent<EnemySkeleton>(out var skeleton))
                {
                    skeleton.TakeDamage(Damage);
                }
                else if (enemy.TryGetComponent<NecromasterEnemy>(out var necromaster))
                {
                    necromaster.TakeDamage(Damage);
                }
                else
                {
                    enemy.TakeDamage(Damage);
                }
                enemy.gameObject.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position
                    - Player.Instance.transform.position).normalized * Force, ForceMode2D.Impulse);
            }
            StartCoroutine(StartCooldown());

            Player.Instance.DecreaseMana(_manaCost);
            //Need Animation!!!!!
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
}
