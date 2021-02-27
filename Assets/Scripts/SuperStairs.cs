using System;
using System.Collections.Generic;
using UnityEngine;

public class SuperStairs : MonoBehaviour
{
    [Header("Components")]
    public Transform gameProgresser;
    public GameObject drops;


    private List<GameObject> powerups;


    private Vector3 spawnPosition;



    private float timeToNextPowerup = 0f;
    private float timeSinceLastPowerup = 0f;
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = gameProgresser.position;
        random = new System.Random();
        Physics.IgnoreLayerCollision(10, 9);
        Physics.IgnoreLayerCollision(11, 9);
        Physics.IgnoreLayerCollision(11, 10);
        Physics.IgnoreLayerCollision(13, 9);

        SpawnPowerup();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandlePowerUps();
    }

    private void HandlePowerUps()
    {
        var distance = (gameProgresser.position - spawnPosition).magnitude;
        if (distance > 100f)
        {
            spawnPosition = gameProgresser.position;
            SpawnPowerup();
        }
    }

    private void SpawnPowerup()
    {
        var powerup = Instantiate(drops, this.transform);
        powerup.transform.position = gameProgresser.position + Vector3.up * 50 + Vector3.right * 50;
    }
}
