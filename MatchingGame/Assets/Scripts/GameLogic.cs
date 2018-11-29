using System.Collections;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	[SerializeField] private Block block;
	[SerializeField] private Vector3 BlockSpawnPoint = new Vector3(20, 0, 0);
	private GameGrid grid = new GameGrid();

	private void Start() 
	{
		for (int x = 0; x < grid.Width; x++)
			for (int y = 0; y < grid.Height; y++)
			{
				grid.Blocks[x].Add(Instantiate(block, BlockSpawnPoint, Quaternion.identity));
				StartCoroutine(grid.Cascade());
			}
	}

	private void Update() 
	{
		if (grid.IsMissingBlocks)
			foreach (var row in grid.Blocks)
				while (row.Count != grid.Height)
					row.Add(Instantiate(block, BlockSpawnPoint, Quaternion.identity));

		if (!grid.IsCascading)
		{
			var mouse = new Mouse();

			if (mouse.GetInteraction() != null)
				grid.RemoveBlock(mouse.GetInteraction().GetComponent<Block>());

			StartCoroutine(grid.Cascade());
		}
		
	}
}