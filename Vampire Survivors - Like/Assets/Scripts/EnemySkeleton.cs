using System.Collections;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    private bool _isAttacking = false;

    private new void Update()
    {
        if (IsDead == false)
        {
            if (Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x,
                _attackRange, LayerMask.GetMask("Player")) && _isAttacking == false)
            {
                StartCoroutine(StartAttack());
            }

            if (_isAttacking == false)
            {
                Rotate();
            }
        }
    }

    private IEnumerator StartAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        Attack();
        yield return new WaitForSeconds(1.1f);
        _isAttacking = false;
    }

    private void Attack()
    {
        var pointA = new Vector2(transform.position.x + 0.2f * transform.localScale.x,
            transform.position.y + 0.9f);
        var pointB = new Vector2(transform.position.x + 1.8f * transform.localScale.x,
            transform.position.y - 0.9f);

        if (Physics2D.OverlapArea(pointA, pointB, LayerMask.GetMask("Player")))
        {
            Player.Instance.TakeDamage(Damage);
        }
    }

    private new void FixedUpdate()
    {
        if (IsDead == false && _isAttacking == false)
        {
            Move();
        }
    }

    private new void Die()
    {
        Debug.Log(gameObject.name + " died!1");

        IsDead = true;

        _animator.SetBool("IsDead", IsDead);
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void ComeAlive(float reviveHpCoefficient)
    {
        IsDead = false;
        _animator.SetBool("IsDead", IsDead);
        _currentHealth = reviveHpCoefficient * _maxHealth;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public new void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " tookDamage!");

        if (_isAttacking == false)
        {
            _animator.SetTrigger("TookDamage");
        }

        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _attackRange
            * transform.localScale.x);
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private new void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}