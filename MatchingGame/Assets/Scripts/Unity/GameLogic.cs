using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class GameLogic : MonoBehaviour
    {
        private Controls controls = new Controls();
        private CellGrid cellGrid = new CellGrid();
        private VisualGrid visualGrid;

        private void Start()
        {
            visualGrid = GetComponent<VisualGrid>();
            visualGrid.Build(cellGrid);
        }

        private void Update()
        {
            try
            {
                if (controls.GetInteraction() != null && cellGrid.Events.Count == 0)
                {
                    var block = controls.GetInteraction();
                    var point = GridQuery.ToPoint(visualGrid, block);
                    cellGrid.Columns[point.x][point.y].Activate(cellGrid);
                }
                else if (cellGrid.Events.Count == 0)
                    GridOperation.Fill(cellGrid);
                else if (cellGrid.Events.Count > 0)
                    visualGrid.Sync(cellGrid);
                    
                visualGrid.Cascade();
            }
            catch (InvalidGridException gridException)
            {
                Debug.Log("An unexpected error has occurred with the grid: " + gridException);
            }
            catch (InvalidEventException eventException)
            {
                Debug.Log("An unexpected error has occurred with one of the grid events: " + eventException);
            }
            catch (InvalidVisualException visualException)
            {
                Debug.Log("An unexpected error has occurred with the game visuals: " + visualException);
            }
            catch (Exception exception)
            {
                Debug.Log("An unexpected error has occurred: " + exception);
            }
        }
    }
}