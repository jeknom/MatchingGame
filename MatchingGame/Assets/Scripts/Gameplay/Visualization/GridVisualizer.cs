namespace MatchingGame.Visualization
{
    using UnityEngine;
    using System.Linq;
    using System.Collections.Generic;
    using MatchingGame.Logic;

    public class GridVisualizer : MonoBehaviour
    {
        private static List<List<GameObject>> visualColumns = new List<List<GameObject>>();
        [SerializeField] private RectTransform parentTransform;
        [SerializeField] private GameObject square;
        [SerializeField] private GameObject blackBomb;
        [SerializeField] private float cascadeSpeed = 10;

        public List<List<GameObject>> VisualColumns { get { return visualColumns; } }

        public void Build(GameGrid grid)
        {
            for (var i = 0; i < grid.Width; i++)
                visualColumns.Add(new List<GameObject>());
        }

        public void Sync(GameGrid grid)
        {
            foreach (var column in grid.Columns)
                foreach (var block in column)
                {
                    var index = new Point { x = grid.Columns.IndexOf(column), y = column.IndexOf(block) };
                    GameObject visualBlock;

                    // Destroys non-existing visual blocks.
                    if (visualColumns[index.x].Count > grid.Columns[index.x].Count)
                    {
                        visualBlock = visualColumns[index.x][index.y];
                        var blockType = GridQuery.PointToBlockType(grid, index);
                        var visualBlockType = VisualToBlockType(visualBlock);
                        if (blockType != visualBlockType)
                        {
                            visualColumns[index.x].Remove(visualBlock);
                            Destroy(visualBlock);
                        }
                    }

                    // Checks for missing visual blocks and adds them.
                    if (visualColumns[index.x].Count < grid.Columns[index.x].Count)
                    {
                        var countPoint = new Point { x = index.x, y = visualColumns[index.x].Count };
                        var blockTypeAtIndex = GridQuery.PointToBlockType(grid, countPoint);
                        var startPosition = new Vector3(index.x, grid.Height);
                        visualBlock = BlockTypeToVisual(blockTypeAtIndex);
                        
                        visualBlock = Instantiate(visualBlock);
                        visualBlock.GetComponent<RectTransform>().SetParent(parentTransform, false);
                        visualBlock.GetComponent<RectTransform>().position = startPosition;
                        visualColumns[index.x].Add(visualBlock);
                    }
                }
        }

        public void Cascade()
        {
            foreach (var column in visualColumns)
                foreach (var block in column)
                {
                    var index = new Point { x = visualColumns.IndexOf(column), y = column.IndexOf(block) };
                    var destination = new Vector3(index.x, index.y);
                    var currentTransform = visualColumns[index.x][index.y].GetComponent<RectTransform>();
                    destination = Vector3.MoveTowards(currentTransform.position, destination, cascadeSpeed * Time.deltaTime);
                    currentTransform.position = destination;
                }
        }

        private bool IsCascading()
        {
            foreach (var column in visualColumns)
                foreach (var block in column)
                {
                    var index = new Point { x = visualColumns.IndexOf(column), y = column.IndexOf(block) };
                    var currentTransform = visualColumns[index.x][index.y].GetComponent<RectTransform>();
                    var destination = new Vector3(index.x, index.y);
                    
                    if (currentTransform.position != destination)
                        return true;
                }
            return false;
        }

        private GameObject BlockTypeToVisual(BlockType type)
        {
            if (type == BlockType.Bomb)
                return blackBomb;
            else
                return square;
        }

        private BlockType VisualToBlockType(GameObject visualBlock)
        {
            if (visualBlock == null)
                throw new InvalidVisualException("Cannot turn null into a BlockType");

            if (visualBlock.tag == "BlackBomb")
                return BlockType.Bomb;
            else
                return BlockType.Square;
        }

        public static Point VisualBlockToPoint(GridVisualizer grid, GameObject visualBlock)
        {
            if (visualBlock == null)
                throw new InvalidVisualException("Cannot turn null into a point");

            if (grid.VisualColumns.Where(b => b.Contains(visualBlock)).SingleOrDefault() == null)
                throw new InvalidVisualException("Cannot turn block into a point because it isn't present on the VisualGrid");

            Point point;
            foreach (var column in grid.VisualColumns)
                foreach (var block in column)
                {
                    point.x = grid.VisualColumns.IndexOf(column);
                    if (column.Contains(visualBlock))
                    {
                        point.y = column.IndexOf(block);
                        return point;
                    }
                }
            throw new InvalidVisualException("An unexpected error has occurred, the block could not be turned into a point");
        }
    }
}