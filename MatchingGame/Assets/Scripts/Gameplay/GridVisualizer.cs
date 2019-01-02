namespace MatchingGame
{
    using UnityEngine;
    using System.Collections.Generic;

    public class GridVisualizer : MonoBehaviour
    {
        private List<List<GameObject>> objectColumns = new List<List<GameObject>>();
        [SerializeField] GameObject square;
        [SerializeField] GameObject blackBomb;
        [SerializeField] RectTransform parentTransform;
        [SerializeField] float cascadeSpeed = 10;

        private void Start() 
        {
            for (var i = 0; i < GameGrid.Width; i++)
                objectColumns.Add(new List<GameObject>());
        }

        private void Update() 
        {
            if (!IsSynced() && IsCascaded())
                SynchronizeColumns();
            Cascade();
        }

        private void Cascade()
        {
            foreach (var column in objectColumns)
            {
                for (var y = 0; y < column.Count; y++)
                {
                    var destination = new Vector3(objectColumns.IndexOf(column), y);
                    column[y].transform.position = Vector3.MoveTowards(column[y].transform.position, destination, cascadeSpeed);
                }
            }
        }

        private void SynchronizeColumns()
        {
            foreach (var column in objectColumns)
                column.Clear();

            for (var x = 0; x < GameGrid.Width; x++)
                foreach (var block in GameGrid.Columns[x])
                {
                    var convertedBlock = Instantiate(GetBlockType(block));
                    convertedBlock.GetComponent<RectTransform>().SetParent(parentTransform, false);
                    convertedBlock.GetComponent<RectTransform>().position = new Vector3(x, GameGrid.Columns[x].Count, -1);
                    objectColumns[x].Add(convertedBlock);
                }
        }

        private GameObject GetBlockType(IBlock block)
        {
            if (block.blockType == BlockType.Bomb)
                return blackBomb;
            else
                return square;
        }

        private bool IsSynced()
        {
            for (var i = 0; i < GameGrid.Width; i++)
                if (objectColumns[i].Count != GameGrid.Columns[i].Count)
                    return false;

            return true;
        }

        private bool IsCascaded()
        {
            foreach (var column in objectColumns)
                foreach (var blockObject in column)
                    if (blockObject.GetComponent<RectTransform>().position != new Vector3(objectColumns.IndexOf(column), column.IndexOf(blockObject)))
                        return false;

            return true;
        }
    }
}