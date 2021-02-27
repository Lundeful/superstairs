using System;
using System.Collections.Generic;
using UnityEngine;

public class SuperStairs : MonoBehaviour
{
    [Header("Components")]
    public Transform gameProgresser;
    public GameObject drops;
    public GameObject fallingEnemy;
    public GameObject runningEnemy;

    private Vector3 powerupPosition;
    private Vector3 runningEnemyPosition;
    private Vector3 fallingEnemyPosition;

    private System.Random random;

    void Start()
    {
        powerupPosition = gameProgresser.position;
        runningEnemyPosition = gameProgresser.position;
        fallingEnemyPosition = gameProgresser.position;
        random = new System.Random();
        Physics.IgnoreLayerCollision(10, 9);
        Physics.IgnoreLayerCollision(11, 9);
        Physics.IgnoreLayerCollision(11, 10);
        Physics.IgnoreLayerCollision(11, 13);
        Physics.IgnoreLayerCollision(13, 10);
        Physics.IgnoreLayerCollision(10, 10);
        Physics.IgnoreLayerCollision(14, 10);
        Physics.IgnoreLayerCollision(14, 13);
        SpawnPowerup();
    }


    private void FixedUpdate()
    {
        HandlePowerUps();
        HandleRunningEnemy();
        HandleFallingEnemy();
    }


    private void HandlePowerUps()
    {
        var distance = (gameProgresser.position - powerupPosition).magnitude;
        if (distance > 100f)
        {
            powerupPosition = gameProgresser.position;
            SpawnPowerup();
        }
    }

    private void HandleRunningEnemy()
    {
        var distance = (gameProgresser.position - runningEnemyPosition).magnitude;
        if (distance > random.Next(50, 85))
        {
            runningEnemyPosition = gameProgresser.position;
            SpawnRunningEnemy();
        }
    }

    private void HandleFallingEnemy()
    {
        var distance = (gameProgresser.position - fallingEnemyPosition).magnitude;
        if (distance > random.Next(20, 50))
        {
            fallingEnemyPosition = gameProgresser.position;
            SpawnFallingEnemy();
        }
    }


    private void SpawnRunningEnemy()
    {
        var enemy = Instantiate(runningEnemy, this.transform);
        enemy.transform.position = gameProgresser.position + Vector3.up * 10 + Vector3.left * 25;
    }

    private void SpawnFallingEnemy()
    {
        var enemy = Instantiate(fallingEnemy, this.transform);
        enemy.transform.position = gameProgresser.position + Vector3.up * 50 + Vector3.right * 50;
    }

    private void SpawnPowerup()
    {
        var powerup = Instantiate(drops, this.transform);
        powerup.transform.position = gameProgresser.position + Vector3.up * 50 + Vector3.right * 50;
    }
}
