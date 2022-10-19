using UnityEngine;

public class SpellChoiceMenu : MonoBehaviour
{
    public GameObject ChoosingContainer;

    public GameObject[] PlayerSpellCotainers;

    public Spell ChoosingSpell { get; set; }

    public void InitializeContainers()
    {
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

        gameObject.SetActive(false);
    }
}
