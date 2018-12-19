using System;
using UnityEngine;

namespace MatchingGame
{
	public class GameLogic : MonoBehaviour
	{
		private GameGrid grid = new GameGrid();
		private IPlayerControls controls = new Mouse();

		private void Update() 
		{
			try
			{
				if (controls.GetInteraction() != null)
					controls.GetInteraction().Activate(grid);
				
				GridOperations.FillGrid(grid);
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