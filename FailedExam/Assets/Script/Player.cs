using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
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
        rb.velocity = moveInput * speed;
    }
    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
}
