using UnityEngine;

public class GameLogic : MonoBehaviour
{
	private Grid grid = new Grid();
	public Block block;

	private void Start() 
	{
		for (int x = 0; x < grid.Width; x++)
			for (int y = 0; y < grid.Height; y++)
			{
				grid.Blocks[x].Add(Instantiate(block));
				grid.Move(grid.Blocks[x][y], x, y);
			}
	}

	private void Update() 
	{
		var mouse = new Mouse();

		if (mouse.GetInteraction() != null)
			grid.RemoveBlock(mouse.GetInteraction().GetComponent<Block>());

		grid.Cascade();
	}
}