using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class EditorGizmos : MonoBehaviour
    {
        [SerializeField] private int gridWidth = 6;
        [SerializeField] private int gridHeight = 6;

        private void OnDrawGizmos() 
        {
            var cellGrid = new CellGrid(gridWidth, gridHeight);
            CellGridOperation.Fill(cellGrid);

            foreach (var column in cellGrid.Columns)
                foreach (var cell in column)
                {
                    var point = GridQuery.ToPoint(cellGrid, cell);
                    var center = new Vector3(point.x, point.y);
                    var size = new Vector3(1, 1, 1);
                    
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireCube(center, size);
                }
        }
    }
}