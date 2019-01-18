using UnityEngine;

namespace MatchingGame
{
    public class GameLogic : MonoBehaviour
    {
        private Controls controls = new Controls();
        private CellGrid cellGrid = new CellGrid(6, 8);
        private VisualGrid visualGrid;
        private ScoreManager scoreManager;

        private void Start()
        {
            this.visualGrid = GetComponent<VisualGrid>();
            this.visualGrid.Build(cellGrid);
            
            this.scoreManager = GetComponent<ScoreManager>();
            this.scoreManager.Moves = Random.Range(10, 20);
        }

        private void Update()
        {
            var interaction = controls.GetInteraction();

            if (interaction != null && cellGrid.Events.Count == 0)
            {
                var point = GridQuery.ToPoint(this.visualGrid, interaction);
                var cell = cellGrid.Columns[point.x][point.y];
                cell.Activate(cellGrid);
                
                this.scoreManager.Moves--;
            }
            else if (cellGrid.Events.Count == 0)
                GridOperation.Fill(cellGrid);
            else if (cellGrid.Events.Count > 0)
                this.visualGrid.Sync(cellGrid);
                
            this.visualGrid.Cascade();
        }
    }
}