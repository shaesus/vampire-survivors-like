using System.Collections;
using UnityEngine;

public class DemonEnemy : Enemy
{
    [SerializeField] private Vector3 offset;

    private void StartAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }

    private new void Update()
    {
        if (Physics2D.Raycast(transform.position + offset, Vector2.left * transform.localScale.x,
                _attackRange, LayerMask.GetMask("Player")))
        {
            StartAttack();
        }

        Rotate();
    }

    private void Attack()
    {
        var pointA = new Vector2(transform.position.x - 1.228f * transform.localScale.x,
            transform.position.y - 0.6f);
        var pointB = new Vector2(transform.position.x - 4.13f * transform.localScale.x,
            transform.position.y - 2.03f);

        if (Physics2D.OverlapArea(pointA, pointB, LayerMask.GetMask("Player")))
        {
            Player.Instance.TakeDamage(damage);
        }
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        return;
    }

    private new void OnCollisionStay2D(Collision2D collision)
    {
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + offset, Vector3.left * transform.localScale.x * _attackRange);
    }
}
