using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchController
{
    public class Controller : MonoBehaviour
    {
        private bool IsControllable = true;
        private MatchModel.Model model = new Model(6, 8);
        private View view;

        private void Start()
        {
            this.view = GetComponent<View>();
        }

        private void Update()
        {
            var input = GetInput();

            if (IsControllable && input != null)
            {
                this.IsControllable = false;
                this.model.SetInput(view.ToPoint(input));
            }
        }

        private GameObject GetInput()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit))
                return hit.collider.gameObject;
            else
                return null;
        }
    }
}