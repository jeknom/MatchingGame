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
	private BlockType Colour;
	public List<Material> Materials = new List<Material>();

	private void Start ()
	{
		Colour = (BlockType)UnityEngine.Random.Range(0, 4);
		GetComponent<MeshRenderer>().material = Materials[(int)Colour];
	}

	public static void ActivateBlock(GameObject block, Vector3 coordinates)
	{
		block.GetComponent<Rigidbody>().position = coordinates;
		block.GetComponent<Rigidbody>().useGravity = true;
	}
}