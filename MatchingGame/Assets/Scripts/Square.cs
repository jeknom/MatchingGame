using System;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour, IBlock
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

	public void Activate(GameGrid grid)
	{
        var toBeDestroyed = new List<IBlock>();
        var queue = new Queue<Vector3>();
        queue.Enqueue(grid.PositionToVector(this));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if ((grid.IsValidLocation(current) && grid.PositionToBlock(current).GetMaterial() == this.GetMaterial()) 
                    && (!toBeDestroyed.Contains(grid.PositionToBlock(current))))
            {
                toBeDestroyed.Add(grid.PositionToBlock(current));
                queue.Enqueue(new Vector3(current.x - 1, current.y));
                queue.Enqueue(new Vector3(current.x + 1, current.y));
                queue.Enqueue(new Vector3(current.x, current.y - 1));
                queue.Enqueue(new Vector3(current.x, current.y + 1));
            }
        }
        
        grid.DestroyBlocks(toBeDestroyed);
	}
}