using System.Linq;
using UnityEngine;

namespace MatchingGame
{
    public class GameLogic : MonoBehaviour
    {
        private Controls controls = new Controls();
        private CellGrid cellGrid = new CellGrid(6, 8);
        private ScoreManager scoreManager;
        private VisualGrid visualGrid;

        private void Start()
        {
            this.visualGrid = GetComponent<VisualGrid>();
            this.visualGrid.Build(cellGrid);
            
            this.scoreManager = GetComponent<ScoreManager>();
            this.scoreManager.Moves = Random.Range(10, 20);
            this.scoreManager.ObjectiveType = (BlockType)Random.Range(0, 3);
            this.scoreManager.ObjectiveCount = Random.Range(20, 25);
        }

        private void Update()
        {
            var interaction = controls.GetInteraction();

            if (interaction != null && cellGrid.Events.Count == 0)
            {
                var point = GridQuery.ToPoint(this.visualGrid, interaction);
                var cell = cellGrid.Columns[point.x][point.y];
                var positions = cell.GetPositionsOrDefault(cellGrid);

                if (positions != null)
                {
                    this.scoreManager.Moves--;
                    foreach (var position in positions)
                        if (cellGrid.Columns[position.x][position.y].Type == this.scoreManager.ObjectiveType)
                            this.scoreManager.ObjectiveCount--;
                    
                    CellGridOperation.RemoveCells(cellGrid, positions);
                }
            }
            else if (cellGrid.Events.Count == 0)
                CellGridOperation.Fill(cellGrid);
            else if (cellGrid.Events.Count > 0)
                this.visualGrid.Sync(cellGrid);
                
            this.visualGrid.Cascade();
        }
    }
}