using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour 
{
	public GameObject BlockPrefab;
	private const int GridWidth = 8, GridHeight = 14;
	public Block[,] BlockGrid = new Block[GridWidth, GridHeight];

	private void Start () 
	{
		for (int y = 0; y < GridHeight; y++)
		{
			for (int x = 0; x < GridWidth; x++)
			{
				var BlockObject = Instantiate(BlockPrefab, new Vector3(x, y), Quaternion.identity);
				BlockGrid[y, x] = BlockObject.GetComponent<Block>();
				BlockGrid[y, x].GridPosX = x;
				BlockGrid[y, x].GridPosY = y;
			}
		}
	}

	private void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			var markedForDestory = new List<GameObject>();

			if (Physics.Raycast (ray, out hit, 50f))
			{
				BlockDestroyer(hit.collider.gameObject.GetComponent<Block>());
			}
		}
	}

	private void BlockDestroyer(Block start)
	{
		var deathRow = new List<GameObject>();
		var blocks = new Stack<Block>();
		blocks.Push(start);

		while (blocks.Count > 0)
		{
			var a = blocks.Pop();
			if (a.GridPosX < GridWidth && a.GridPosX > 0 && a.GridPosY < GridHeight && a.GridPosY > 0)
			{
				if (a.Colour == start.Colour)
				{
					deathRow.Add(a.gameObject);
					blocks.Push(BlockGrid[a.GridPosX - 1, a.GridPosY]);
					blocks.Push(BlockGrid[a.GridPosX + 1, a.GridPosY]);
					blocks.Push(BlockGrid[a.GridPosX, a.GridPosY - 1]);
					blocks.Push(BlockGrid[a.GridPosX, a.GridPosY + 1]);
				}
			}
		}

		foreach (var block in deathRow)
			Destroy(block);
	}
}
