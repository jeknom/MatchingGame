using UnityEngine;

public class GameLogic : MonoBehaviour
{
	private Grid grid = new Grid();
	public Block block;

	private void Start() 
	{
		for (int y = 0; y < grid.Height; y++)
			for (int x = 0; x < grid.Width; x++)
			{
				grid.Blocks[x, y] = Instantiate(block);
				grid.Move(grid.Blocks[x, y], x, y);
			}
	}

	private void Update() 
	{
		grid.DestroyTarget(new Mouse());
	}
}