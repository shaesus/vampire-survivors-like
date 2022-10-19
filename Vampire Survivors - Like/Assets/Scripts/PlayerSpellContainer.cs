using UnityEngine;
using UnityEngine.EventSystems;

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
        Player.Instance.GetComponent<PlayerCombat>()
            .AddSpell(_spellChoiceMenu.ChoosingSpell, _index);
        _spellChoiceMenu.EndChoice();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var container = _spellChoiceMenu.ChoosingContainer;
        container.transform.position = new Vector3(eventData.pointerEnter.transform.position.x,
            container.transform.position.y, container.transform.position.z);
        Debug.Log(eventData.pointerEnter.gameObject.name);
    }
}
