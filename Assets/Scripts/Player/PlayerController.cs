using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float moveSpeed = 5f;
    public Rigidbody2D playerRb;
    private Vector2 movement;
    [SerializeField] private Vector2 mousePos;
    private Animator animator;

    public Camera cam;
    private PlayerHealth playerHealth;
    private GameManager gameManager;
    private Transform firePoint;

    //Max Angle that the FirePoint can deviate from the player's orientation for finetuning to point at the mouse
    public float finetuneRange = 5f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        firePoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {   
        if(!GameManager.isGamePaused)
        {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        animator.SetFloat("speed", movement.sqrMagnitude);
        }

         if(playerHealth.isDead)
        {
            gameManager.GameOver();
        }
    
        
    }

    void FixedUpdate() 
    {
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        //Rotate the player to look at the mouse
        Vector2 lookDir = mousePos - playerRb.position ;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRb.rotation = angle;


        //Fine tune the rotation of the FirePoint to make it look at Mouse Pointer
        lookDir = mousePos - (Vector2)firePoint.position;
        //Clamp finetuing the FirePoint rotation with respect to Player rotation
        float finetune_offset = Mathf.Clamp(Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f -angle, -finetuneRange, finetuneRange);
        firePoint.rotation = Quaternion.AngleAxis(angle + finetune_offset, Vector3.forward);
        
      

    }

    void isShootingFalse()
    {
          animator.SetBool("isShooting", false);
    }
}
