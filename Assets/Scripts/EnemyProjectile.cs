using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float Speed = 10f;

    public float Damage { get; set; } = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player playerComp))
        {
            playerComp.TakeDamage(Damage);
        }
    }
}
