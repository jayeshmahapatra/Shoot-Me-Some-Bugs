using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    public GameObject explosion;
    public GameObject bloodSplatter;
    private GameObject hitEffect;
    private float xRange = 11f;
    private float yRange = 7f;
    public int damage = 20;

    public bool enemyBullet = false; //True if fired by enemy, false if fired by player. Used to disable freindly fire.
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //Destroy bullets if they leave the game area
        if(transform.position.x < -xRange || transform.position.x > xRange)
        {
            Destroy(gameObject);
        }
        else if(transform.position.y <= -yRange || transform.position.y >= yRange)
        {
            Destroy(gameObject);
        }


        //Destroy bullet after a certain time.
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.gameObject.CompareTag("Enemy") && !enemyBullet)
        {
            //Damage the enemy
            other.gameObject.GetComponent<EnemyHealth>().DamageHealth(damage);
            hitEffect = bloodSplatter;


        } else if (other.gameObject.CompareTag("Player") && enemyBullet)
        {
            other.gameObject.GetComponent<PlayerHealth>().DamageHealth(damage);
            hitEffect = bloodSplatter;
        }
        else
        {
            hitEffect = explosion;
        }

        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.33f);
        Destroy(gameObject);

    }
}
