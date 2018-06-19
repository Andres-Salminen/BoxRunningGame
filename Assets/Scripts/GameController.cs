using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject PlayerPrefab;

	private EnemySpawnerController _enemySpawner;


	public static GameController Instance;

	private bool _gameIsOver = false;

	private int _playerScore = 0;

	private float _startTime;

	private bool _gameStarted = false;

	

	// Use this for initialization
	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this);

		_enemySpawner = GetComponent<EnemySpawnerController>();

		Debug.Log("Awake called.");
	}

	void Start()
	{
		if (CheckIfValid())
		{
			StartCoroutine(CountDown());

			InputController.Instance.AddGetKeyDownInput(KeyCode.Escape, () => {
				Application.Quit();
			});
		}
		else
		{
			Debug.LogError("Scene is missing necessary components, the game will not work.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_gameIsOver && Input.anyKeyDown)
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(0);
		}
		else if (_gameStarted)
			AddScore();
	}

	public void AddScore()
	{
		_playerScore = (int)(Time.time - _startTime);
		UIController.Instance.SetScoreText(_playerScore.ToString());
	}

	private void StartGame()
	{
		GameObject.Instantiate(PlayerPrefab);
		_gameStarted = true;
		_startTime = Time.time;
		_enemySpawner.StartSpawning();
	}

	public void GameOver()
	{
		_gameIsOver = true;
		UIController.Instance.EnableAnykeyText();
		Time.timeScale = 0f;
	}

	private IEnumerator CountDown()
	{
		for (int i = 3; i > 0; --i)
		{
			UIController.Instance.SetCountdownText(i.ToString());
			yield return new WaitForSeconds(1f);
		}
		UIController.Instance.HideCountdownText();
		StartGame();
	}

	private bool CheckIfValid()
	{
		if (PlayerPrefab == null || UIController.Instance == null || _enemySpawner == null || InputController.Instance == null)
			return false;
		else
			return true;
	}
}
