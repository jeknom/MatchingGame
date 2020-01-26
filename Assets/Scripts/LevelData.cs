using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public bool hasBeenWon;
    }
}