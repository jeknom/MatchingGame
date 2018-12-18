using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame
{
    public class GameGrid : MonoBehaviour
    {
        private const int Width = 6;
        private const int Height = 10;
        private const float CascadeSpeed = 10f;
        private List<List<IBlock>> Columns = new List<List<IBlock>>();
        [SerializeField] private GameObject Square;
        [SerializeField] private GameObject BlackBomb;
        [SerializeField] private int BlackBombChance = 7;
        [SerializeField] private AudioClip DestroySound;

        public GameGrid()
        {
            for (var x = 0; x < Width; x++)
                this.Columns.Add(new List<IBlock>());
        }

        public void FillGrid()
        {
            foreach (var column in this.Columns)
                while (column.Count < Height)
                {
                    var instantiatedBlock = Instantiate(RandomizeBlock().GetObject);
                    instantiatedBlock.GetComponent<RectTransform>().SetParent(this.transform, false);
                    
                    var spawnPoint = new Vector3(Columns.IndexOf(column), 10, 0);
                    instantiatedBlock.GetComponent<RectTransform>().position = spawnPoint;
                    
                    column.Add(instantiatedBlock.GetComponent<IBlock>());
                }
        }

        public void Cascade()
        {
            foreach (var column in this.Columns)
                foreach (var block in column)
                {
                    var destination = new Vector3(Columns.IndexOf(column), column.IndexOf(block));
                    block.GetRectTransform = Vector3.MoveTowards(block.GetRectTransform, destination, CascadeSpeed * Time.deltaTime);
                }
        }

        public void DestroyBlocks(List<IBlock> blocks)
        {
            foreach (var block in blocks)
                this.Columns.ForEach(b => { b.Remove(block); Destroy(block.GetObject); });

            GetComponent<AudioSource>().PlayOneShot(DestroySound);
        }

        public List<IBlock> GetSurroundingBlocks(IBlock target, bool isHexagonal)
        {
            if (target == null)
                throw new InvalidBlockException("Cannot find surrounding blocks for a null object.");

            var targetX = this.Columns.Where(b => b.Contains(target)).Select(b => this.Columns.IndexOf(b)).SingleOrDefault();
            var targetY = this.Columns[targetX].IndexOf(target);

            var positions = new List<Point>();
            positions.Add(new Point { x = targetX + 1, y = targetY });
            positions.Add(new Point { x = targetX - 1, y = targetY });
            positions.Add(new Point { x = targetX, y = targetY + 1 });
            positions.Add(new Point { x = targetX, y = targetY - 1 });

            if (isHexagonal)
            {
                positions.Add(new Point { x = targetX + 1, y = targetY +1 });
                positions.Add(new Point { x = targetX - 1, y = targetY +1 });
                positions.Add(new Point { x = targetX + 1, y = targetY - 1 });
                positions.Add(new Point { x = targetX - 1, y = targetY - 1 });
            }

            var surrounding = new List<IBlock>();
            foreach (var position in positions)
                if ((position.x >= 0 && position.x < Width) && (position.y >= 0 && position.y < Height) 
                    && Columns[position.x].Count > position.y)
                        surrounding.Add(this.Columns[position.x][position.y]);

            return surrounding;
        }

        private IBlock RandomizeBlock()
        {
            var blocks = new List<IBlock>();
            var randomNumber = Random.Range(0, 100);

            if (randomNumber <= BlackBombChance)
                blocks.Add(BlackBomb.GetComponent<IBlock>());
            else
                blocks.Add(Square.GetComponent<IBlock>());

            return blocks[Random.Range(0, blocks.Count)];
        }
    }
}