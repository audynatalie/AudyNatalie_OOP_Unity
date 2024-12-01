using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2f;  // Speed of the enemy's movement
    private Vector2 moveDirection;  // Direction in which the enemy moves horizontally

    private float spawnRangeX = 8f;  // Spawn range for the enemy along the X-axis
    private float spawnYRange = 4f;  // Spawn range for the enemy along the Y-axis

    private void Start()
    {
        RespawnAtSide();  // Call method to respawn the enemy at a random side of the screen
    }

    private void Update()
    {
        // Move the enemy horizontally based on its direction and speed
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // If the enemy moves off the screen (either left or right), respawn it on the opposite side
        if (transform.position.x < -spawnRangeX || transform.position.x > spawnRangeX)
        {
            RespawnAtSide();  // Call the respawn method to place the enemy on the other side
        }
    }

    // Method to randomly position the enemy at either the left or right side of the screen
    private void RespawnAtSide()
    {
        // Randomly choose the spawn position on the X-axis (left or right side)
        float spawnX = Random.Range(0, 2) == 0 ? -spawnRangeX : spawnRangeX;
        float spawnY = Random.Range(-spawnYRange, spawnYRange);  // Random Y position within the spawn range

        // Set the enemy's position at the chosen spawn point with a random Y coordinate
        transform.position = new Vector2(spawnX, spawnY);

        // Determine the movement direction based on the spawn side (left or right)
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;

        // Ensure the enemy remains upright (no rotation)
        transform.rotation = Quaternion.identity;
    }
}
