using Tools;
using UnityEngine;
using System.Collections.Generic;

namespace AI
{
	public class Dijkstra : IPathFinder
	{
		private List<Vector2Int> nodes;

		public Dijkstra(List<Vector2Int> traversibleNodes)
		{
			nodes = traversibleNodes;
		}

		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
			Vector2Int currentNode = start;
			Dictionary<Vector2Int, Vector2Int> ancestors = new Dictionary<Vector2Int, Vector2Int>();
			// lookingfornodes thing
			Queue<Vector2Int> frontier = new Queue<Vector2Int>();
			frontier.Enqueue(currentNode);

			while (frontier.Count > 0)
			{
				if (currentNode == goal)
				{
					break;
				}

				currentNode = frontier.Dequeue();

				foreach (Vector2Int direction in DirectionTools.Dirs)
				{
					Vector2Int nextNode = currentNode + direction;
					if (nodes.Contains(nextNode))
					{
						if (!ancestors.ContainsKey(nextNode))
						{
							frontier.Enqueue(nextNode);
							ancestors.Add(nextNode, currentNode);
						}
					}
				}

				if (ancestors.ContainsKey(goal))
				{
					List<Vector2Int> path = new List<Vector2Int>();
					foreach (var nextNode in ancestors)
					{
						path.Add(nextNode.Value);
					}
					path.Reverse();
					return path;
				}
			}

			return null;
		}
	}
}