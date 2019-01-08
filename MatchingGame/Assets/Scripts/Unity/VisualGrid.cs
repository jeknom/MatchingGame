using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchingGame
{
    public class VisualGrid : MonoBehaviour
    {
        [SerializeField] private float cascadeSpeed;
        [SerializeField] private RectTransform parentTransform;
        [SerializeField] private GameObject basicBlock;
        [SerializeField] private GameObject bombBlock;
        private List<List<GameObject>> visualColumns = new List<List<GameObject>>();

        public int MyProperty { get; set; }
        
        public List<List<GameObject>> VisualColumns
        { 
            get { return visualColumns; } 
            set { visualColumns = value; }
        }

        public void Build(CellGrid grid)
        {
            for (var x = 0; x < grid.Width; x++)
                visualColumns.Add(new List<GameObject>());
        }

        public void Sync(CellGrid grid)
        {
            if (grid.Events.Count <= 0)
                throw new InvalidVisualException("There is nothing to sync.");

            while (grid.Events.Count > 0)
            {
                var latestEvent = grid.Events.Dequeue();
                latestEvent.Unload(grid, this);
            }
        }

        public void Cascade()
        {
            foreach (var column in visualColumns)
                foreach (var block in column)
                {
                    var x = visualColumns.IndexOf(column);
                    var y = column.IndexOf(block);
                    var destination = new Vector3(x, y);
                    var currentTransform = block.GetComponent<RectTransform>();
                    
                    if (currentTransform.position != destination)
                    {
                        var nextPosition = Vector3.MoveTowards(currentTransform.position, destination, cascadeSpeed * Time.deltaTime);
                        currentTransform.position = nextPosition;
                    }
                }
        }

        public void InstantiateCellAt(BlockType blockType, int index)
        {
            var block = ToBlock(blockType);
            block = Instantiate(block);

            var blockTransform = block.GetComponent<RectTransform>();
            var startPosition = new Vector3(index, visualColumns[index].Count);
            blockTransform.SetParent(parentTransform, false);
            blockTransform.position = startPosition;
            visualColumns[index].Add(block);
        }

        public void DestroyCellAt(Point point)
        {
            Destroy(visualColumns[point.x][point.y]);
            visualColumns[point.x].RemoveAt(point.y);
        }

        private GameObject ToBlock(BlockType blockType)
        {
            GameObject block;

            if ((int)blockType < 4)
            {
                block = basicBlock;
                var image = block.GetComponent<Image>();

                if ((int)blockType == 0)
                    image.color = new Color32(255, 255, 0, 255);
                else if ((int)blockType == 1)
                    image.color = new Color32(255, 0, 0, 255);
                else if ((int)blockType == 2)
                    image.color = new Color32(0, 255, 0, 255);
                else if ((int)blockType == 3)
                    image.color = new Color32(0, 0, 255, 255);
                else
                    throw new InvalidVisualException("An unexpected error has occurred.");

                return block;
            }
            else if (blockType == BlockType.Bomb)
            {
                block = bombBlock;
                return block;
            }
            else
                throw new InvalidVisualException("Cannot turn non-existing BlockType into a GameObject.");
        }
    }   
}