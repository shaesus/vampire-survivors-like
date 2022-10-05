using UnityEngine;
using System.Linq;
using System.Collections;

public class SwordWeapon : MonoBehaviour
{
    public float Damage { get; set; } = 30f;
    public float Force { get; set; } = 10f;
    public float AttackCooldown { get; set; } = 1.5f;
    public float AttackRange { get; set; } = 2.4f;

    private bool _canAttack = true;

    private Vector2 _pointA;
    private Vector2 _pointB;

    private Vector3 _trailOffset;

    public void Attack()
    {
        if (_canAttack)
        {
            var localScaleX = Player.Instance.transform.localScale.x;
            _pointA = new Vector2(transform.position.x + 0.4f * localScaleX,
                transform.position.y + 0.3f);
            _pointB = new Vector2(transform.position.x + AttackRange * localScaleX,
                transform.position.y + -0.3f);

            var enemies = Physics2D.OverlapAreaAll(_pointA, _pointB)
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

            SpawnTrail(Player.Instance.GetComponent<PlayerCombat>().MeleeAttackTrail);

            StartCoroutine(StartCooldown());
            Debug.Log("Sword Attacked!");
        }
        else
        {
            Debug.Log("Attack CD!");
        }
    }

    private void SpawnTrail(GameObject attackTrail)
    {
        _trailOffset = new Vector3(1.75f * Player.Instance.transform.localScale.x, -0.3f, 0);
        var trail = Instantiate(attackTrail, Player.Instance.transform.position + _trailOffset,
            Quaternion.identity);
        trail.transform.localScale = Player.Instance.transform.localScale;
        Destroy(trail, 0.35f);
    }

    private IEnumerator StartCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        _canAttack = true;
    }
}
