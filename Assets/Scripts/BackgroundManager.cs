using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject player;
    Transform tileParent;

    private GameObject[] tiles;
    private GameObject playerTile;
    private int gridSize = 3;
    private float tileSize;

    private Random random = new Random();

    // Start is called before the first frame update
    void Start()
    {
        if (!player) Debug.LogError("Missing player component");

        tileParent = this.transform;
        // Generate tiles
        tiles = new GameObject[gridSize * gridSize];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = Instantiate(background, tileParent);
        }

        // Set default player tile and tilesize
        playerTile = tiles[0];
        tileSize = 22.5f;

        // Place tiles
        //UpdateTilePositions();
        int count = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 newPosition = playerTile.transform.position + new Vector3(j * tileSize - tileSize, 0, i * tileSize - tileSize);
                tiles[count].transform.position = newPosition;
                count++;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        GameObject closestTile = tiles[0];
        float closestDistance = (player.transform.position - tiles[0].transform.position).sqrMagnitude;
        foreach (var tile in tiles)
        {
            float distance = (tile.transform.position - playerPosition).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestTile = tile;
                closestDistance = distance;
            }
        }
        if (playerTile == null || closestTile != playerTile)
        {
            playerTile = closestTile;
            UpdateTilePositions();
        }
    }

    private void UpdateTilePositions()
    {
        // Update tile positions
        int count = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Vector3 newPosition = playerTile.transform.position + new Vector3(j * tileSize - tileSize, i * tileSize - tileSize, 0f);
                GameObject newTile = Instantiate(background, newPosition, Quaternion.Euler(0, 0, 0));
                GameObject oldTile = tiles[count];
                tiles[count] = newTile;
                Destroy(oldTile);
                count++;
            }
        }
    }
}
