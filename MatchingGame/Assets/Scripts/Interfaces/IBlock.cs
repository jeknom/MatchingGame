using UnityEngine;

namespace MatchingGame
{
	public interface IBlock
	{
		BlockType blockType { get; set; }
		void Activate();
	}

	public enum BlockType
	{
		Square,
		Bomb
	}
}