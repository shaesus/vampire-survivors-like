using UnityEngine;
using System.Collections;

public class HellBeastEnemy : Enemy
{
    private void Start()
    {
        _damageDelay = 0.8f;
    }

    private void StartAttack()
    {
        _isAttacking = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            if (_isAttacking == true && Time.time > _timeForNextDamage)
            {
                player.TakeDamage(Damage);
                _timeForNextDamage = Time.time + _damageDelay;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            if (_isAttacking == false)
            {
                _animator.SetTrigger("Attack");
            }
            else
            {
                player.TakeDamage(Damage);
            }
        }
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private new void OnCollisionStay2D(Collision2D collision)
    {

    }
}
