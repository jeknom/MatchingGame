using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile : IPlayerControls 
{
	public GameObject GetInteraction()
	{
		if (Input.touchCount > 0)
		{
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast(ray, out hit))
				return hit.collider.gameObject;
		}
		
		return null;
	}
}
