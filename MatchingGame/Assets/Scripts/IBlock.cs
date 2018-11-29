using UnityEngine;

public interface IBlock
{
	GameObject GetObject();
	Material GetMaterial();
	void Destroy();
}