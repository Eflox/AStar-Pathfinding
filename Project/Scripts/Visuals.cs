using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visuals : MonoBehaviour
{
	public List<Node> nodesToColor = new List<Node>();

	private int index = 3;
	[SerializeField] private int nodeCount = 3;

	private float timer;

	Grid grid;

	private void Awake()
	{
		grid = GetComponent<Grid>();
		index = nodeCount;
	}

	private void Update()
	{
		if (index == nodesToColor.Count - 1 || nodesToColor.Count == 0)
			return;

		timer -= Time.deltaTime;
		if (timer < 0)
		{

			if (index < nodesToColor.Count - (nodeCount + 1))
			{
				for (int j = nodeCount; j > 0; j--)
					nodesToColor[index - j].visual.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;

				for (int j = 0; j < nodeCount; j++)
					nodesToColor[index + j].visual.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

				index += nodeCount;
			}
			else
			{
				for (int j = nodeCount; j > 0; j--)
					nodesToColor[index - j].visual.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;

				nodesToColor[++index].visual.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

			}

			timer = 0.01f;

			if (index == nodesToColor.Count - 1)
				foreach (var node in grid.path)
					node.visual.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
		}

	}

	private void SetOldColor(int index)
	{
		nodesToColor[index].visual.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
	}

	private void SetNewColor(int index)
	{
		nodesToColor[index].visual.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
	}

	public void ClearBoard()
	{
		if (nodesToColor.Count == 0)
			return ;

		foreach (var node in nodesToColor)
			node.visual.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;

		index = nodeCount;
		nodesToColor.Clear();
	}
}
