using System.Linq;
using System.Collections.Generic;

namespace MatchingGame.Logic
{
    public static class GridQuery
    {
        public static List<IBlock> GetSurroundingBlocks(GameGrid grid, IBlock target, bool isHexagonal)
        {
            if (target == null)
                throw new InvalidBlockException("Cannot find surrounding blocks for a null object.");

            var targetX = grid.Columns.Where(b => b.Contains(target)).Select(b => grid.Columns.IndexOf(b)).SingleOrDefault();
            var targetY = grid.Columns[targetX].IndexOf(target);

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
                if ((position.x >= 0 && position.x < grid.Width) && (position.y >= 0 && position.y < grid.Height) 
                    && grid.Columns[position.x].Count > position.y)
                        surrounding.Add(grid.Columns[position.x][position.y]);

            return surrounding;
        }

        public static BlockType PointToBlockType(GameGrid grid, Point position)
        {
            return grid.Columns[position.x][position.y].blockType;
        }
    }
}