using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour 
{
	public GameObject BlockPrefab;
	public GameObject[,] BlockGrid = new GameObject[8, 8];

	private void Start () 
	{
		for (int y = 0; y < 8; y++)
		{
			for (int x = 0; x < 8; x++)
			{
				BlockGrid[y, x] = Instantiate(BlockPrefab, new Vector3(0.0f, 20.0f, 10.0f), Quaternion.identity);
				Block.ActivateBlock(BlockGrid[y, x], new Vector3(x - 4, y + 10 + x, 10));
			}
		}
	}

	private void Update ()
	{
		
	}
}
