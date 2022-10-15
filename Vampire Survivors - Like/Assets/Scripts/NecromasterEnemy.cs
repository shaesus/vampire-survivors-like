using System.Collections;
using System.Linq;
using UnityEngine;

public class NecromasterEnemy : Enemy
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private float _reviveRadius = 5f;
    [SerializeField] private float _reviveCD = 7f;

    private Vector3 _directionTowardsThePlayer;
    private Vector3 _projectileOffset = new Vector3(-0.245f, -0.48f, 0);

    private void Start()
    {
        StartCoroutine(StartRevivingCycle());
    }

    private IEnumerator StartRevivingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_reviveCD);
            if (_isAttacking == false)
            {
                _animator.SetTrigger("Revive");
                yield return new WaitForSeconds(0.9f);
                Revive();
            }
        }
    }

    private new void Update()
    {
        _directionTowardsThePlayer = Player.Instance.transform.position - transform.position;
        var distanceToPlayer = _directionTowardsThePlayer.magnitude;

        if (distanceToPlayer <= _attackRange && _isAttacking == false)
        {
            StartAttack();
        }

        Rotate();
    }

    private new void FixedUpdate()
    {
        if (_isAttacking == false)
        {
            Move();
        }
    }

    private void Revive()
    {
        Debug.Log("Revive Invoked");
        var skeletons = Physics2D.OverlapCircleAll(transform.position, _reviveRadius)
            .Where(obj => obj.TryGetComponent(out EnemySkeleton enemy))
            .Select(obj => obj.GetComponent<EnemySkeleton>());
        Debug.Log(skeletons.Count());
        foreach (var skeleton in skeletons)
        {
            skeleton.ComeAlive(0.4f);
            Debug.Log("Revived");
        }  
    }

    private void StartAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }

    private void Attack()
    {
        var angle = Utilities.GetAngle(Vector2.right, _directionTowardsThePlayer);
        var projectile = Instantiate(_projectilePrefab, transform.position + _projectileOffset,
            Quaternion.Euler(0, 0, angle)).GetComponent<EnemyProjectile>();
        if (angle > 90 && angle < 270)
        {
            projectile.transform.localScale = new Vector3(1, -1, 1);
        }
        projectile.Damage = Damage;
        projectile.GetComponent<Rigidbody2D>().AddForce(_directionTowardsThePlayer.normalized
            * projectile.Speed, ForceMode2D.Impulse);
        Destroy(projectile, 3f);
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private new void OnCollisionStay2D(Collision2D collision)
    {

    }
}
