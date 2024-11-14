using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitboxComponent : MonoBehaviour
{

    private HealthComponent healthComponent;

    private void Awake()
    {
        // Check if HealthComponent is attached to the same object
        healthComponent=GetComponent<HealthComponent>();
        if(healthComponent==null)
        {

            Debug.LogError("HealthComponent not found on " +gameObject.name);
        }

    }

    // Method to reduce health by receiving damage from a Bullet
    public void Damage(Bullet bullet)
    {
        if(bullet!=null)
        {

            healthComponent.Subtract(bullet.damage);
        }

    }

    // Method to reduce health by receiving integer damage
    public void Damage(int damage)
    {

        healthComponent.Subtract(damage);
    }

}
