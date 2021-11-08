using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int health;
    public bool isDead;
    public HealthBar healthBar;
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            isDead = true;
        }
    }

    public void DamageHealth(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }
}
