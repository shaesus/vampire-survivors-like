using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset;

    private void Start()
    {
        _offset = new Vector3(0, 0, -10);
    }

    private void LateUpdate()
    {
        transform.position = Player.Instance.transform.position + _offset;
    }
}
