using UnityEngine;

namespace MatchingGame
{
	public interface IBlock
	{
		GameObject GetObject { get; }
		Vector3 GetTransform { get; set; }
		BlockType blockType { get; set; }
		void Activate(GameGrid grid);
	}

	public enum BlockType
	{
		red,
		blue,
		yellow,
		green,
		bomb
	}
}