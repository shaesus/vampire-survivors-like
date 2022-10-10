using UnityEngine;
using System.Collections;

public class HellBeastEnemy : Enemy
{
    private bool _isAttacking = false;

    private void Start()
    {
        _damageDelay = 0.8f;
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _isAttacking = true;
        yield return new WaitForSeconds(0.95f);
        _isAttacking = false;
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
                StartCoroutine(StartAttack());
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
