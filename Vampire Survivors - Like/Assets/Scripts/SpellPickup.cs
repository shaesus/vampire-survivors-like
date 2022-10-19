using UnityEngine;

public class SpellPickup : MonoBehaviour
{
    private bool _canPickup = false;

    private Spell spell;

    private Spell[] _playerSpells;

    private void Awake()
    {
        Physics2D.SetLayerCollisionMask(gameObject.layer, LayerMask.GetMask("Player"));

        spell = GetComponent<Spell>();
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
        _canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canPickup = false;
    }

    private void Pickup()
    {
        if (_playerSpells[0] == null)
        {
            InitialiseSpell(0);
        }
        else if (_playerSpells[1] == null)
        {
            InitialiseSpell(1);
        }
        else
        {
            //UI choice
        }
        Destroy(gameObject);
    }

    private void InitialiseSpell(int position)
    {
        HUD.Instance.SpellImages[position].gameObject.SetActive(true);

        if (CompareTag("ExplosionPickup"))
        {
            var explosionSpell = Player.Instance.gameObject.AddComponent<ExplosionSpell>();

            _playerSpells[position] = explosionSpell;
        }
        else if (CompareTag("DashPickup"))
        {
            var dashSpell = Player.Instance.gameObject.AddComponent<DashSpell>();

            _playerSpells[position] = dashSpell;
        }
        else if (CompareTag("SpeedUpPickup"))
        {
            var speedUpSpell = Player.Instance.gameObject.AddComponent<SpeedUpSpell>();

            _playerSpells[position] = speedUpSpell;
            Debug.Log("SU");
        }

        HUD.Instance.SpellImages[position].sprite = spell.SpellSprite;
    }
}
