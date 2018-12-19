using UnityEngine;

namespace MatchingGame
{
	public interface IBlock
	{
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