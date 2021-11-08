using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{   
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public float bulletForce = 20f;
    private Animator animator;

    //How many seconds to wait for firing one bullet
    public float shootRate = 0.25f;
    private float nextShootTime;

    private AudioSource playerAS;
    // Start is called before the first frame update
    void Awake()
    {
        playerAS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        nextShootTime = Time.time + shootRate;
    }

    // Update is called once per frame
    void Update()
    {   if(!GameManager.isGamePaused)
        {
            if(Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            {   
                if(Time.time > nextShootTime)
                {
                    Shoot();
                    nextShootTime = Time.time + shootRate;
                }
                
                
            }
        }
        
    }

    void Shoot()
    {   animator.SetBool("isShooting", true);
        Invoke("isShootingFalse", 0.1f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().enemyBullet = false;
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        muzzleFlash.transform.parent = transform;
        playerAS.Play();
        Destroy(muzzleFlash, 0.1f);
        Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void isShootingFalse()
    {
          animator.SetBool("isShooting", false);
    }
}
