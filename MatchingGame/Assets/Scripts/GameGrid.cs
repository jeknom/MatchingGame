using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private const int _width = 6;
    private const int _height = 11;
    private const float CascadeSpeed = 10;
    private List<List<IBlock>> _blockGrid = new List<List<IBlock>>();
    [SerializeField] private Vector3 SpawnPoint;
    [SerializeField] private Square square;
    
    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public bool NeedsCascading { get { return _needsCascading(); } }
    public bool IsMissingBlocks { get { return _isMissingBlocks(); } }
    public List<List<IBlock>> Blocks { get { return _blockGrid; } }

    public GameGrid()
    {
        for (var x = 0; x < _width; x++)
            _blockGrid.Add(new List<IBlock>());
    }

    public void AddMissingBlocks()
    {
        foreach (var row in _blockGrid)
            while (row.Count < _height)
            {
                var instantiatedBlock = Instantiate(square, SpawnPoint, Quaternion.identity);
                instantiatedBlock.transform.parent = gameObject.transform;
                row.Add(instantiatedBlock);
            }
    }

    public void FloodRemove(GameObject squareObject)
    {
        if (squareObject == null && squareObject.GetComponent<Square>() == null)
            throw new InvalidOperationException("Flood remove cannot be used on null objects. Make sure that the GameObject has a Square script component.");

        var square = squareObject.GetComponent<Square>();
        var queue = new Queue<Vector3>();
        queue.Enqueue(PositionOnGrid(square));

        var toBeDestroyed = new List<IBlock>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if ((BlockIsValid(current) && _blockGrid[(int)current.x][(int)current.y].GetMaterial() == square.GetMaterial()) && (!toBeDestroyed.Contains(_blockGrid[(int)current.x][(int)current.y])))
            {
                toBeDestroyed.Add(_blockGrid[(int)current.x][(int)current.y]);
                queue.Enqueue(new Vector3(current.x - 1, current.y));
                queue.Enqueue(new Vector3(current.x + 1, current.y));
                queue.Enqueue(new Vector3(current.x, current.y - 1));
                queue.Enqueue(new Vector3(current.x, current.y + 1));
            }
        }
        
        foreach (var squares in toBeDestroyed)
        {
            squares.Destroy();
            _blockGrid.ForEach(b => b.Remove(squares));
        }
    }

    private bool BlockIsValid(Vector3 position)
    {
        if ((position.x < _width && position.x >= 0) && (position.y < _height && position.y >= 0))
            return true;

        return false;
    }

    public void Cascade()
    {
        if (_needsCascading())
            foreach (var row in _blockGrid)
                row.ForEach(b => b.GetObject().transform.position = Vector3.MoveTowards(b.GetObject().transform.position, PositionOnGrid(b), CascadeSpeed * Time.deltaTime));
    }

    private bool _needsCascading()
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