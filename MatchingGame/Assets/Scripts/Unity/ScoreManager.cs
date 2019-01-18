using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Image basicBlockImage;
    [SerializeField] private Text objectiveCounter;
    [SerializeField] private Text moveCounter;
    private int moves;
    private int objectiveCount;

    public int Moves
    { 
        get { return this.moves;} 
        set { this.moveCounter.text = value.ToString(); this.moves = value; }
    }

    public int ObjectiveCount
    { 
        get { return this.objectiveCount; } 
        set { this.objectiveCounter.text = value.ToString(); this.ObjectiveCount = value; }
    }
}
