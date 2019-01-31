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
            if (input != null)
            {
                StopAllCoroutines();
                this.model.SetInput(view.ToPoint(input));
            }

            var events = this.model.Events;
            if (events.Count > 0)
            {
                this.view.Sync(events);
                this.model.Events.Clear();
            }
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