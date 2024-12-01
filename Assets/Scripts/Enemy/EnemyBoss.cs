using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 2f;  // Speed of the enemy boss movement
    private Vector2 moveDirection;  // Direction in which the boss moves
    private SpriteRenderer spriteRenderer;  // SpriteRenderer component for the boss

    private float spawnRangeX = 8f;  // Spawn range for the enemy boss along the X-axis
    private float spawnYRange = 4f;  // Spawn range for the enemy boss along the Y-axis

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer component for the boss
        RespawnAtSide();  // Respawn the boss at a random side of the screen
    }

    private void Update()
    {
        // Move the boss horizontally based on the moveDirection and speed
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // If the boss moves off the screen (either left or right), respawn on the opposite side
        if (transform.position.x < -spawnRangeX || transform.position.x > spawnRangeX)
        {
            RespawnAtSide();  // Call respawn method to place the boss at the other side
        }
    }

    // Method to spawn the boss at a random side (left or right) of the screen
    private void RespawnAtSide()
    {
        // Randomly choose the spawn position along the X-axis (left or right)
        float spawnX = Random.Range(0, 2) == 0 ? -spawnRangeX : spawnRangeX;
        float spawnY = Random.Range(-spawnYRange, spawnYRange);  // Random Y position within spawn range

        // Set the boss position at the selected spawn point with a random Y coordinate
        transform.position = new Vector2(spawnX, spawnY);

        // Determine the movement direction based on the spawn side
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;

        // Ensure the boss faces the correct direction (no rotation)
        transform.rotation = Quaternion.identity;
    }
}
