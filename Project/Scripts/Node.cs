using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public int x, y, gCost, hCost;
	public bool wall;
	public Node parent;
	public GameObject visual;

	public int FCost
	{
		get
		{
			return gCost + hCost;
		}
	}

	public Node(int _x, int _y, bool _wall, GameObject _visual)
	{
		this.x = _x;
		this.y = _y;
		this.wall = _wall;
		this.visual = _visual;
		this.visual.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
	}

	public void makeWall()
	{
		this.wall = true;
		visual.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
	}

	public void breakWall()
	{
		this.wall = false;
		visual.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
	}
}
