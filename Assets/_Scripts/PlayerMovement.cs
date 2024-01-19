using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float currentMovementSpeed;
    public float groundDrag;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    bool grounded;
    [SerializeField]
    private Vector2 keyInput;
    private Vector3 moveDirection;
    private Rigidbody rb;


    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentMovementSpeed = walkSpeed;
    }
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        //get player inputs every frame
        Inputs();
        SpeedControl();
        //handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }
    private void FixedUpdate()
    {

        //move player every fixed frame
        MovePlayer();
    }
    private void Inputs()
    {
        keyInput.x = Input.GetAxisRaw("Horizontal");
        keyInput.y = Input.GetAxisRaw("Vertical");
    }    

    private void MovePlayer()
    {
        moveDirection = transform.forward * keyInput.y + transform.right * keyInput.x;
        rb.AddForce(moveDirection.normalized * currentMovementSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > currentMovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentMovementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

}
