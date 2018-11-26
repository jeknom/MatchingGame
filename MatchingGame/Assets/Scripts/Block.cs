using System;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
	public int x { get; set; }
	public int y { get; set; }

	[SerializeField] private Material[] colour;

	private void OnEnable ()
	{
		GetComponent<MeshRenderer>().material = colour[UnityEngine.Random.Range(0, 4)];
	}

	public GameObject GetObject() 
	{ 
		return this.gameObject;
	}

	public void Destroy()
	{
		Destroy(this.gameObject);
	}
}