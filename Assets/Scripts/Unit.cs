using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	protected UnitStats _myStats;


	public void TakeDamage(int damage)
	{
		_myStats.Health -= damage;

		if (_myStats.Health <= 0)
			Die();
	}

	protected virtual void Die()
	{
		Destroy(gameObject);
	}
}
