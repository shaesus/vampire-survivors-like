using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = Player.Instance.MaxMana;

        UpdateManaBar();

        Player.Instance.OnPlayerChangeMana.AddListener(UpdateManaBar);
    }

    private void UpdateManaBar()
    {
        _slider.value = Player.Instance.CurrentMana;
    }
}
