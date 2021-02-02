using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesHolder : MonoBehaviour
{
    private Tile _baseTile;
    private Tile _wallTile;
    private Tile _keyTile;
    private Tile _doorTile;
    private Tile _startTile;
    private Tile _endTile;

    private void Awake()
    {
        _baseTile = (Tile)Resources.Load("Base", typeof(Tile));
        _wallTile = (Tile)Resources.Load("Wall", typeof(Tile));
        _keyTile = (Tile)Resources.Load("Key", typeof(Tile));
        _doorTile = (Tile)Resources.Load("Door", typeof(Tile));
        _startTile = (Tile)Resources.Load("Start", typeof(Tile));
        _endTile = (Tile)Resources.Load("End", typeof(Tile));

    }
    public Tile GetBaseTile()
    {
        return _baseTile;
    }

    public Tile GetWallTile()
    {
        return _wallTile;
    }

    public Tile GetKeyTile()
    {
        return _keyTile;
    }

    public Tile GetDoorTile()
    {
        return _doorTile;
    }

    public Tile GetStartTile()
    {
        return _startTile;
    }

    public Tile GetEndTile()
    {
        return _endTile;
    }
}
