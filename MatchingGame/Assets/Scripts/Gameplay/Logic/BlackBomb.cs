using System.Linq;
using System.Collections.Generic;

namespace MatchingGame.Logic
{
	public class BlackBomb : IBlock 
	{
		public BlockType blockType { get; set; }

		public BlackBomb()
		{
			blockType = BlockType.Bomb;
		}

		public void Activate(GameGrid grid)
		{
			var toBeDestroyed = new List<IBlock>();
			var queue = new Queue<IBlock>();
			queue.Enqueue(this);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();

				if (!toBeDestroyed.Contains(current) && current.blockType == this.blockType)
				{
					toBeDestroyed.Add(current);

					var surroundingBlocks = GridQuery.GetSurroundingBlocks(grid, current, true);
					var surroundingNonBombs = surroundingBlocks.Where(b => b.blockType != BlockType.Bomb).ToList();
					var surroundingBombs = surroundingBlocks.Where(b => b.blockType == this.blockType).ToList();

					foreach (var nonBomb in surroundingNonBombs)
						if (!toBeDestroyed.Contains(nonBomb))
							toBeDestroyed.Add(nonBomb);

					foreach (var bomb in surroundingBombs)
						queue.Enqueue(bomb);
				}
			}

			BlockOperations.RemoveBlocks(grid, toBeDestroyed);
		}
	}	
}