using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject background;
    public Transform player;
    public Transform camera;

    [Header("Properties")]
    public float tileSize = 22.5f;
    private int stackWidth = 6;

    private Queue<GameObject> tiles;
    private Transform tileParent;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Queue<GameObject>();
        tileParent = this.transform;

        var initialPosition = player.position + Vector3.left * tileSize * 1.5f;

        var firstTile = Instantiate(background, tileParent);
        firstTile.transform.position = initialPosition;
        tiles.Enqueue(firstTile);

        for (int i = 1; i < stackWidth; i++)
        {
            var tile = Instantiate(background, tileParent);
            tile.transform.position = initialPosition + Vector3.right * tileSize * i;
            tiles.Enqueue(tile);
        }
    }

    private void FixedUpdate()
    {
        var thisPosition = this.transform.position;
        this.transform.position =  new Vector3(camera.transform.position.x, camera.transform.position.y, thisPosition.z);
        //var distanceToOldestTile = Mathf.Abs(player.position.x - tiles.Peek().transform.position.x);
        //if (distanceToOldestTile > tileSize * 2)
        //{
        //    var tileToMove = tiles.Dequeue();
        //    var vertDistance = Mathf.Abs((player.position.y - tiles.Peek().transform.position.y));
        //    var moveDirection = new Vector3(stackWidth * tileSize, tileSize);
        //    tileToMove.transform.position += Vector3.right * stackWidth * tileSize;
        //    //tileToMove.transform.position += Vector3.up * tileSize;
        //    tiles.Enqueue(tileToMove);
        //}
    }
}
