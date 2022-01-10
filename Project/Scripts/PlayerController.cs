using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Pathfinding pathfinding;
	Visuals visuals;
	Grid grid;

	private void Awake()
	{
		pathfinding = GetComponent<Pathfinding>();
		visuals = GetComponent<Visuals>();
		grid = GetComponent<Grid>();
	}

	private void CreatePath()
	{
		Node endNode = ShootRay();

		if (endNode != null && !endNode.wall)
		{
			visuals.ClearBoard();
			endNode.visual.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			pathfinding.FindPath(grid.grid[3, 3], endNode);
		}
	}

	private void WallHandler()
	{
		Node target = ShootRay();

		if (target == null)
			return;

		visuals.ClearBoard();

		if (target.wall == false)
			target.makeWall();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
			CreatePath();

		if (Input.GetMouseButton(0))
			WallHandler();

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}

	public Node ShootRay()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (hit.collider != null)
		{
			int x, y;

			x = hit.transform.gameObject.GetComponent<NodeXY>().x;
			y = hit.transform.gameObject.GetComponent<NodeXY>().y;

			return (grid.grid[x, y]);
		}
		return null;
	}
}

