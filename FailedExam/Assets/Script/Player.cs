using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
<<<<<<< HEAD

// Takes and handles input and movement for a player character
public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    Animator gunAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gunAnimator = transform.GetChild(0).GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {

                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isRunning", success);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnAttack()
    {
        gunAnimator.SetTrigger("isAttack");
    }

    public void SwordAttack()
    {
        
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        
        swordAttack.StopAttack();
    }

    
=======
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    public float speed;
    [SerializeField]
    public float rotationSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Vector2 smoothMovementInput;
    private Vector2 movementInputSmoothVelocity;
    private bool facingRight = true;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float collisionOffset = 0.5f;
    Animator animator;
    
    /*void Start()
    {
       
    }*/
    /*
    // Update is called once per frame
    void Update()
    {
      if (controlType == ControlType.PC) 
       { 
           moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       }
       else if (controlType == ControlType.Android)
       {
           moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
       }


       moveVelocity = moveInput.normalized * speed;
   }*/
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    /* private void FixedUpdate()
     {
         rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
     }*/
    private void Update()
    {
        /*if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput.x > 0) 
        {
            Flip();
        }*/
    }
    private void FixedUpdate()
    {
        //SetPlayerVelocity();
       // RotateInDirectionOfInput();
       if(moveInput != Vector2.zero)
        {
            bool success = TryMove(moveInput);
            if (!success)
            {
                success = TryMove(new Vector2(moveInput.x,  0));
                if (!success)
                {
                    success = TryMove(new Vector2(0, moveInput.y));
                } 
            }
            animator.SetBool("isRunning", success);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
    /*private void SetPlayerVelocity()
    {
        smoothMovementInput = Vector2.SmoothDamp(smoothMovementInput, moveInput, ref movementInputSmoothVelocity, 0.1f);

        rb.velocity = smoothMovementInput * speed;

    }
    /*private void RotateInDirectionOfInput()
    {
        if(moveInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rotation);
        }
    }*/
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
    /*private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }*/
    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            speed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }
>>>>>>> origin/main
}
