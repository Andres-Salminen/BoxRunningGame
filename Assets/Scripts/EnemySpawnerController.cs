using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour {

	[SerializeField] private GameObject[] _enemyPrefabs;
	[SerializeField] private Transform[] _spawnPoints;

	[SerializeField] private int _maxConcurrentEnemies = 60;
	[SerializeField] private float _timeBetweenSpawns = 1f;
	[SerializeField] private float _timeBetweenSpawnsRandomisation = 2f;

	private float _nextSpawnTime = 0f;
	private int _currentEnemyCount = 0;
	private bool _spawningStarted = false;

	private Transform _playerTransform;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (_spawningStarted &&
			Time.time > _nextSpawnTime &&
			_currentEnemyCount < _maxConcurrentEnemies)
		{
			SpawnEnemy();
		}
	}

	private void SpawnEnemy()
	{
		_nextSpawnTime = Time.time + _timeBetweenSpawns + Random.Range(0f, _timeBetweenSpawnsRandomisation);
		int randomIndex = Random.Range(0, _spawnPoints.Length);
		Transform spawnPoint = _spawnPoints[randomIndex];

		InstantiateAndInitializeEnemy(spawnPoint);

		++_currentEnemyCount;
	}

	private void InstantiateAndInitializeEnemy(Transform spawnPoint)
	{
		int enemyIndex = Random.Range(0, _enemyPrefabs.Length);
		EnemyController enemy = GameObject.Instantiate(_enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity).GetComponent<EnemyController>();
		enemy.SetTarget(_playerTransform);
		enemy.SetSpawnerController(this);
		Vector3 newPosition = enemy.transform.position;
		newPosition.y += enemy.transform.localScale.y * 0.5f;
		enemy.transform.position = newPosition;
	}

	public void StartSpawning()
	{
		_spawningStarted = true;
		_playerTransform = FindObjectOfType<PlayerController>().transform;
	}

	public void ReduceCount()
	{
		--_currentEnemyCount;
	}
}
