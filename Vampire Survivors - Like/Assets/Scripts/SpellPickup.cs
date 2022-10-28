using UnityEngine;
using DG.Tweening;

public class SpellPickup : MonoBehaviour
{
    [SerializeField] private GameObject useKeyPrompt;

    private Vector3 _promptDefaultPos;

    private bool _canPickup = false;

    private Spell _spell;

    private Spell[] _playerSpells;

    private void Awake()
    {
        Physics2D.SetLayerCollisionMask(gameObject.layer, LayerMask.GetMask("Player"));

        _spell = GetComponent<Spell>();

        _promptDefaultPos = useKeyPrompt.transform.position;
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
        
        useKeyPrompt.GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
        useKeyPrompt.transform.DOLocalMoveY(0.67f, 0.2f);
        
        _canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        useKeyPrompt.transform.DOLocalMoveY(0.1f, 0.2f);
        useKeyPrompt.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);

        _canPickup = false;
    }

    private void Pickup()
    {
        var playerCombat = Player.Instance.GetComponent<PlayerCombat>();

        if (_playerSpells[0] == null)
        {
            playerCombat.AddSpell(_spell, 0);
        }
        else if (_playerSpells[1] == null)
        {
            playerCombat.AddSpell(_spell, 1);
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
        spellChoiceMenu.GetComponent<SpellChoiceMenu>().ChoosingSpell = _spell;
        spellChoiceMenu.GetComponent<SpellChoiceMenu>().InitializeContainers();

        Time.timeScale = 0f;
        GameManager.Instance.IsGamePaused = true;
        GameManager.Instance.IsChoosingSpell = true;
    }
}
