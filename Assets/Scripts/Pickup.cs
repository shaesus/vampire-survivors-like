using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        Physics2D.SetLayerCollisionMask(gameObject.layer, LayerMask.GetMask("Player"));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var direction = (collision.transform.position - transform.position).normalized;
        _rb.velocity = direction;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
