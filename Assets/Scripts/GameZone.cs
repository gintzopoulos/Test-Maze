﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameZone : MonoBehaviour
{
    public bool lvl1Done = false;
    [SerializeField]
    private GameObject _playerGameObject;
    [SerializeField]
    private GameObject _keyGameObject;
    [SerializeField]
    private GameObject _doorGameObject;
    [SerializeField]
    private GameObject _exitGameObject;
    /*[SerializeField]
    public Canvas _NextLevelUI;*/
    

    private Transform _playerTransform;
    private Transform _keyTransform;
    private Transform _doorTransform;
    private Transform _exitTransform;
    
    private Tilemap _gameZoneTilemap_Ground;
    private Tilemap _gameZoneTilemap_Collider;

    private TilesHolder _tilesHolder;
 
    private readonly char[,] lvl1 = Level.level_1;
    private readonly char[,] lvl2 = Level.level_2;
    //public static bool nextUi;

    private void Awake()
    {
        _playerTransform = _playerGameObject.transform;
        _keyTransform = _keyGameObject.transform;
        _doorTransform = _doorGameObject.transform;
        _exitTransform = _exitGameObject.transform;
        _gameZoneTilemap_Ground = GameObject.Find("Ground").GetComponent<Tilemap>();
        _gameZoneTilemap_Collider = GameObject.Find("Collision").GetComponent<Tilemap>();
        _tilesHolder = GetComponent<TilesHolder>();
        //_NextLevelUI.GetComponent<Canvas>().enabled = false;

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

    //Generate the the Tilemap depending on Level's matrices
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
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetBaseTile());
                    Vector3 doorPoint = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);
                    _doorTransform.transform.position = doorPoint + new Vector3(1, -1, 0);
                    Spawn(_doorGameObject, _doorTransform);

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
                    _gameZoneTilemap_Ground.SetTile(currentCellPosition, _tilesHolder.GetBaseTile());
                    Vector3 endPoint = _gameZoneTilemap_Ground.CellToWorld(currentCellPosition);
                    _exitTransform.transform.position = endPoint + new Vector3(1, -1, 0);
                    Spawn(_exitGameObject, _exitTransform.transform);

                }
                currentCellPosition = new Vector3Int(
                    (int)(cellSize.x + currentCellPosition.x), currentCellPosition.y, origin.z);

            }
            currentCellPosition = new Vector3Int(origin.x, (int)(cellSize.y + currentCellPosition.y), origin.z);
        }
        _gameZoneTilemap_Ground.CompressBounds();
        
    }

    //Spawn prefabs in Tilemap
    private void Spawn(GameObject obj, Transform pos)
    {
        Instantiate(obj, pos);
    }
    //Destroy prefabs in Tilemap
    private void Destroy(GameObject obj)
    {
        Destroy(obj);
    }
    
}




