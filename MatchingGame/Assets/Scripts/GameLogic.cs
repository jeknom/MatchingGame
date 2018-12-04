using System.Collections;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	[SerializeField] private GameGrid grid;
	private IPlayerControls controls = new Mouse();

	private void Update() 
	{
		while (!grid.NeedsCascading && controls.GetInteraction() != null)
			grid.FloodRemove(controls.GetInteraction());

		grid.AddMissingBlocks();
		grid.Cascade();
	}
}