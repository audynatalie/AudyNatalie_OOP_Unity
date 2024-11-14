using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public float maxHealth;
    private float health;

    // Getter for the health property
    public float Health=>health;

    // Setter to decrease the health value
    public void Subtract(float damage)
    {
        health -= damage;

        if(health<=0)
        {
            Destroy(gameObject); // Destroy the object if health <= 0
        }
    }

    // Initialize health with maxHealth
    private void Start()
    {

        health=maxHealth;
    }
    
}

