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
        if (IsCascading())
            foreach (var row in this.Blocks)
                row.ForEach(b => b.GetObject().transform.position = Vector3.MoveTowards(b.GetObject().transform.position, PositionToVector(b), CascadeSpeed * Time.deltaTime));
    }

    public void DestroyBlocks(List<IBlock> blocks)
    {
        foreach (var block in blocks)
            this.Blocks.ForEach(b => { b.Remove(block); Destroy(block.GetObject()); });
    }

    public bool IsValidLocation(Vector3 position)
    {
        if ((position.x < Width && position.x >= 0) && (position.y < Height && position.y >= 0))
            return true;

        return false;
    }

    private bool IsCascading()
    {
        foreach (var row in this.Blocks)
        {
            if (row.Count < Height)
                return true;

            foreach (var block in row)
            {
                if (block.GetObject().transform.position != PositionToVector(block))
                    return true;
            }
        }
        return false;
    }

    public Vector3 PositionToVector(IBlock block)
    {
        foreach (var row in this.Blocks)
            if (row.Contains(block))
                return new Vector3 (this.Blocks.IndexOf(row), row.IndexOf(block));

        throw new InvalidOperationException("The block needs to be present on the grid.");
    }

    public IBlock PositionToBlock(Vector3 position)
    {
        foreach (var row in this.Blocks)
            if (row.Contains(this.Blocks[(int)position.x][(int)position.y]))
                return this.Blocks[(int)position.x][(int)position.y];

        throw new InvalidOperationException("The grid does not contain a block that would match the Vector.");
    }
}