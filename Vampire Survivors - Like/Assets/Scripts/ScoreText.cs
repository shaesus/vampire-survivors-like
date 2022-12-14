using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        GlobalEventManager.OnScoreChanged.AddListener(UpdateScoreText);
    }

    private void UpdateScoreText()
    {
        _scoreText.text = "SCORE " + GameManager.Instance.Score.ToString();
    }
}
