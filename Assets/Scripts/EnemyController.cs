using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit {

	[SerializeField] private UnitStats _enemyStatsTemplate;

	private Transform _target;

	private Rigidbody _rigidBody;

	private EnemySpawnerController _spawnerController;

	// Use this for initialization
	void Start () {
		_rigidBody = GetComponent<Rigidbody>();

		_myStats = _enemyStatsTemplate;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	protected override void Die()
	{
		_spawnerController.ReduceCount();
		base.Die();
	}

	private void Move()
	{
		Vector3 toPlayer = _target.position - transform.position;
		_rigidBody.velocity = toPlayer.normalized * _myStats.MovementSpeed;
	}

	public void SetTarget(Transform target)
	{
		_target = target;
	}

	void OnCollisionEnter(Collision col)
	{
		PlayerController other = col.gameObject.GetComponent<PlayerController>();
		if (other != null)
		{
			other.TakeDamage(_myStats.Damage);
			Die();
		}
	}

	public void SetSpawnerController(EnemySpawnerController controller)
	{
		_spawnerController = controller;
	}

	
}
