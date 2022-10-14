using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed { get; set; } = 10f;

    public float Damage { get; set; } = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Debug.Log(enemy.name);
        }
    }
}
