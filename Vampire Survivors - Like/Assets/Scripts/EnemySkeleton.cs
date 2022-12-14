using System.Collections;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    [SerializeField] private float _destroyCD = 5f;

    private float _timeToDestroy;
    private float _timeSinceDeath;

    private new void Update()
    {
        if (IsDead == false)
        {
            if (Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x,
                _attackRange, LayerMask.GetMask("Player")) && _isAttacking == false)
            {
                StartAttack();
            }

            if (_isAttacking == false)
            {
                Rotate();
            }
        }
    }

    private void StartAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }

    private void Attack()
    {
        var pointA = new Vector2(transform.position.x + 0.2f * transform.localScale.x,
            transform.position.y + 0.9f);
        var pointB = new Vector2(transform.position.x + 1.8f * transform.localScale.x,
            transform.position.y - 0.9f);

        if (Physics2D.OverlapArea(pointA, pointB, LayerMask.GetMask("Player")))
        {
            Player.Instance.TakeDamage(damage);
        }
    }

    private new void FixedUpdate()
    {
        if (IsDead == false && _isAttacking == false)
        {
            Move();
        }
    }

    public override void Die()
    {
        if (IsDead == false)
        {
            GameManager.Instance.IncrementScore();
            Player.Instance.IncreaseXP(_xpForKill);
        }

        Debug.Log(gameObject.name + " died!1");

        _timeSinceDeath = Time.time;
        _timeToDestroy = Time.time + _destroyCD;

        IsDead = true;

        _animator.SetBool("IsDead", IsDead);
        GetComponent<BoxCollider2D>().isTrigger = true;

        StartCoroutine(WaitForDestroy());
    }

    private IEnumerator WaitForDestroy()
    {
        while(IsDead)
        {
            if (_timeSinceDeath > _timeToDestroy)
            {
                Destroy(gameObject);
            }

            _timeSinceDeath += Time.deltaTime;
            yield return null;
        }
    }

    public void ComeAlive(float reviveHpCoefficient)
    { 
        IsDead = false;
        _animator.SetBool("IsDead", IsDead);
        _currentHealth = reviveHpCoefficient * _maxHealth;
        GetComponent<BoxCollider2D>().isTrigger = false;
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
