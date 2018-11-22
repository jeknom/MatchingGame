using System;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
	public int x { get; set; }
	public int y { get; set; }

	public GameObject GetObject() 
	{ 
		return this.gameObject;
	}

	public void Destroy()
	{
		Destroy(this.gameObject);
	}
}

public interface IBlock
{
	int x { get; set; }
	int y { get; set; }

	GameObject GetObject();
	void Destroy();
}