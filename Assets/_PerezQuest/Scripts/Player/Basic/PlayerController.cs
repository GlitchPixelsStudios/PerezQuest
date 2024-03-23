using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool dj;
    [SerializeField] private int  maxJumps = 2;

    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpHeightForce = 10.0f;

    private Rigidbody2D myRigidBody;
    private float circleGroundedCheck = 0.6f;

    private bool grounded;
    private int jumpsRemaining;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        // Comprobar si el jugador está en el suelo
        grounded = Physics2D.OverlapCircle(transform.position, circleGroundedCheck, LayerMask.GetMask("Suelo"));
        
        // Movimiento horizontal
        float movement = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(movement * movementSpeed, myRigidBody.velocity.y);

        // Salto
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            Jump();
        }

        //Debug.Log($"{jumpsRemaining} jumps + . {grounded}");
    }


    private void Jump()
    {
        if (grounded && !dj)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpHeightForce);
            myRigidBody.AddForce(Vector2.up * jumpHeightForce, ForceMode2D.Impulse);
        }

        if (dj)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpHeightForce);
            jumpsRemaining--;
        }

        if (grounded && dj) jumpsRemaining = maxJumps;
    }

}
