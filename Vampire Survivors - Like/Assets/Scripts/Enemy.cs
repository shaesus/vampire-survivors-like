using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float Damage = 25f;

    public bool IsDead { get; protected set; } = false;

    [SerializeField] protected Animator _animator;

    [SerializeField] protected int _directionCoefficient = 1;

    [SerializeField] protected float _attackRange = 0f;
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected float _deathTime = 0f;

    protected float _currentHealth;
    protected float _timeForNextDamage = 0f;
    protected float _damageDelay = 2f;

    protected Vector2 _movement;

    protected void Awake()
    {
        _currentHealth = _maxHealth;
    }

    protected void Update()
    {
        Rotate();
    }

    protected void Rotate()
    {
        _movement = (Player.Instance.transform.position - transform.position).normalized;
        if (_movement.x > 0)
        {
            transform.localScale = new Vector3(1 * _directionCoefficient, 1, 1);
        }
        else if (_movement.x <= 0)
        {
            transform.localScale = new Vector3(-1 * _directionCoefficient, 1, 1);
        }
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected void Move()
    {
        transform.Translate(_movement * _speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " tookDamage!");

        if (_animator != null)
        {
            _animator.SetTrigger("TookDamage");
            
        }

        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }

    protected void Die()
    {
        Debug.Log(gameObject.name + " died!");
        IsDead = true;

        if (_animator != null)
        {
            _animator.SetBool("IsDead", IsDead);
        }
        StartCoroutine(DeathDelay());
    }

    protected IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(_deathTime);
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player playerComp))
        {
            playerComp.TakeDamage(Damage);
            _timeForNextDamage = Time.time + _damageDelay;
        } //Take damage as enemy touches player
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player playerComp) && Time.time > _timeForNextDamage)
        {
            playerComp.TakeDamage(Damage);
            _timeForNextDamage = Time.time + _damageDelay;
        } //Take damage multiple times as enemy touches player
    }
}
