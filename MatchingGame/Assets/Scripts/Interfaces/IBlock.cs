using UnityEngine;

namespace MatchingGame
{
	public interface IBlock
	{
		GameObject GetObject { get; }
		Vector3 GetRectTransform { get; set; }
		BlockType blockType { get; set; }
		void Activate(GameGrid grid);
	}

	public enum BlockType
	{
		Red,
		Blue,
		Yellow,
		Green,
		Bomb
	}
}