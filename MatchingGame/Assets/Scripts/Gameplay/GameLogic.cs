using System;
using UnityEngine;

namespace MatchingGame
{
	public class GameLogic : MonoBehaviour
	{
		private IPlayerControls controls = new Mouse();

		private void Update() 
		{
			try
			{
				GridOperations.FillGrid();
				
				if (controls.GetInteraction() != null)
					Destroy(controls.GetInteraction());
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