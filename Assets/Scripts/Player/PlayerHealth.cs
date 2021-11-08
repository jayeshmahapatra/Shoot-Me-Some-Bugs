using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int health;
    public bool isDead;
    public bool isInvulnerable;
    public HealthBar healthBar;
    public ArmourBar armourBar;
    public GameObject sheild;
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
        if(!isInvulnerable)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
        
    }

    public void AddHealth(int extraHealth)
    {
        health = Mathf.Min(health+extraHealth, maxHealth);
        healthBar.SetHealth(health);
    }

    public void MakeInvulnerable(float armourTimeOut)
    {
        StartCoroutine(PlayerInvulnerable(armourTimeOut));
    }

    IEnumerator PlayerInvulnerable(float armourTimeOut)
    {   isInvulnerable = true;
        sheild.SetActive(true);
        float elapsedTime = 0;
        while(elapsedTime < armourTimeOut)
        {
            elapsedTime += Time.deltaTime;
            armourBar.SetArmour(armourTimeOut - elapsedTime);
            yield return null;
        }
        isInvulnerable = false;
        sheild.SetActive(false);
        
        
    }
}
