using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{   
    public int extraHealth = 50;
    private PlayerHealth playerHealth;
    private GameManager gameManager;
    private AudioSource pickupAS;
    private bool pickedUp = false;

    private void Awake() {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.UpdateHealthPickupCount(gameManager.healthPickupCount + 1);
        pickupAS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !GameManager.isGamePaused && !pickedUp)
        {   //If player is not at Max-Health, initiate pickup
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth.health < playerHealth.maxHealth)
            {   pickedUp = true;
                playerHealth.AddHealth(extraHealth);
                gameManager.UpdateHealthPickupCount(gameManager.healthPickupCount-1);
                pickupAS.PlayOneShot(pickupAS.clip);
                Destroy(gameObject, 0.2f);
            }
            //If player at max health, ignore
        }
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player") && !GameManager.isGamePaused && !pickedUp)
        {   //If player is not at Max-Health, initiate pickup
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth.health < playerHealth.maxHealth)
            {   pickedUp = true;
                playerHealth.AddHealth(extraHealth);
                gameManager.UpdateHealthPickupCount(gameManager.healthPickupCount-1);
                pickupAS.PlayOneShot(pickupAS.clip);
                Destroy(gameObject, 0.2f);
            }
            //If player at max health, ignore
        }
        
    }
}
