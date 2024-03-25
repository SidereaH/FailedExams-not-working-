using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        SetPlayerVelocity();
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

        }

    }
    private void SetPlayerVelocity()
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
}
