using UnityEngine;
using UnityEngine.UI;

namespace MatchingGame
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private Image basicBlockImage;
        [SerializeField] private Text objectiveCounter;
        [SerializeField] private Text moveCounter;
        private int moves;
        private int objectiveCount = 0;
        private BlockType objectiveType = BlockType.Bomb;

        public BlockType ObjectiveType
        { 
            get
            {
                return this.objectiveType;
            }
            set
            {
                this.objectiveType = value;
                basicBlockImage.color = VisualOperation.ToColor(this.objectiveType);
            }
        }

        public int Moves
        { 
            get { return this.moves;} 
            set { this.moves = value; this.moveCounter.text = this.moves.ToString(); }
        }

        public int ObjectiveCount
        { 
            get { return this.objectiveCount; } 
            set { this.objectiveCount = value; this.objectiveCounter.text = this.ObjectiveCount.ToString(); }
        }
    }
}