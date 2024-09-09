using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    public GameObject SpellChoiceMenu;

    public Sprite DashSpellSprite;
    public Sprite ExplosionSpellSprite;
    public Sprite SpeedUpSpellSprite;

    public Image[] SpellImages;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Player.Instance.GetComponent<PlayerCombat>().OnSpellSwitch.AddListener(SwitchSpellSprites);
    }

    private void SwitchSpellSprites()
    {
        if (SpellImages[0].sprite == null && SpellImages[1].sprite == null)
        {
            Debug.Log("2 NULLS");
            return;
        }
        else if (SpellImages[0].sprite != null && SpellImages[1].sprite != null)
        {
            (SpellImages[0].sprite, SpellImages[1].sprite) =
                (SpellImages[1].sprite, SpellImages[0].sprite);
            Debug.Log("0 NULLS");
            return;
        }
        else
        {
            int count = 0;
            int i = 0;
            while (count < 2)
            {
                if (SpellImages[i].sprite == null)
                {
                    SpellImages[i].gameObject.SetActive(true);
                    SpellImages[i].sprite = SpellImages[Utilities.IncrementInRange(ref i)].sprite;
                    SpellImages[i].sprite = null;
                    SpellImages[i].gameObject.SetActive(false);
                }
                Utilities.IncrementInRange(ref i);
                count++;
            }
            Debug.Log("1 NULL");
        }
    }
}
