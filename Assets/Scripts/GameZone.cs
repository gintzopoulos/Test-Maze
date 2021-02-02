using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameZone : MonoBehaviour
{
    public bool lvl1Done = false;
    public Transform _player;

    private const float CameraPositionModifier = 0.5f;
    private const float CameraSizeModifier = 1.2f;
  
    private Tilemap _gameZoneTilemap_Ground;
    private Tilemap _gameZoneTilemap_Collider;

    private TilesHolder _tilesHolder;
    private Level _gameData;
    private Camera _camera;
    private Vector3 startPoint;


    private readonly char[,] lvl1 = Level.level_1;
    private readonly char[,] lvl2 = Level.level_2;

    private void Awake()
    {
        
        _gameZoneTilemap_Ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        _gameZoneTilemap_Collider = GameObject.Find("Collision").GetComponent<Tilemap>();
        //_player = GameObject.Find("Player").GetComponent<GameObject>();
        _tilesHolder = GetComponent<TilesHolder>();
        _gameData = FindObjectOfType<Level>();
        _camera = Camera.main;

    }

    private void Start()
    {
        //InitialiseTileMap(lvl1);
        if (!lvl1Done)
        {
            InitialiseTileMap(lvl1);
            //InitialiseCollider(lvl1);
        }
        else
        {
            InitialiseTileMap(lvl2);

        }
        //GameObject p = _player;
        //Debug.Log(p.transform.position);
        //p.transform.position = Vector3.MoveTowards(transform.position, startPoint, 2f * Time.deltaTime);
        


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
        startPoint = origin;
    
        for (var w = 0; w < width; w++)
        {
            for (var h = 0; h < height; h++)
            {
                //Debug.Log(level[h, w]);

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
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetKeyTile());

                }
                else if (level[h, w] == 'd')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetDoorTile());

                }
                else if (level[h, w] == 's')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetStartTile());
                    Vector3 startPosition = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);
                    _player.transform.position = startPosition + new Vector3(1,-1,0);
                    
                    Debug.Log("startPoint" + startPoint);
                    Debug.Log("origin" + origin);
                    Debug.Log("CurrentCellPosition" + currentCellPosition);
                    Debug.Log("CurrentCellWorldPosition" + startPosition);
                }
                else if (level[h, w] == 'e')
                {
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetEndTile());

                }
                currentCellPosition = new Vector3Int(
                    (int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);

            }
            currentCellPosition = new Vector3Int(origin.x, (int)(cellSize.y + currentCellPosition.y), origin.z);
        }
        _gameZoneTilemap_Ground.CompressBounds();

        //ModifyCamera(width);
    }



    /*private void InitialiseCollider(char[,] lvl1)
    {

    }*/

    private void ModifyCamera(int width)
    {
        var modifier = (width - 4) * CameraPositionModifier;
        _camera.transform.position = new Vector3(
            _camera.transform.position.x + modifier,
            _camera.transform.position.y + modifier,
            _camera.transform.position.z
        );
        _camera.orthographicSize = Mathf.Pow(CameraSizeModifier, (width - 4)) * _camera.orthographicSize;
    }
}




