using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private int _intervalBetweenWaves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _healthBarTemplate;
    [SerializeField] private Text _wavesCount;
    [SerializeField] private PlayerInfo _playerInfo;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastWave = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {        
        if (_currentWave == null)
        {
            if (_waves.Count > _currentWaveNumber + 1)
            {
                if (_timeAfterLastWave >= _intervalBetweenWaves)
                {
                    SetWave(++_currentWaveNumber);
                    _spawned = 0;
                    _timeAfterLastWave = 0;
                }
                _timeAfterLastWave += Time.deltaTime;
            }
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)        
            _currentWave = null;        
    }

    private void InstantiateEnemy()
    {   
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        _healthBarTemplate.GetComponentInChildren<EnemyHealthBar>().Init(enemy);

        Canvas _canvas = Instantiate(_healthBarTemplate, _spawnPoint.position + new Vector3(-0.57f, 1.7f + Random.Range(-0.2f, 0.2f), 0), _spawnPoint.rotation, enemy.transform).GetComponent<Canvas>();
        _canvas.GetComponent<Canvas>().worldCamera = Camera.main;

        enemy.Init(_player);
        enemy.Dying += OnEnemyDied;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _wavesCount.text = (_currentWaveNumber + 1).ToString();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _player.AddMoney(enemy.Reward);
        _playerInfo.ShowInfo();
        enemy.GetComponentInChildren<Canvas>().enabled = false;
        enemy.Dying -= OnEnemyDied;
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
