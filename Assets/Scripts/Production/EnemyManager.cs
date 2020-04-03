using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
	[SerializeField] private GameObject enemyPrefab = null;

	private void Update()
	{
		// Just for test
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CreateEnemy();
		}
	}

	public void CreateEnemy()
	{
		Instantiate(enemyPrefab);
	}
}