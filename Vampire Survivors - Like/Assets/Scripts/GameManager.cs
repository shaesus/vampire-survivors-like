using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject PauseMenu;

    public bool IsGamePaused { get; private set; } = false;

    public int Stage { get; private set; }
    public int Score { get; private set; }
    public int ScoreToNextStage { get; private set; } = 10;

    private int _maxStage;

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

        Stage = 1;
    }

    private void Start()
    {
        Player.Instance.OnPlayerLvlUp.AddListener(LvlUpAllWeaponsAndSpells);

        GlobalEventManager.SendOnScoreChanged();
        GlobalEventManager.SendOnGameStageChanged();

        _maxStage = EnemySpawner.Instance.Enemies.GetLength(0);
    }

    public void IncrementStage()
    {
        if (Stage < _maxStage)
        {
            Stage++;
            ScoreToNextStage += (int)(ScoreToNextStage * 1.1f);
            GlobalEventManager.SendOnGameStageChanged();
        }
    }

    public void IncrementScore()
    {
        Score++;

        if (Stage < _maxStage && Score >= ScoreToNextStage)
        {
            IncrementStage();
        }

        GlobalEventManager.SendOnScoreChanged();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LvlUpAllWeaponsAndSpells()
    {
        var weapons = Player.Instance.GetComponents<Weapon>();
        foreach (var weapon in weapons)
        {
            weapon.LvlUp();
        }

        var childWeapons = Player.Instance.GetComponentsInChildren<Weapon>();
        foreach (var weapon in childWeapons)
        {
            weapon.LvlUp();
        }

        var spells = Player.Instance.GetComponents<Spell>();
        foreach (var spell in spells)
        {
            spell.LvlUp();
        }
    }

    public void TogglePauseGame()
    {
        if (PauseMenu.activeSelf == true)
        {
            IsGamePaused = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            //WeaponContainers.Instance.gameObject.SetActive(false);
        }
        else
        {
            IsGamePaused = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            //WeaponContainers.Instance.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        TogglePauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
