using UnityEngine;

public class CircleOfProjectiles : MonoBehaviour
{
    public GameObject Prefab { get; set; }

    [SerializeField] private int _count;

    [SerializeField] private float _rotationAngle = 10f; //Circle rotation angle

    private void Update()
    {
        transform.Rotate(transform.forward, -_rotationAngle * Time.deltaTime); //Rotate circle
    }

    private void LateUpdate()
    {
        transform.position = Player.Instance.transform.position; //Follow Player
    }

    public void CreateCircle(int count, float radius)
    {
        var angle = 0f;
        var angleOffset = 2 * Mathf.PI / count;
        Vector3 offset;
        for (int i = 0; i < count; i++)
        {
            offset = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            var projectile = Instantiate(Prefab, transform.position + offset, Quaternion.identity);
            projectile.transform.parent = transform;
            angle += angleOffset;
        }
    }
}
