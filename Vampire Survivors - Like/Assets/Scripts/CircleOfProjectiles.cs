using UnityEngine;

public class CircleOfProjectiles : MonoBehaviour
{
    public RotatingWeapon RotatingWeapon { get; set; }

    [SerializeField] private float _rotationAngle = 10f; //Circle rotation angle

    private void Update()
    {
        transform.Rotate(transform.forward, -_rotationAngle * Time.deltaTime); //Rotate circle
    }

    private void LateUpdate()
    {
        transform.position = Player.Instance.transform.position; //Follow Player
    }

    public void CreateCircle()
    {
        var angle = 0f;
        var angleOffset = 2 * Mathf.PI / RotatingWeapon.Count;
        Vector3 offset;
        for (int i = 0; i < RotatingWeapon.Count; i++)
        {
            offset = new Vector3(Mathf.Cos(angle) * RotatingWeapon.Radius, Mathf.Sin(angle) * RotatingWeapon.Radius, 0);
            var projectile = Instantiate(RotatingWeapon.Prefab, transform.position + offset, Quaternion.identity);
            RotatingWeapon.Projectiles.Add(projectile);
            projectile.transform.localScale = Vector3.one * Mathf.Pow(1.1f, RotatingWeapon.Level - 1);
            projectile.transform.parent = transform;
            angle += angleOffset;
        }
    }
}
