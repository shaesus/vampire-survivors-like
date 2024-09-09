using UnityEngine;

public class SpellChoiceMenu : MonoBehaviour
{
    public GameObject ChoosingContainer;

    public GameObject[] PlayerSpellCotainers;

    public SpellPickup CurrentSpellPickup { get; private set; }

    public GameObject ExplosionSpellPickup;
    public GameObject DashSpellPickup;
    public GameObject SpeedUpSpellPickup;

    public Spell ChoosingSpell { get; set; }

    public void InitializeContainers(SpellPickup spellPickup)
    {
        CurrentSpellPickup = spellPickup;
        
        var playerSpells = Player.Instance.GetComponent<PlayerCombat>().Spells;

        for (int i = 0; i < PlayerSpellCotainers.Length; i++)
        {
            PlayerSpellCotainers[i].GetComponent<SpellContainer>().CurrentSpell = playerSpells[i];
            PlayerSpellCotainers[i].GetComponent<SpellContainer>().InitializeContainer();
        }

        ChoosingContainer.GetComponent<SpellContainer>().CurrentSpell = ChoosingSpell;
        ChoosingContainer.GetComponent<SpellContainer>().InitializeContainer();
    }

    public void EndChoice()
    {

        Time.timeScale = 1f;
        GameManager.Instance.IsGamePaused = false;
        GameManager.Instance.IsChoosingSpell = false;

        gameObject.SetActive(false);
    }
}
