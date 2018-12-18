using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
	public class BlackBomb : MonoBehaviour, IBlock 
	{
		public GameObject GetObject { get { return gameObject; } }
		public BlockType blockType { get; set; }
		public Vector3 GetRectTransform
		{ 
			get { return gameObject.GetComponent<RectTransform>().position; }
			set { gameObject.GetComponent<RectTransform>().position = value; }
		}

		private void Start ()
		{
			blockType = BlockType.Bomb;
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

					var surroundingBlocks = grid.GetSurroundingBlocks(current, true);
					var surroundingNonBombs = surroundingBlocks.Where(b => b.blockType != BlockType.Bomb).ToList();
					var surroundingBombs = surroundingBlocks.Where(b => b.blockType == this.blockType).ToList();

					foreach (var nonBomb in surroundingNonBombs)
						if (!toBeDestroyed.Contains(nonBomb))
							toBeDestroyed.Add(nonBomb);

					foreach (var bomb in surroundingBombs)
						queue.Enqueue(bomb);
				}
			}

			grid.DestroyBlocks(toBeDestroyed);
		}
	}	
}