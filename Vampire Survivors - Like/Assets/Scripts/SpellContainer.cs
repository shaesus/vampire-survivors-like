using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellContainer : MonoBehaviour
{
    public Spell CurrentSpell { get; set; }

    [SerializeField] private Image _spellPlaceholder;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _discriptionText;

    private void Start()
    {
        InitializeContainer();
    }

    public void InitializeContainer()
    {
        _spellPlaceholder.sprite = CurrentSpell.SpellSprite;
        _nameText.text = CurrentSpell.Name;
        _discriptionText.text = CurrentSpell.Discription;
    }
}
