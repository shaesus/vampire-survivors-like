using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.Mathematics;

public class PlayerSpellContainer : SpellContainer, IPointerEnterHandler, IPointerDownHandler
{
    private int _index;

    private SpellChoiceMenu _spellChoiceMenu;

    private void Awake()
    {
        _spellChoiceMenu = HUD.Instance.SpellChoiceMenu.GetComponent<SpellChoiceMenu>();
        if (gameObject == _spellChoiceMenu.PlayerSpellCotainers[0])
        {
            _index = 0;
        }
        else if (gameObject == _spellChoiceMenu.PlayerSpellCotainers[1])
        {
            _index = 1;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var currentSpellPickup = _spellChoiceMenu.CurrentSpellPickup;
        
        if (CurrentSpell is ExplosionSpell)
        {
            Instantiate(_spellChoiceMenu.ExplosionSpellPickup, currentSpellPickup.transform.position,
                quaternion.identity);
        }
        else if (CurrentSpell is DashSpell)
        {
            Instantiate(_spellChoiceMenu.DashSpellPickup, currentSpellPickup.transform.position,
                quaternion.identity);
        }
        else if (CurrentSpell is SpeedUpSpell)
        {
            Instantiate(_spellChoiceMenu.SpeedUpSpellPickup, currentSpellPickup.transform.position,
                quaternion.identity);
        }
        
        Destroy(currentSpellPickup.gameObject);
        
        Player.Instance.GetComponent<PlayerCombat>()
            .AddSpell(_spellChoiceMenu.ChoosingSpell, _index);
        _spellChoiceMenu.EndChoice();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var container = _spellChoiceMenu.ChoosingContainer;

        container.transform.DOMoveX(eventData.pointerEnter.transform.position.x, 0.2f).SetUpdate(true);

        Debug.Log(eventData.pointerEnter.gameObject.name);
    }
}
