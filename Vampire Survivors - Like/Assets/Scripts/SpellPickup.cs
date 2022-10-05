using UnityEngine;
using UnityEngine.Events;

public class SpellPickup : MonoBehaviour
{
    private void Awake()
    {
        Physics2D.SetLayerCollisionMask(gameObject.layer, LayerMask.GetMask("Player"));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Can pickup!");
    }

    private void Pickup()
    {
        var playerSpells = Player.Instance.GetComponent<PlayerCombat>().Spells;
        if (playerSpells[0] == null)
        {
            InitialiseSpell(ref playerSpells[0], 0);
        }
        else if (playerSpells[1] == null)
        {
            InitialiseSpell(ref playerSpells[1], 1);
        }
        else
        {
            //UI choice
        }
        Destroy(gameObject);
    }

    private void InitialiseSpell(ref UnityAction action, int position)
    {
        HUD.Instance.SpellImages[position].gameObject.SetActive(true);

        if (CompareTag("ExplosionPickup"))
        {
            var explosionSpell = Player.Instance.gameObject.AddComponent<ExplosionSpell>();

            HUD.Instance.SpellImages[position].sprite = HUD.Instance.ExplosionSpellSprite;

            action = explosionSpell.Cast;
        }
        else if (CompareTag("DashPickup"))
        {
            var dashSpell = Player.Instance.gameObject.AddComponent<DashSpell>();

            HUD.Instance.SpellImages[position].sprite = HUD.Instance.DashSpellSprite;

            action = dashSpell.Cast;
        }
    }
}
