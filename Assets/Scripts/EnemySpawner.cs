using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    private float _spawnCd = 1;

    [SerializeField] private int _maxEnemies = 15;

    [SerializeField] private float _spawnRadius = 15f;

    [SerializeField] private GameObject[] _enemies1stStage;
    [SerializeField] private GameObject[] _enemies2ndStage;
    [SerializeField] private GameObject[] _enemies3rdStage;
    [SerializeField] private GameObject[] _enemies4thStage;
    [SerializeField] private GameObject[] _enemies5thStage;
    [SerializeField] private GameObject[] _enemies6thStage;
    [SerializeField] private GameObject[] _enemies7thStage;
    [SerializeField] private GameObject[] _enemies8thStage;
    [SerializeField] private GameObject[] _enemies9thStage;

    public GameObject[][] Enemies;

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

        Enemies = new GameObject[][] { _enemies1stStage, _enemies2ndStage, _enemies3rdStage, 
            _enemies4thStage, _enemies5thStage, _enemies6thStage, _enemies7thStage, _enemies8thStage,
            _enemies9thStage};
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());

        ChangeEnemySpawnCD();
        ChangeMaxEnemiesCount();
        GlobalEventManager.OnGameStageChanged.AddListener(ChangeEnemySpawnCD);
        GlobalEventManager.OnGameStageChanged.AddListener(ChangeMaxEnemiesCount);
    }

    private void ChangeMaxEnemiesCount()
    {
        _maxEnemies = GameManager.Instance.Stage * 7;
    }

    private void ChangeEnemySpawnCD()
    {
        _spawnCd = 2 / Mathf.Pow(1.2f, GameManager.Instance.Stage - 1);
    }

    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnCd);

            if (FindObjectsOfType<Enemy>().Where(enemy => enemy.IsDead == false).ToArray()
                .Length < _maxEnemies)
            {
                var randomEnemy = Enemies[GameManager.Instance.Stage - 1][Random.Range(0,
                    Enemies[GameManager.Instance.Stage - 1].Length)];

                var rndAngle = Random.Range(0, 360);
                //Calculating spawn position
                var offset = new Vector3(Mathf.Cos(rndAngle) * _spawnRadius, Mathf.Sin(rndAngle) * _spawnRadius, 0);
                Instantiate(randomEnemy, Player.Instance.transform.position + offset, Quaternion.identity);
            }
        }
    }
}
