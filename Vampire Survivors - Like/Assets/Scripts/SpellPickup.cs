using UnityEngine;
using DG.Tweening;

public class SpellPickup : MonoBehaviour
{
    [SerializeField] private GameObject _useKeyPrompt;

    private Vector3 _promptDefaulPos;

    private bool _canPickup = false;

    private Spell spell;

    private Spell[] _playerSpells;

    private void Awake()
    {
        Physics2D.SetLayerCollisionMask(gameObject.layer, LayerMask.GetMask("Player"));

        spell = GetComponent<Spell>();

        _promptDefaulPos = _useKeyPrompt.transform.position;
    }

    private void Start()
    {
        _playerSpells = Player.Instance.GetComponent<PlayerCombat>().Spells;
    }

    private void Update()
    {
        if (_canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Can pickup!1!");

        _useKeyPrompt.SetActive(true);

        _canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _useKeyPrompt.SetActive(false);
        _useKeyPrompt.transform.position = _promptDefaulPos;

        _canPickup = false;
    }

    private void Pickup()
    {
        var playerCombat = Player.Instance.GetComponent<PlayerCombat>();

        if (_playerSpells[0] == null)
        {
            playerCombat.AddSpell(spell, 0);
        }
        else if (_playerSpells[1] == null)
        {
            playerCombat.AddSpell(spell, 1);
        }
        else
        {
            //UI choice
            ChooseSpell();
        }

        Destroy(gameObject);
    }

    private void ChooseSpell()
    {
        var spellChoiceMenu = HUD.Instance.SpellChoiceMenu;
        spellChoiceMenu.SetActive(true);
        spellChoiceMenu.GetComponent<SpellChoiceMenu>().ChoosingSpell = spell;
        spellChoiceMenu.GetComponent<SpellChoiceMenu>().InitializeContainers();

        Time.timeScale = 0f;
        GameManager.Instance.IsGamePaused = true;
        GameManager.Instance.IsChoosingSpell = true;
    }
}
