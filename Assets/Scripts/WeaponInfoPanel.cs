using UnityEngine;
using TMPro;

public class WeaponInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _damageText;

    public void InitializePanel(string name, string type, float damage)
    {
        _nameText.text = "Name: " + name;
        _typeText.text = "Type: " + type;
        _damageText.text = "Damage: " + damage.ToString();
    }
}
