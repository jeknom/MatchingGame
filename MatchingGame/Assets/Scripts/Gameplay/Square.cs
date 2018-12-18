using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingGame
{
	public class Square : MonoBehaviour, IBlock
	{
		[SerializeField] private Color32[] colours;
		public GameObject GetObject { get { return gameObject; } }
		public BlockType blockType { get; set; }
		public Vector3 GetRectTransform
		{ 
			get { return gameObject.GetComponent<RectTransform>().position; }
			set { gameObject.GetComponent<RectTransform>().position = value; }
		}

		private void Start ()
		{
			blockType = (BlockType)UnityEngine.Random.Range(0, 4);
			GetComponent<Image>().color = colours[(int)blockType];
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

			if (toBeDestroyed.Count >= 2)
				grid.DestroyBlocks(toBeDestroyed);
		}
	}
}