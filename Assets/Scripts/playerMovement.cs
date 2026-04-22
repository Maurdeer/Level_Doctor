using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour
{
    public float Speed;
    public float Jump;
    float inputMovement;

    PlayerManager PlayerManager;

    Rigidbody2D rb;

    bool touchGrass;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float radius;

    private void Awake()
    {
     
        rb = GetComponent<Rigidbody2D>();
        PlayerManager = GetComponent<PlayerManager>();

    }

    private void FixedUpdate()
    {
        touchGrass = Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
        if (PlayerManager.canMove)
        {
            inputMovement = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(inputMovement * Speed, rb.linearVelocity.y);
        }
        

    }

    // Update is called once per frame
    void Update() {

        if (touchGrass && Input.GetKeyDown(KeyCode.Space) && PlayerManager.canMove)
        {
            rb.linearVelocity = Vector2.up * Jump;
        }
        // If movement, essentially resets the player.
        if (GameManager.Instance != null && GameManager.Instance.gamePhase == 1)
        {
            if (Input.GetKeyDown(KeyCode.R)) GameManager.Instance.ResetOnPlayerDeath();
        }
    }

    
}

// https://www.youtube.com/watch?v=vA-jv_fP5EE


