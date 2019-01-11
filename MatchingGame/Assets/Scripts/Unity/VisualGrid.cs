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
        private List<List<GameObject>> columns = new List<List<GameObject>>();
        private Queue<GameObject> removeBuffer = new Queue<GameObject>();

        public List<List<GameObject>> Columns { get { return columns; } set { columns = value; } }
        public Queue<GameObject> RemoveBuffer { get { return removeBuffer; } set { removeBuffer = value; } }

        public void Build(CellGrid grid)
        {
            for (var x = 0; x < grid.Width; x++)
                columns.Add(new List<GameObject>());
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
            
            ExecuteRemoveBuffer();
        }

        public void Cascade()
        {
            foreach (var column in columns)
                foreach (var block in column)
                {
                    var x = columns.IndexOf(column);
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
            var block = ToBlock(blockType);
            block = Instantiate(block);

            var blockTransform = block.GetComponent<RectTransform>();
            var startPosition = new Vector3(index, grid.Height);
            blockTransform.SetParent(parentTransform, false);
            blockTransform.position = startPosition;
            columns[index].Add(block);
        }

        private void ExecuteRemoveBuffer()
        {
            while (removeBuffer.Count > 0)
            {
                var block = removeBuffer.Dequeue();
                var columnQuery =   
                    from column in columns
                    where column.Contains(block)
                    select column;

                var containingColumn = columnQuery.SingleOrDefault();

                if (containingColumn == null)
                    throw new InvalidGridException("The block does not exist within the grid.");
                else
                {
                    var columnX = columns.IndexOf(containingColumn);
                    var columnY = containingColumn.IndexOf(block);
                    var soundManager = GetComponent<SoundManager>();
                    var randomNumber = Random.Range(0, soundManager.BlockBreakSounds.Count);
                    var soundClip = soundManager.BlockBreakSounds[randomNumber];
                    
                    Destroy(columns[columnX][columnY]);
                    columns[columnX].Remove(block);
                    if (!soundManager.GetComponent<AudioSource>().isPlaying)
                        soundManager.PlaySound(soundClip);
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