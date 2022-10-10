using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Stage { get; private set; }

    public int Score { get; private set; }

    public int ScoreToNextStage { get; private set; } = 10;

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
        GlobalEventManager.SendOnScoreChanged();
        GlobalEventManager.SendOnGameStageChanged();
    }

    public void IncrementStage()
    {
        Stage++;
        ScoreToNextStage += (int)(ScoreToNextStage * 1.5f);
        GlobalEventManager.SendOnGameStageChanged();
    }

    public void IncrementScore()
    {
        Score++;

        if (Stage < 4 && Score >= ScoreToNextStage)
        {
            IncrementStage();
        }

        GlobalEventManager.SendOnScoreChanged();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
