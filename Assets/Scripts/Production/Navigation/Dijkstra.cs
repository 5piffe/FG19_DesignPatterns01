using System;
using UnityEngine;
using System.Collections.Generic;

namespace AI
{
	//TODO: Fixa denna med hashsets osv och snygga till.
	//TODO: Implement IPathFinder using Dijsktra algorithm.
	public class Dijkstra : IPathFinder
	{
		private List<Vector2Int> grid;
		private Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

		public Dijkstra(List<Vector2Int> newGrid)
		{
			grid = newGrid;
		}
		
		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
			Vector2Int currentNode = start;
			Dictionary<Vector2Int, Vector2Int> ancestors = new Dictionary<Vector2Int, Vector2Int>();
			Queue<Vector2Int> queue = new Queue<Vector2Int>();
			queue.Enqueue(currentNode);

			while (queue.Count > 0)
			{
				currentNode = queue.Dequeue();
				if (currentNode == goal)
				{
					break;
				}

				for (int i = 0; i < directions.Length; i++)
				{
					Vector2Int node = currentNode + directions[i];
					if (grid.Contains(node))
					{
						if (!ancestors.ContainsKey(node))
						{
							queue.Enqueue(node);
							ancestors.Add(node, currentNode);
						}
					}
				}
			}

			if (ancestors.ContainsKey(goal))
			{
				List<Vector2Int> path = new List<Vector2Int>();
				foreach (var node in ancestors)
				{
					path.Add(node.Value);
				}

				path.Reverse();
				return path;
			}

			return null;
		}
	}
}