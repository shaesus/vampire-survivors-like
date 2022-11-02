using UnityEngine;
using DG.Tweening;

public class SpellPickup : MonoBehaviour
{
    [SerializeField] private GameObject useKeyPrompt;
    
    private bool _canPickup = false;

    private Spell _spell;

    private Spell[] _playerSpells;

    private void Awake()
    {
        Physics2D.SetLayerCollisionMask(gameObject.layer, LayerMask.GetMask("Player"));

        _spell = GetComponent<Spell>();
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

        useKeyPrompt.transform.DOLocalMoveY(0.67f, 0.2f);
        useKeyPrompt.GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
        
        _canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        useKeyPrompt.transform.DOLocalMoveY(0.1f, 0.2f);
        useKeyPrompt.GetComponent<SpriteRenderer>().DOFade(0, 0.1f);

        _canPickup = false;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Pickup()
    {
        var playerCombat = Player.Instance.GetComponent<PlayerCombat>();

        if (_playerSpells[0] == null)
        {
            playerCombat.AddSpell(_spell, 0);
            Destroy(gameObject);
        }
        else if (_playerSpells[1] == null)
        {
            playerCombat.AddSpell(_spell, 1);
            Destroy(gameObject);
        }
        else
        {
            //UI choice
            ChooseSpell();
        }
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void ChooseSpell()
    {
        var spellChoiceMenu = HUD.Instance.SpellChoiceMenu;
        spellChoiceMenu.SetActive(true);
        spellChoiceMenu.GetComponent<SpellChoiceMenu>().ChoosingSpell = _spell;
        spellChoiceMenu.GetComponent<SpellChoiceMenu>().InitializeContainers(this);

        Time.timeScale = 0f;
        GameManager.Instance.IsGamePaused = true;
        GameManager.Instance.IsChoosingSpell = true;
    }
}
