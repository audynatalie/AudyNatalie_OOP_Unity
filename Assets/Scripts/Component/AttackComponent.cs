using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;  // Bullet used for attack
    public int damage;     // Damage dealt by the object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitbox = GetComponent<HitboxComponent>();  // Get the HitboxComponent attached to the object

        // If the collision is with an object of the same tag, do nothing
        if (collision.gameObject.tag == gameObject.tag)
        {
            return;
        }

        // If the collision is with a bullet, apply damage through HitboxComponent
        if (collision.CompareTag("Bullet"))
        {
            // int damage = collision.GetComponent<Bullet>().damage; // Get damage from Bullet (commented out)

            if (hitbox != null)
            {
                hitbox.Damage(collision.GetComponent<Bullet>()); // Apply damage using HitboxComponent with Bullet as parameter
                Debug.Log("Bullet damage applied.");
            }
        }

        // If the collision is with an enemy or player, apply direct damage
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            hitbox = GetComponent<HitboxComponent>();  // Get the HitboxComponent again for direct damage
            if (hitbox != null)
            {
                hitbox.Damage(damage);  // Apply direct damage to the target
                Debug.Log("Direct damage applied.");

                var invincibility = collision.GetComponent<InvicibiltyComponent>();  // Check if the object has InvincibilityComponent
                if (invincibility != null)
                {
                    invincibility.StartInvincibility();  // Start invincibility effect for the collided object
                    Debug.Log("Invincibility started for collided object.");
                }
            }
        }
    }
}
