using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform;  // The position of the Player
    private float speed = 2.0f;         // The movement speed of the enemy

    // Start is called before the first frame update
    private void Start()
    {
        // Find the Player object in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Ensure the Player was found before assigning its transform
        if (player != null)
        {
            playerTransform = player.transform;  // Set the playerTransform to the Player's position
        }
        else
        {
            Debug.LogWarning("Player not found in the scene. EnemyTargeting will not move.");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // If the Player is found, move towards it
        if (playerTransform != null)
        {
            // Calculate the direction from the enemy to the player, and normalize it
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            // Move the enemy towards the player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    // Triggered when the enemy collides with another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy collides with the Player, destroy the enemy
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);  // Destroy the enemy object when it touches the player
        }
    }
}
