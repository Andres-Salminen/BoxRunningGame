using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit {

	[SerializeField] private UnitStats _playerStatsTemplate;

	private Rigidbody _rigidBody;

	private Vector3 _velocity;

	// Use this for initialization
	void Start () {
		_myStats = _playerStatsTemplate;

		_rigidBody = GetComponent<Rigidbody>();

		InputController.Instance.AddGetKeyInput(KeyCode.W, () => {
			AddVelocity(new Vector3(0f, 0f, 1f));
		});

		InputController.Instance.AddGetKeyInput(KeyCode.A, () => {
			AddVelocity(new Vector3(-1f, 0f, 0f));
		});

		InputController.Instance.AddGetKeyInput(KeyCode.S, () => {
			AddVelocity(new Vector3 (0f, 0f, -1f));
		});

		InputController.Instance.AddGetKeyInput(KeyCode.D, () => {
			AddVelocity(new Vector3 (1f, 0f, 0f));
		});
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Move();
	}

	public void AddVelocity(Vector3 direction)
	{
		_velocity += direction;
	}

	private void Move()
	{
		_rigidBody.velocity = _velocity.normalized * _myStats.MovementSpeed;
		_velocity = Vector3.zero;
	}

	protected override void Die()
	{
		GetComponent<MeshRenderer>().material.color = Color.black;
		GameController.Instance.GameOver();
	}
}
