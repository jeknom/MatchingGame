using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class GameGrid : MonoBehaviour
    {
        private List<List<IBlock>> columns = new List<List<IBlock>>();
        [SerializeField] private int width = 6;
        [SerializeField] private int height = 10;

        public List<List<IBlock>> Columns { get { return this.columns; } set {this.columns = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public GameGrid()
        {
            for (var x = 0; x < this.width; x++)
                this.columns.Add(new List<IBlock>());
        }
    }
}