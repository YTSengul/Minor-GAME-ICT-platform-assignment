using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthValue maxHealth;
    public int health;

    void Start()
    {
        health = maxHealth.healthValue;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void removeHealth()
    {
        health--;
    }
}
