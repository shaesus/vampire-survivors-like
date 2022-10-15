using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = Player.Instance.XPForNextLevel;

        UpdateXPBar();

        Player.Instance.OnPlayerChangeXp.AddListener(UpdateXPBar);
    }

    private void UpdateXPBar()
    {
        _slider.value = Player.Instance.CurrentXP;
    }
}
