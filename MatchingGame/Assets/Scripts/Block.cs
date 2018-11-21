using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
	Blue,
	Green,
	Red,
	Yellow
}

public class Block : MonoBehaviour 
{
	public Vector3 Coodinates;
	public BlockType Colour;
	public List<Material> Materials = new List<Material>();
	public int GridPosX, GridPosY;

	private void Start ()
	{
		Colour = (BlockType)UnityEngine.Random.Range(0, 4);
		GetComponent<MeshRenderer>().material = Materials[(int)Colour];
	}
}