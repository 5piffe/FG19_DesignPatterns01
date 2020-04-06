using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	private Dijkstra dijkstra;
	private List<Vector2Int> walkableTile = new List<Vector2Int>();

	private void Awake()
	{
		
	}
}