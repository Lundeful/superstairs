using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("Where should the first tile be placed?")]
    public Transform levelStartPosition;
    public Transform player;

    [Header("Object pooling")]
    public int destroyAtDistance = 60;
    public int createAtDistance = 60;

    [Header("Tiles")]
    public GameObject flatStart;
    public GameObject flatMid;
    public GameObject stairStart;
    public GameObject stairMid;

    [Header("Level Properties")]
    public int minSubsequentFlatTiles = 6;
    public int maxSubsequentFlatTiles = 15;
    public int minSubsequentStairTiles = 7;
    public int maxSubsequentStairTiles = 25;


    [Header("Tile Spacing")]
    public Vector3 flatStartOffset;
    public Vector3 flatMidOffset;
    public Vector3 stairStartOffset;
    public Vector3 stairMidOffset;

    // Internal state
    private System.Random random;
    private Transform TileParent;
    private List<GameObject> tiles;

    void Awake()
    {
        random = new System.Random();
        tiles = new List<GameObject>();
        TileParent = this.transform;
        GenerateSetOfTiles();
    }

    private void FixedUpdate()
    {
        AddAndRemoveTiles();
    }

    private void AddAndRemoveTiles()
    {
        if (tiles.Count > 0)
        {
            var distanceToOldestTile = (player.position - tiles[0].transform.position).magnitude;
            if (distanceToOldestTile > destroyAtDistance) 
            {
                var oldestTile = tiles[0];
                tiles.RemoveAt(0);
                Destroy(oldestTile);
            }

            var distanceToNewestTile = (player.position - tiles[tiles.Count - 1].transform.position).magnitude;
            if (distanceToNewestTile < createAtDistance) GenerateSetOfTiles();
        }
    }

    private void GenerateSetOfTiles()
    {

        var flatTilesCount = GetAmountOfFlatTiles();
        var stairTilesCount = GetAmountOfStairTiles();

        AddFlatTiles(flatTilesCount);
        AddStairTiles(stairTilesCount);
    }

    private void AddFlatTiles(int flatTilesCount)
    {
        var startTile = Instantiate(flatStart, TileParent);
        var startTilePosition = tiles.Count > 0 ? tiles[tiles.Count - 1].transform.position + flatStartOffset : levelStartPosition.position;
        startTile.transform.position = startTilePosition;
        tiles.Add(startTile);
        for (int i = 1; i < flatTilesCount; i++)
        {
            AddFlatMidTile();
        }
    }

    private void AddFlatMidTile()
    {
        var tile = Instantiate(flatMid, TileParent);
        tile.transform.position = tiles[tiles.Count - 1].transform.position + flatMidOffset;
        tiles.Add(tile);
    }

    private void AddStairTiles(int stairTilesCount)
    {
        var startTile = Instantiate(stairStart, TileParent);
        var startTilePosition = tiles.Count > 0 ? tiles[tiles.Count - 1].transform.position + stairStartOffset : levelStartPosition.position;
        startTile.transform.position = startTilePosition;
        tiles.Add(startTile);
        for (int i = 1; i < stairTilesCount; i++)
        {
            AddStairMidTile();
        }
    }

    private void AddStairMidTile()
    {
        var tile = Instantiate(stairMid, TileParent);
        tile.transform.position = tiles[tiles.Count - 1].transform.position + stairMidOffset;
        tiles.Add(tile);
    }

    private int GetAmountOfFlatTiles()
    {
        return random.Next(minSubsequentFlatTiles, maxSubsequentFlatTiles);
    }

    private int GetAmountOfStairTiles()
    {
        return random.Next(minSubsequentStairTiles, maxSubsequentStairTiles);
    }
}
