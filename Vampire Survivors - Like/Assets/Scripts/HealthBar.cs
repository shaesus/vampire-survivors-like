using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = Player.Instance.MaxHealth;

        UpdateHealthBar();

        Player.Instance.OnPlayerChangeHp.AddListener(UpdateHealthBar);
    }

    private void UpdateHealthBar()
    {
        _slider.value = Player.Instance.CurrentHealth;
    }
}
