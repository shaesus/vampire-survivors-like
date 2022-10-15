using UnityEngine;
using TMPro;

public class ScoreToNextStageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreToNextStageText;

    private void Awake()
    {
        GlobalEventManager.OnGameStageChanged.AddListener(UpdateScoreText);
    }

    private void UpdateScoreText()
    {
        string text;
        if (GameManager.Instance.Stage >= 9)
        {
            text = "TO NEXT STAGE -";
        }
        else
        {
            text = "TO NEXT STAGE " + GameManager.Instance.ScoreToNextStage.ToString();
        }

        _scoreToNextStageText.text = text;
    }
}
