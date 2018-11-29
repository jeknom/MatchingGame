using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    private const int _width = 5;
    private const int _height = 6;
    private const float CascadeSpeed = 0.5f;
    private List<List<IBlock>> _blockGrid = new List<List<IBlock>>();
    
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public bool IsCascading { get { return _isCascading(); } }
    public bool IsMissingBlocks { get { return _isMissingBlocks(); } }
    public List<List<IBlock>> Blocks { get { return _blockGrid; } }

    public GameGrid()
    {
        for (var x = 0; x < _width; x++)
            _blockGrid.Add(new List<IBlock>());
    }

    public void RemoveBlock(IBlock block)
    {
        var queue = new Queue<Vector3>();
        queue.Enqueue(PositionOnGrid(block));

        var toBeDestroyed = new List<IBlock>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if ((BlockIsValid(current) && _blockGrid[(int)current.x][(int)current.y].GetMaterial() == block.GetMaterial()) && (!toBeDestroyed.Contains(_blockGrid[(int)current.x][(int)current.y])))
            {
                toBeDestroyed.Add(_blockGrid[(int)current.x][(int)current.y]);
                queue.Enqueue(new Vector3(current.x - 1, current.y));
                queue.Enqueue(new Vector3(current.x + 1, current.y));
                queue.Enqueue(new Vector3(current.x, current.y - 1));
                queue.Enqueue(new Vector3(current.x, current.y + 1));
            }
        }
        RemoveBlock(toBeDestroyed);
    }

    public void RemoveBlock(List<IBlock> blocks)
    {
        foreach (var block in blocks)
        {
            block.Destroy();
            _blockGrid.ForEach(b => b.Remove(block));
        }
    }

    private bool BlockIsValid(Vector3 position)
    {
        if ((position.x < _width && position.x >= 0) && (position.y < _height && position.y >= 0))
            return true;

        return false;
    }

    public IEnumerator Cascade()
    {
        while (_isCascading())
        {
            foreach (var row in _blockGrid)
                row.ForEach(b => b.GetObject().transform.position = Vector3.MoveTowards(b.GetObject().transform.position, PositionOnGrid(b), CascadeSpeed));
            
            yield return null;
        }
    }

    private bool _isCascading()
    {
        foreach (var row in _blockGrid)
            foreach (var block in row)
            {
                if (block.GetObject().transform.position != PositionOnGrid(block))
                    return true;
            }

        return false;
    }

    private bool _isMissingBlocks()
    {
        foreach (var row in _blockGrid)
            if (row.Count != _height)
                return true;

        return false;
    }

    private Vector3 PositionOnGrid(IBlock block)
    {
        foreach (var row in _blockGrid)
            if (row.Contains(block))
                return new Vector3 (_blockGrid.IndexOf(row), row.IndexOf(block));

        throw new InvalidOperationException("The block needs to be present on the grid.");
    }
}