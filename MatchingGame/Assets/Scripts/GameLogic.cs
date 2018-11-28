using System.Collections;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	[SerializeField] private Block block;
	[SerializeField] private Vector3 BlockSpawnPoint = new Vector3(0, 0, 0);
	private Grid grid = new Grid();

	private void Start() 
	{
		for (int x = 0; x < grid.Width; x++)
			for (int y = 0; y < grid.Height; y++)
			{
				grid.Blocks[x].Add(Instantiate(block, BlockSpawnPoint, Quaternion.identity));
				StartCoroutine(grid.MoveBlock(grid.Blocks[x][y], new Vector3(x, y)));
			}
	}

	private void Update() 
	{
		var mouse = new Mouse();

		if (mouse.GetInteraction() != null)
			grid.RemoveBlock(mouse.GetInteraction().GetComponent<Block>());

		StartCoroutine(Cascade());
	}

	public IEnumerator Cascade()
    {
        foreach (var row in grid.Blocks)
            row.ForEach(b => StartCoroutine(grid.MoveBlock(b, new Vector3(grid.Blocks.IndexOf(row), row.IndexOf(b)))));
        
        yield return null;
    }
}