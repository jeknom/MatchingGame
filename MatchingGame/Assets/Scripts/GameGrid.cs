using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private const int Width = 6;
    private const int Height = 11;
    private const float CascadeSpeed = 10;
    private List<List<IBlock>> Blocks = new List<List<IBlock>>();
    [SerializeField] private Vector3 SpawnPoint;
    [SerializeField] private Square Square;

    public GameGrid()
    {
        for (var x = 0; x < Width; x++)
            this.Blocks.Add(new List<IBlock>());
    }

    public void FillGrid()
    {
        foreach (var row in this.Blocks)
            while (row.Count < Height)
            {
                var instantiatedBlock = Instantiate(this.Square, this.SpawnPoint, Quaternion.identity);
                instantiatedBlock.transform.parent = gameObject.transform;
                row.Add(instantiatedBlock);
            }
    }

    public void Cascade()
    {
        foreach (var row in this.Blocks)
            foreach (var block in row)
            {
                var destination = new Vector3(Blocks.IndexOf(row), row.IndexOf(block));
                block.GetTransform = Vector3.MoveTowards(block.GetTransform, destination, CascadeSpeed * Time.deltaTime);
            }
    }

    public void DestroyBlocks(List<IBlock> blocks)
    {
        foreach (var block in blocks)
            this.Blocks.ForEach(b => { b.Remove(block); Destroy(block.GetObject); });
    }

    public List<IBlock> GetSurroundingBlocks(IBlock target, bool hexagonal)
    {
        var targetX = this.Blocks.Where(b => b.Contains(target)).Select(b => this.Blocks.IndexOf(b)).SingleOrDefault();
        var targetY = this.Blocks[targetX].IndexOf(target);

        var positions = new List<Point>();
        positions.Add(new Point { x = targetX + 1, y = targetY });
        positions.Add(new Point { x = targetX - 1, y = targetY });
        positions.Add(new Point { x = targetX, y = targetY + 1 });
        positions.Add(new Point { x = targetX, y = targetY - 1 });

        if (hexagonal)
        {
            positions.Add(new Point { x = targetX + 1, y = targetY +1 });
            positions.Add(new Point { x = targetX - 1, y = targetY +1 });
            positions.Add(new Point { x = targetX + 1, y = targetY - 1 });
            positions.Add(new Point { x = targetX - 1, y = targetY - 1 });
        }

        var surrounding = new List<IBlock>();
        foreach (var position in positions)
            if ((position.x >= 0 && position.x < Width) && (position.y >= 0 && position.y < Height))
                surrounding.Add(this.Blocks[position.x][position.y]);

        return surrounding;
    }
}