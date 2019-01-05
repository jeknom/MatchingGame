using System;
using UnityEngine;
using MatchingGame.Logic;
using MatchingGame.Visualization;
using MatchingGame.Controls;

namespace MatchingGame
{
	public class GameLogic : MonoBehaviour
	{
		private IPlayerControls controls = new Mouse();
		private GameGrid grid = new GameGrid();
		private GridVisualizer gridVisualizer;

		private void Start() 
		{
			gridVisualizer = GetComponent<GridVisualizer>();
			gridVisualizer.Build(grid);
		}

		private void Update() 
		{
			try
			{
				GridOperations.FillGrid(grid);
				gridVisualizer.Sync(grid);
				gridVisualizer.Cascade();
			}
			catch (InvalidGridException gridException)
			{
				Debug.Log("An unexpected error has occurred with the game grid. " + gridException);
			}
			catch (InvalidBlockException blockException)
			{
				Debug.Log("An unexpected error has occurred with one of the blocks. " + blockException);
			}
			catch (Exception exception)
			{
				Debug.Log("An unexpected error has occurred. " + exception);
			}
		}
	}
}