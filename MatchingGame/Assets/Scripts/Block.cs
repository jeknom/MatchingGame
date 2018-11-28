using System;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
	[SerializeField] private Material[] colours;
	private Material _material;

	public Material Colour { get { return _material; } }

	private void OnEnable ()
	{
		_material = colours[UnityEngine.Random.Range(0, 4)];
		GetComponent<MeshRenderer>().material = _material;
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