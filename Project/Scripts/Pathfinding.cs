using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Pathfinding : MonoBehaviour
{
	Grid grid;
	Visuals visuals;

	private void Awake()
	{
		grid = GetComponent<Grid>();
		visuals = GetComponent<Visuals>();
	}

	public void FindPath(Node startNode, Node endNode)
	{
		List<Node> openNodes = new List<Node>();
		List<Node> closedNodes = new List<Node>();

		openNodes.Add(startNode);

		while (openNodes.Count > 0)
		{
			Node currentNode = openNodes[0];

			for (int i = 1; i < openNodes.Count; i++)
				if (openNodes[i].FCost < currentNode.FCost || openNodes[i].FCost == currentNode.FCost && openNodes[i].hCost < currentNode.hCost)
					currentNode = openNodes[i];

			openNodes.Remove(currentNode);
			closedNodes.Add(currentNode);

			if (currentNode == endNode)
			{
				RetracePath(startNode, endNode);
				return ;
			}
			
			foreach (Node node in grid.GetNeighbors(currentNode))
			{
				if (node.wall == true || closedNodes.Contains(node))
					continue;

				int newMovementCost = currentNode.gCost + manhattenDistance(currentNode, node);
				if (newMovementCost < node.gCost || !openNodes.Contains(node))
				{
					node.gCost = newMovementCost;
					node.hCost = manhattenDistance(node, endNode);
					node.parent = currentNode;

					visuals.nodesToColor.Add(node);

					if (!openNodes.Contains(node))
						openNodes.Add(node);
				}
			}
		}
	}

	public  void RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		grid.path = path;
	}

	private int manhattenDistance(Node a, Node b)
	{
		int distX = Mathf.Abs(a.x - b.x);
		int distY = Mathf.Abs(a.y - b.y);

		if (distX > distY)
			return 14 * distY + 10 * (distX - distY);
		return 14 * distX + 10 * (distY - distX);
	}
}
