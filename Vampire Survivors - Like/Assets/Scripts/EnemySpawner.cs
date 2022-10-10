using System.Collections;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    private float _spawnCd = 1;

    [SerializeField] private int _maxEnemies = 15;

    [SerializeField] private float _spawnRadius = 15f;

    [SerializeField] private GameObject[] _enemies1stStage;
    [SerializeField] private GameObject[] _enemies2ndStage;
    [SerializeField] private GameObject[] _enemies3rdStage;
    [SerializeField] private GameObject[] _enemies4thStage;

    private GameObject[][] _enemies;

    private void Start()
    {
        _enemies = new GameObject[][] { _enemies1stStage, _enemies2ndStage, _enemies3rdStage, _enemies4thStage };

        StartCoroutine(SpawnEnemies());

        ChangeEnemySpawnCD();
        GlobalEventManager.OnGameStageChanged.AddListener(ChangeEnemySpawnCD);
        GlobalEventManager.OnGameStageChanged.AddListener(ChangeMaxEnemiesCount);
    }

    private void ChangeMaxEnemiesCount()
    {
        _maxEnemies = GameManager.Instance.Stage * 15;
    }

    private void ChangeEnemySpawnCD()
    {
        _spawnCd = 3 / GameManager.Instance.Stage;
    }

    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(_spawnCd);

            if (FindObjectsOfType<Enemy>().Where(enemy => enemy.IsDead == false).ToArray()
                .Length < _maxEnemies)
            {
                var randomEnemy = _enemies[GameManager.Instance.Stage - 1][Random.Range(0,
                    _enemies[GameManager.Instance.Stage - 1].Length)];

                var rndAngle = Random.Range(0, 360);
                //Calculating spawn position
                var offset = new Vector3(Mathf.Cos(rndAngle) * _spawnRadius, Mathf.Sin(rndAngle) * _spawnRadius, 0);
                Instantiate(randomEnemy, Player.Instance.transform.position + offset, Quaternion.identity);
            }
        }
    }
}
