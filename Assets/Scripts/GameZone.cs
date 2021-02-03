using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameZone : MonoBehaviour
{
    public bool lvl1Done = false;
    public GameObject _playerGameObject;
    public GameObject _keyGameObject;

    private Transform _playerTransform;
    private Transform _keyTransform;
    
    private const float CameraPositionModifier = 0.5f;
    private const float CameraSizeModifier = 1.2f;
  
    private Tilemap _gameZoneTilemap_Ground;
    private Tilemap _gameZoneTilemap_Collider;

    private TilesHolder _tilesHolder;
    private Level _gameData;
    private Camera _camera;

    
    //private Vector3 keyPoint;
    //private Vector3 doorPoint;


    private readonly char[,] lvl1 = Level.level_1;
    private readonly char[,] lvl2 = Level.level_2;

    private void Awake()
    {
        _playerTransform = _playerGameObject.transform;
        _keyTransform = _keyGameObject.transform;
        _gameZoneTilemap_Ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        _gameZoneTilemap_Collider = GameObject.Find("Collision").GetComponent<Tilemap>();
        _tilesHolder = GetComponent<TilesHolder>();
        //_gameData = FindObjectOfType<Level>();
        _camera = Camera.main;

    }

    private void Start()
    {     
        if (!lvl1Done)
        {
            InitialiseTileMap(lvl1);
        }
        else
        {
            InitialiseTileMap(lvl2);

        }
    }

    private void InitialiseTileMap(char[,] level)
    {
        var size_lvl = level.Length;
        
        var origin = _gameZoneTilemap_Ground.origin;
        var cellSize = _gameZoneTilemap_Ground.cellSize;
        _gameZoneTilemap_Ground.ClearAllTiles();
        var currentCellPosition = origin;
        var width = (int)Math.Sqrt(size_lvl);
        var height = (int)Math.Sqrt(size_lvl);
    
        for (var w = 0; w < width; w++)
        {
            for (var h = 0; h < height; h++)
            {
                if (level[h, w] == '0')
                {
                    _gameZoneTilemap_Collider.SetTile(currentCellPosition, _tilesHolder.GetWallTile());

                }
                else if (level[h, w] == '1')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetBaseTile());

                }
                else if (level[h, w] == 'k')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetBaseTile());
                    Vector3 keyPoint = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);
                    _keyTransform.transform.position = keyPoint + new Vector3(1, -1, 0);
                    Spawn(_keyGameObject, _keyTransform.transform);
                }
                else if (level[h, w] == 'd')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetDoorTile());
                    Vector3 doorPoint = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);

                }
                else if (level[h, w] == 's')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetStartTile());
                    Vector3 startPosition = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);
                    _playerTransform.transform.position = startPosition + new Vector3(1,-1,0);
                    Spawn(_playerGameObject, _playerTransform.transform);
                }
                else if (level[h, w] == 'e')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetEndTile());
                    Vector3 endPoint = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);
                }
                currentCellPosition = new Vector3Int(
                    (int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);

            }
            currentCellPosition = new Vector3Int(origin.x, (int)(cellSize.y + currentCellPosition.y), origin.z);
        }
        _gameZoneTilemap_Ground.CompressBounds();
        //ModifyCamera(width);
    }
    private void Spawn(GameObject obj, Transform pos)
    {
        Instantiate(obj, pos);
    }
    private void Destroy(GameObject obj)
    {
        Destroy(obj);
    }
    
}




