using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class GameLogic : MonoBehaviour
    {
        private Controls controls = new Controls();
        private CellGrid cellGrid = new CellGrid(6, 8);
        private VisualGrid visualGrid;

        private void Start()
        {
            visualGrid = GetComponent<VisualGrid>();
            visualGrid.Build(cellGrid);
        }

        private void Update()
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
    }
}