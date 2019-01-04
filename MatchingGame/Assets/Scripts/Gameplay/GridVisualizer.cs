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
            Cascade();
            AddMissing();
            //RemoveNonexisting();
        }

        private void Cascade()
        {
            foreach (var column in objectColumns)
                foreach (var blockObject in column)
                {
                    var index = new Point{ x = objectColumns.IndexOf(column), y = column.IndexOf(blockObject)};
                    var destination = new Vector3(index.x, index.y);
                    objectColumns[index.x][index.y].GetComponent<RectTransform>().position = Vector3.MoveTowards(blockObject.transform.position, destination, cascadeSpeed);
                }
        }

        private void AddMissing()
        {
            foreach (var column in GameGrid.Columns)
                foreach (var block in column)
                {
                    var index = new Point{ x = GameGrid.Columns.IndexOf(column), y = column.IndexOf(block)};
                    
                    if (objectColumns[index.x].Count < GameGrid.Columns[index.x].Count)
                    {
                        var instantiatedObject = Instantiate(ToBlockType(GameGrid.Columns[index.x][index.y]));
                        instantiatedObject.GetComponent<RectTransform>().SetParent(parentTransform, false);
                        objectColumns[index.x].Add(instantiatedObject);
                    }
                }
        }

        private void RemoveNonexisting()
        {
            foreach (var column in GameGrid.Columns)
                foreach (var block in column)
                {
                    var index = new Point{ x = GameGrid.Columns.IndexOf(column), y = column.IndexOf(block)};
                    if (GameGrid.Columns[index.x][index.y].blockType != ToBlockType(objectColumns[index.x][index.y]))
                    {
                        var current = objectColumns[index.x][index.y];
                        objectColumns[index.x].Remove(current);
                        Destroy(current);
                    }
                }
        }

        private BlockType ToBlockType(GameObject block)
        {
            if (block.tag == "BlackBomb")
                return BlockType.Bomb;
            else
                return BlockType.Square;
        }

        private GameObject ToBlockType(IBlock block)
        {
            if (block.blockType == BlockType.Bomb)
                return blackBomb;
            else
                return square;
        }
    }
}