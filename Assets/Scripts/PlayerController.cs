using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueValue = 1f;
    [SerializeField] float normalSpeed = 20f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float jumpSpeed = 10f;

    [SerializeField] ParticleSystem jumpParticles;
    bool isJumping;
    bool isJumpKeyPressed;
    bool isBoosting;
    float rotationInput;
    bool canMove = true;

    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //RotatePlayer();
            //BoostPlayer();
            //JumpPlayer();
            (isJumping,isJumpKeyPressed) = CheckJumpInput();
            isBoosting = CheckBoostInput();
            rotationInput = CheckRotationInput();
            if (isJumpKeyPressed && !jumpParticles.isPlaying) //doesnt work without jumpParticles.isPlaying, because of not proper init
            {
                jumpParticles.Play();
            }
            else if (!isJumpKeyPressed && jumpParticles.isPlaying) //doesnt work without jumpParticles.isPlaying, because of not proper init
            {
                jumpParticles.Stop();
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            PerformJump();
        }
        if (isBoosting)
        {
            PerformBoostSpeed();
        }
        else
        {
            PerformNormalSpeed();
        }
        PerformRotation(rotationInput);
    }

    void RotatePlayer() //not advised approach
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb2d.AddTorque(torqueValue); //left direction
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb2d.AddTorque(-torqueValue); //right direction
        }
    }

    float CheckRotationInput()
    {
        // Return -1 for left, 1 for right, and 0 for no input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            return -1f; // Left rotation
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            return 1f; // Right rotation
        }

        return 0f; // No rotation
    }

    void PerformRotation(float direction)
    {
        if (direction != 0)
        {
            rb2d.AddTorque(torqueValue * direction); // Rotate based on direction
        }
    }

    void BoostPlayer() //not advised approach
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = normalSpeed;
        }
    }

    bool CheckBoostInput()
    {
        return (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
    }

    void PerformBoostSpeed()
    {
        surfaceEffector2D.speed = boostSpeed;
    }

    void PerformNormalSpeed()
    {
        surfaceEffector2D.speed = normalSpeed;
    }

    (bool,bool) CheckJumpInput()
    {
        return ((Input.GetKey(KeyCode.Space) && !isJumping), (Input.GetKey(KeyCode.Space)));
    }

    void PerformJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed); // Set the vertical velocity 
        isJumping = false; // Reset the flag after applying the jump
    }

    public void DisableControls()
    {
        canMove = false;
    }
}
