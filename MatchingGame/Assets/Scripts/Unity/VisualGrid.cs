using System.Linq;
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
        [SerializeField] private Color32 yellow;
        [SerializeField] private Color32 red;
        [SerializeField] private Color32 blue;
        [SerializeField] private Color32 green;
        [SerializeField] private Color32 defaultColour;
        private List<List<GameObject>> columns = new List<List<GameObject>>();
        private Queue<GameObject> removeBuffer = new Queue<GameObject>();

        public List<List<GameObject>> Columns { get { return this.columns; } }
        public Queue<GameObject> RemoveBuffer { get { return removeBuffer; } }

        public void Build(CellGrid grid)
        {
            Debug.Assert(grid != null, "Cannot build from a null CellGrid.");

            for (var x = 0; x < grid.Width; x++)
                this.columns.Add(new List<GameObject>());
        }

        public void Sync(CellGrid grid)
        {
            Debug.Assert(grid.Events.Count > 0, "There is nothing to sync.");

            while (grid.Events.Count > 0)
            {
                var latestEvent = grid.Events.Dequeue();
                latestEvent.Process(grid, this);
            }
            
            ExecuteRemoveBuffer();
        }

        public void Cascade()
        {
            foreach (var column in this.columns)
                foreach (var block in column)
                {
                    var x = this.columns.IndexOf(column);
                    var y = column.IndexOf(block);
                    var destination = new Vector3(x, y);
                    var currentTransform = block.GetComponent<RectTransform>();

                    if (currentTransform.position != destination)
                    {
                        var nextPosition = Vector3.MoveTowards(currentTransform.position, destination, cascadeSpeed * Time.smoothDeltaTime);
                        currentTransform.position = nextPosition;
                    }
                }
        }

        public void InstantiateCellAt(CellGrid grid, BlockType blockType, int index)
        {
            Debug.Assert(grid != null, "Cannot instantiate from a null CellGrid.");
            Debug.Assert(index >= 0 && index < grid.Width, "The CellGrid does not contain that many columns.");

            var block = Instantiate(ToBlock(blockType));
            var blockTransform = block.GetComponent<RectTransform>();
            var startPosition = new Vector3(index, grid.Height);
            
            blockTransform.SetParent(this.parentTransform, false);
            blockTransform.position = startPosition;
            this.columns[index].Add(block);
        }

        private void ExecuteRemoveBuffer()
        {
            while (this.removeBuffer.Count > 0)
            {
                var block = this.removeBuffer.Dequeue();
                var columnQuery =   
                    from column in this.columns
                    where column.Contains(block)
                    select column;

                var containingColumn = columnQuery.Single();
                var columnX = this.columns.IndexOf(containingColumn);
                var columnY = containingColumn.IndexOf(block);
                
                Destroy(this.columns[columnX][columnY]);
                this.columns[columnX].Remove(block);
            }
        }

        private GameObject ToBlock(BlockType blockType)
        {
            GameObject block = this.basicBlock;
            Color32 colour = new Color32();

            if (blockType == BlockType.Yellow)
                colour = this.yellow;
            else if (blockType == BlockType.Red)
                colour = this.red;
            else if (blockType == BlockType.Green)
                colour = this.green;
            else if (blockType == BlockType.Blue)
                colour = this.blue;
            else if (blockType == BlockType.Bomb)
            {
                colour = this.defaultColour;
                block = bombBlock;
            }
            
            block.GetComponent<Image>().color = colour;

            return block;
        }
    }   
}