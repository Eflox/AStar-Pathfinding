using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField] private int width = 30;
	[SerializeField] private int height = 30;

	public Node[,] grid;

	public GameObject cellPrefab;
	public Node startNode, endNode;
	public List<Node> path;

	Pathfinding pathfinding;

	private void Awake()
	{
		pathfinding = GetComponent<Pathfinding>();
	}

	private void Start()
	{
		CreateGrid();
	}

	private void CreateGrid()
	{
		grid = new Node[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var tmpCell = Instantiate(cellPrefab, new Vector3(x - (width/2), y - (height/2), 0), Quaternion.identity);
				tmpCell.GetComponent<NodeXY>().x = x;
				tmpCell.GetComponent<NodeXY>().y = y;
				grid[x, y] = new Node(x, y, false, tmpCell);
			}
		}
	}

	public List<Node> GetNeighbors(Node node)
	{
		List<Node> neighbors = new List<Node>();

		if (node.x > 0)
			neighbors.Add(grid[node.x - 1, node.y]);
		if (node.x < width - 1)
			neighbors.Add(grid[node.x + 1, node.y]);
		if (node.y > 0)
			neighbors.Add(grid[node.x, node.y - 1]);
		if (node.y < height - 1)
			neighbors.Add(grid[node.x, node.y + 1]);
		if (node.x > 0 && node.y > 0)
			neighbors.Add(grid[node.x - 1, node.y - 1]);
		if (node.x < width - 1 && node.y < height - 1)
			neighbors.Add(grid[node.x + 1, node.y + 1]);
		if (node.x < width - 1 && node.y > 0)
			neighbors.Add(grid[node.x + 1, node.y - 1]);
		if (node.x > 0 && node.y < height - 1)
			neighbors.Add(grid[node.x - 1, node.y + 1]);

		return neighbors;
	}
}
