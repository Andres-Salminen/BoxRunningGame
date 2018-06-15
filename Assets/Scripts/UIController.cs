using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField] private Text _scoreText;
	[SerializeField] private GameObject _countdownRoot;
	[SerializeField] private Text _countdownText;
	[SerializeField] private Text _anyKeyText;

	public static UIController Instance;

	// Use this for initialization
	void Awake () {
		if (Instance == null)
			Instance = this;
		else
			Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetScoreText (string text)
	{
		_scoreText.text = text;
	}

	public void SetCountdownText (string text)
	{
		_countdownText.text = text;
	}

	public void HideCountdownText()
	{
		_countdownRoot.SetActive(false);
	}

	public void EnableAnykeyText()
	{
		_anyKeyText.gameObject.SetActive(true);
	}
}
