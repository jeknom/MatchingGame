﻿using System.Linq;
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
        private List<Point> unremovedBlocks = new List<Point>();

        public List<Point> UnremovedBlocks { get { return unremovedBlocks; } set { unremovedBlocks = value; } }
        
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

            DestroyCellsAt(UnremovedBlocks);
            UnremovedBlocks.Clear();
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

        public void DestroyCellsAt(List<Point> points)
        {
            var blocks = new List<GameObject>();
            foreach (var point in points)
                blocks.Add(visualColumns[point.x][point.y]);

            foreach (var block in blocks)
            {
                var columnQuery =   
                    from column in visualColumns
                    where column.Contains(block)
                    select column;

                var containingColumn = columnQuery.SingleOrDefault();

                if (containingColumn == null)
                    throw new InvalidGridException("The block does not exist within the grid.");
                else
                {
                    var columnX = visualColumns.IndexOf(containingColumn);
                    var columnY = containingColumn.IndexOf(block);
                    
                    Destroy(visualColumns[columnX][columnY]);
                    visualColumns[columnX].Remove(block);
                }
            }
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