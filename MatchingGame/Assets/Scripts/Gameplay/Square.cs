using UnityEngine;
using System.Collections.Generic;

namespace MatchingGame
{
	public class Square : IBlock
	{
		public BlockType blockType { get; set; }

		public Square()
		{
			blockType = BlockType.Square;
		}

		public void Activate()
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
					
					var surroundingBlocks = GridQuery.GetSurroundingBlocks(current, false);
					foreach (var block in surroundingBlocks)
						queue.Enqueue(block);
				}
			}

			if (toBeDestroyed.Count >= 2)
				BlockOperations.RemoveBlocks(toBeDestroyed);
		}
	}
}