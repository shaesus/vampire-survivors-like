using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameEndStates
{
    Win,
    Lose
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject PauseMenu;
    public GameObject LoseVictoryMenu;

    public bool IsGamePaused { get; set; } = false;

    public int Stage { get; private set; }
    public int Score { get; private set; }
    public int ScoreToNextStage { get; private set; } = 10;

    [SerializeField] private int _scoreAddCoefficient = 10;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _loseVictoryText;

    private int _maxStage;

    private float _timeSinceGameStart = 0;

    private bool _isGameEnded = false;

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

    private void Update()
    {
        _timeSinceGameStart += Time.deltaTime;
    }

    public void IncrementStage()
    {
        if (Stage < _maxStage)
        {
            Stage++;
            ScoreToNextStage += _scoreAddCoefficient;
            _scoreAddCoefficient = (int)(_scoreAddCoefficient * 1.3f);
            GlobalEventManager.SendOnGameStageChanged();
        }
    }

    public void IncrementScore()
    {
        Score++;

        if (Score >= 300)
        {
            EndGame(GameEndStates.Win);
        }

        if (Stage < _maxStage && Score >= ScoreToNextStage)
        {
            IncrementStage();
        }

        GlobalEventManager.SendOnScoreChanged();
    }

    public void EndGame(GameEndStates state)
    {
        if (state == GameEndStates.Lose)
        {
            LoseVictoryMenu.SetActive(true);
            _loseVictoryText.text = "YOU LOST!";
            _isGameEnded = true;
            Time.timeScale = 0f;
            _timeText.text = "time: " + _timeSinceGameStart.ToString();
        }
        else
        {
            LoseVictoryMenu.SetActive(true);
            _loseVictoryText.text = "YOU WON!";
            _isGameEnded = true;
            Time.timeScale = 0f;
            _timeText.text = "time: " + _timeSinceGameStart.ToString();
        }
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
        if ((PauseMenu.activeSelf == true || LoseVictoryMenu.activeSelf == true)
            && _isGameEnded == false)
        {
            IsGamePaused = false;
            HUD.Instance.OptionsMenu.SetActive(false);
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (PauseMenu.activeSelf == false && _isGameEnded == false)
        {
            IsGamePaused = true;
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        _isGameEnded = false;
        TogglePauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
