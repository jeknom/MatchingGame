using System.Linq;
using System.Collections.Generic;

namespace MatchingGame
{
    public static class GridQuery
    {
        public static List<IBlock> GetSurroundingBlocks(GameGrid gameGrid, IBlock target, bool isHexagonal)
        {
            if (target == null)
                throw new InvalidBlockException("Cannot find surrounding blocks for a null object.");

            var targetX = gameGrid.Columns.Where(b => b.Contains(target)).Select(b => gameGrid.Columns.IndexOf(b)).SingleOrDefault();
            var targetY = gameGrid.Columns[targetX].IndexOf(target);

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
                if ((position.x >= 0 && position.x < gameGrid.Width) && (position.y >= 0 && position.y < gameGrid.Height) 
                    && gameGrid.Columns[position.x].Count > position.y)
                        surrounding.Add(gameGrid.Columns[position.x][position.y]);

            return surrounding;
        }
    }
}