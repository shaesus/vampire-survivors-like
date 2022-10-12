using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;

    public float Damage { get
        {
            return _damage;
        }
    }

    [SerializeField] private float _damage = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (enemy.TryGetComponent<EnemySkeleton>(out var skeleton))
            {
                skeleton.TakeDamage(_damage);
                Debug.Log(skeleton.name);
            }
            else if (enemy.TryGetComponent<NecromasterEnemy>(out var necromaster))
            {
                necromaster.TakeDamage(_damage);
            }
            else
            {
                enemy.TakeDamage(_damage);
                Debug.Log(enemy.name);
            }
        }
    }
}
