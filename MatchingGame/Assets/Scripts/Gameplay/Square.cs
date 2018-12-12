using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour, IBlock
{
	[SerializeField] private Material[] colours;
	public GameObject GetObject { get { return gameObject; } }
	public BlockType blockType { get; set; }
	public Vector3 GetTransform
	{ 
		get { return gameObject.transform.position; }
		set { gameObject.transform.position = value; } 
	}

	private void Start ()
	{
		blockType = (BlockType)UnityEngine.Random.Range(0, 4);
		GetComponent<MeshRenderer>().material = colours[(int)blockType];
	}

	public void Activate(GameGrid grid)
	{
		if (grid == null)
			throw new InvalidGridException("Cannot activate block on a null grid object.");

        var toBeDestroyed = new List<IBlock>();
		var queue = new Queue<IBlock>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (!toBeDestroyed.Contains(current) && current.blockType == this.blockType)
            {
                toBeDestroyed.Add(current);
				
				var surroundingBlocks = grid.GetSurroundingBlocks(current, false);
				foreach (var block in surroundingBlocks)
					queue.Enqueue(block);
            }
        }

        grid.DestroyBlocks(toBeDestroyed);
	}
}