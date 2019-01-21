using UnityEngine;

namespace MatchingGame
{
	public class Controls
	{
		public GameObject GetInteraction()
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					var interaction = hit.collider.gameObject;
					return interaction;
				}
			}
			return null;
		}
	}
}