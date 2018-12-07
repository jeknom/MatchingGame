using System.Collections;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	[SerializeField] private GameGrid grid;
	private IPlayerControls controls = new Mouse();

	private void Update() 
	{
		if (controls.GetInteraction() != null)
			controls.GetInteraction().GetComponent<IBlock>().Activate(grid);

		grid.FillGrid();
		grid.Cascade();
	}
}