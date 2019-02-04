using System.Collections.Generic;
using MatchModel;
using MatchView;
using UnityEngine;

namespace MatchController
{
    public class Controller : MonoBehaviour
    {
        private Model model = new Model(6, 8);
        private View view;

        private void Start()
        {
            this.view = GetComponent<View>();
        }

        private void Update()
        {
            var input = GetInput();
            var changes = this.model.Events;

            if (input != null && !this.view.IsSyncing)
                this.model.SetInput(view.ToPoint(input));

            if (changes.Count > 0)
                this.view.Sync(changes, this.model.width, this.model.height);
        }

        private GameObject GetInput()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out RaycastHit hit))
                return hit.collider.gameObject;
            else
                return null;
        }
    }
}