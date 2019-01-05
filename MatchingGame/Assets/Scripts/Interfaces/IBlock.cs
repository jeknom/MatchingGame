using UnityEngine;

namespace MatchingGame.Logic
{
	public interface IBlock
	{
		BlockType blockType { get; set; }
		void Activate(GameGrid grid);
	}

	public enum BlockType
	{
		Square,
		Bomb
	}
}