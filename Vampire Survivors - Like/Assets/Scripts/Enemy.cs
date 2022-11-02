using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [HideInInspector] protected UnityEvent OnEnemyDie = new UnityEvent();

    [SerializeField] protected float damage = 25f;

    public bool IsDead { get; protected set; } = false;

    [SerializeField] protected Animator _animator;

    [SerializeField] protected int _directionCoefficient = 1;

    [SerializeField] protected float _attackRange = 0f;
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected float _deathTime = 0f;
    [SerializeField] protected float _xpForKill = 5f;

    [SerializeField] protected SpriteRenderer _spriteRenderer;

    protected float _currentHealth;
    protected float _timeForNextDamage = 0f;
    protected float _damageDelay = 2f;
    protected float _pickupSpawnChance = 0.1f;

    protected Vector2 _movement;

    protected bool _isAttacking = false;
    protected bool _isBlinking = false;

    protected void Awake()
    {
        _currentHealth = _maxHealth;

        OnEnemyDie.AddListener(SpawnPickup);
        OnEnemyDie.AddListener(Die);
    }

    protected void Update()
    {
        Rotate();
    }

    private void SetIsAttackingFalse()
    {
        _isAttacking = false;
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

    public virtual void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " tookDamage!");

        if (IsDead == false)
        {
            StartCoroutine(StartBlinkSprite());
        }

        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            OnEnemyDie.Invoke();
        }
    }

    private IEnumerator StartBlinkSprite()
    {
        if (_isBlinking == false)
        {
            _isBlinking = true;
            var color = _spriteRenderer.color;
            var defaultAlpha = color.a;
            color.a = 0f;
            _spriteRenderer.color = color;

            yield return new WaitForSeconds(0.2f);

            color.a = defaultAlpha;
            _spriteRenderer.color = color;
            _isBlinking = false;
        }
    }

    public virtual void Die()
    {
        if (IsDead == false)
        {
            GameManager.Instance.IncrementScore();
            Player.Instance.IncreaseXP(_xpForKill);
        }

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
            playerComp.TakeDamage(damage);
            _timeForNextDamage = Time.time + _damageDelay;
        } //Take damage as enemy touches player
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player playerComp) && Time.time > _timeForNextDamage)
        {
            playerComp.TakeDamage(damage);
            _timeForNextDamage = Time.time + _damageDelay;
        } //Take damage multiple times as enemy touches player
    }

    protected void SpawnPickup()
    {
        if (Random.value < 1f - _pickupSpawnChance)
        {
            return;
        }

        var gameManager = GameManager.Instance;
        
        if (gameManager.SpellPickups.Count == 0 && gameManager.WeaponPickups.Count == 0)
        {
            return;
        }

        var pickups = gameManager.WeaponPickups.Concat(gameManager.SpellPickups).ToList();
        var randomPickup = pickups[Random.Range(0, pickups.Count)];

        Instantiate(randomPickup, transform.position, Quaternion.identity);

        if (randomPickup.TryGetComponent<SpellPickup>(out var spellPickup))
        {
            gameManager.SpellPickups.Remove(randomPickup);
        }
        else if (randomPickup.TryGetComponent<Pickup>(out var weaponPickup))
        {
            gameManager.WeaponPickups.Remove(randomPickup);
        }
    }
}
