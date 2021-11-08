using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickup : MonoBehaviour
{   
    public float armourTimeOut = 10f;
    private PlayerHealth playerHealth;
    private GameManager gameManager;
    private bool pickedUp = false;
    private AudioSource pickupAS;

    private void Awake() {
        
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.UpdateArmourPickupCount(gameManager.armourPickupCount + 1);
        pickupAS = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player") && !GameManager.isGamePaused && !pickedUp)
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            //Pickup only if the player isn't already invulnerable
            if(!playerHealth.isInvulnerable)
            {   pickedUp = true;
                //Make Player Invulnerable for ArmourTimeOut Seconds
                playerHealth.MakeInvulnerable(armourTimeOut);
                gameManager.UpdateArmourPickupCount(gameManager.armourPickupCount - 1);
                pickupAS.PlayOneShot(pickupAS.clip);
                Destroy(gameObject, 0.2f);
            }

        }
        
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player") && !GameManager.isGamePaused && !pickedUp)
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            //Pickup only if the player isn't already invulnerable
            if(!playerHealth.isInvulnerable)
            {   pickedUp = true;
                //Make Player Invulnerable for ArmourTimeOut Seconds
                playerHealth.MakeInvulnerable(armourTimeOut);
                gameManager.UpdateArmourPickupCount(gameManager.armourPickupCount - 1);
                pickupAS.PlayOneShot(pickupAS.clip);
                Destroy(gameObject, 0.2f);
            }

        }
        
    }
    
}
