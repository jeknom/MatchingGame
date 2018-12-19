using UnityEngine;

namespace MatchingGame
{
    public class ObjectiveCounter
    {
        private int moves;
        private int squareCount;
        private int blackBombCount;

        public int Moves 
        { 
            get { return moves; } 
            set { moves = value; }
        }
        public int SquareCount { get { return squareCount; } set { squareCount = value; } }
        public int BlackBombCount { get { return blackBombCount; } set { blackBombCount = value; } }

        public ObjectiveCounter()
        {
            this.moves = Random.Range(10, 20);
            this.squareCount = Random.Range(10, 100);
            this.blackBombCount = Random.Range(10, 30);
        }

        public ObjectiveCounter(int moves, int squareCount, int blackBombCount)
        {
            this.moves = moves;
            this.squareCount = squareCount;
            this.blackBombCount = blackBombCount;
        }
    }
}