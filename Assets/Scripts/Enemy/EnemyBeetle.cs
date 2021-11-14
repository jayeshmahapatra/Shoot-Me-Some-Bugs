using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBeetle : MonoBehaviour
{   public int killScore = 20; //Score added to player once the enemy is killed
    public int damage = 10; //Damage caused to player
    public AudioClip deathSound;
    public bool killed = false;
    private EnemyHealth healthScript;
    private AudioSource EnemyAS;
    private GameManager gameManager;
    private AIDestinationSetter aiDestinationSetter;
    private bool killedByPlayer = false;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyAS = GetComponent<AudioSource>();
        healthScript = GetComponent<EnemyHealth>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        aiDestinationSetter.target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        gameManager.UpdateBeetleCount(gameManager.beetleCount + 1);
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Check if dead
        if(healthScript.isDead && !killed)
        {   killedByPlayer = true;
            Kill(killedByPlayer);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Player") && !GameManager.isGamePaused && !killed)
        {
            //Damage Player
            other.gameObject.GetComponent<PlayerHealth>().DamageHealth(damage);
            //Kill the Beetle
            killedByPlayer = false;
            Kill(killedByPlayer);
        }
    }

    void Kill(bool killedByPlayer)
    {   killed = true;
        //If killed by player, add score to Game
        if(killedByPlayer)
        {
            gameManager.UpdateScore(killScore);
        }
        gameManager.UpdateBeetleCount(gameManager.getBeetleCount() - 1);        
        //Play Death Animation & Sound
        EnemyAS.clip = deathSound;
        EnemyAS.PlayOneShot(EnemyAS.clip);
        //Destroy the Beetle Object
        Destroy(gameObject, deathSound.length);
    }
}
