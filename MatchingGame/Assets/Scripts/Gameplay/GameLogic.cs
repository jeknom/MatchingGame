using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	[SerializeField] private GameGrid grid;
	private IPlayerControls controls = new Mouse();

	private void Update() 
	{
		try
		{
			if (controls.GetInteraction() != null)
			controls.GetInteraction().GetComponent<IBlock>().Activate(grid);

			grid.FillGrid();
			
			grid.Cascade();
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