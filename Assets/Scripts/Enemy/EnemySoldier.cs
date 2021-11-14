using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour
{   
    //Seconds to wait before firing the next round
    public GameObject player;
    public int killScore = 50; //Score added to player once the enemy is killed
    public float shootRate = 0.5f;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    
    //Seconds to wait before starting to fire after spawning
    private float startBulletCoolDown = 3f; 
    private Animator animator;
    private AudioSource EnemyAS;
    public AudioClip deathSound;
    private Rigidbody2D rb;
    private float nextShootTime;
    private EnemyHealth healthScript;
    private bool killed = false;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyAS = GetComponent<AudioSource>();
        healthScript = GetComponent<EnemyHealth>();
        //Wait for some seconds before firing the first round
        nextShootTime = Time.time + startBulletCoolDown;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.UpdateSoldierCount(gameManager.soldierCount + 1);
        

    }


    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate() {

        //Check if dead
        if(healthScript.isDead && !killed)
        {   Kill();
        }
        
        Vector2 lookDir = player.GetComponent<Rigidbody2D>().position - rb.position ; 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        //Fire bullet once every shootRate seconds
        if (Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + shootRate;
            Shoot();
        }
        
    }

    void Shoot()
    {   animator.SetBool("isShooting", true);
        Invoke("isShootingFalse", 0.1f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().enemyBullet = true;
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        muzzleFlash.transform.parent = transform;
        EnemyAS.PlayOneShot(EnemyAS.clip);
        Destroy(muzzleFlash, 0.1f);
        Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void Kill()
    {   killed = true;
        //Update Score
        gameManager.UpdateScore(killScore);
        gameManager.UpdateSoldierCount(gameManager.getSoldierCount() - 1);
        //Play Death Animation & Sound
        EnemyAS.clip = deathSound;
        EnemyAS.PlayOneShot(EnemyAS.clip);
        //Kill the player
        Destroy(gameObject, deathSound.length);
    }

    void isShootingFalse()
    {
          animator.SetBool("isShooting", false);
    }

}
