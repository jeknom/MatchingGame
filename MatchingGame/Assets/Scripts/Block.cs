using System;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
	[SerializeField] private Material[] colours;
	private Material material;

	private void OnEnable ()
	{
		material = colours[UnityEngine.Random.Range(0, 4)];
		GetComponent<MeshRenderer>().material = material;
	}

	public GameObject GetObject() 
	{ 
		return this.gameObject;
	}

	public Material GetMaterial()
	{
		return this.material;
	}

	public void Destroy()
	{
		Destroy(this.gameObject);
	}
}