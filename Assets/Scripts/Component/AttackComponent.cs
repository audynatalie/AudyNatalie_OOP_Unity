using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;  // Bullet used for attacking
    public int damage;     // Damage dealt by this object

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has a HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        
        // If the collided object has a different tag and contains a HitboxComponent
        if(hitbox!=null)
        {
            // Deal damage to the object with the bullet
            
            
            hitbox.Damage(bullet);

            // Or if damage uses an integer
            // hitbox.Damage(damage); 
        }

    }

    
}
