using UnityEngine;

public interface IBlock
{
	int x { get; set; }
	int y { get; set; }

	GameObject GetObject();
	void Destroy();
}