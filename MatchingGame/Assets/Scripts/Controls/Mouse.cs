using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
	public class Mouse : IPlayerControls
	{
		public GameObject GetInteraction()
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit))
					return hit.collider.gameObject;
			}
			
			return null;
		}
	}
}