using System.Collections;
using UnityEngine;

public class DemonEnemy : Enemy
{
    private bool _isAttacking = false;

    private IEnumerator StartAttack()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(0.915f);
        Attack();
        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
    }

    private void Attack()
    {
        var pointA = new Vector2(transform.position.x - 1.228f * transform.localScale.x,
            transform.position.y - 0.6f);
        var pointB = new Vector2(transform.position.x - 4.13f * transform.localScale.x,
            transform.position.y - 2.03f);

        if (Physics2D.OverlapArea(pointA, pointB, LayerMask.GetMask("Player")))
        {
            Player.Instance.TakeDamage(Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player) && _isAttacking == false)
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private new void OnCollisionStay2D(Collision2D collision)
    {

    }
}
