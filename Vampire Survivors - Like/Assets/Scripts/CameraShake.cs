using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 1f;
    [SerializeField] private float _shakeMagnitude = 1f;

    private void Start()
    {
        Player.Instance.OnPlayerTakeDamage.AddListener(ShakeCamera);
    }

    private void ShakeCamera()
    {
        StartCoroutine(Shake(_shakeDuration, _shakeMagnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        var originalPos = Vector3.zero;

        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            var xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            var yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
