using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;

    [SerializeField] private float _refreshRate = 0.5f;

    private float _timer;

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            var fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = string.Format("{0} fps", fps);
            _timer = Time.unscaledDeltaTime + _refreshRate;
        }
    }
}
