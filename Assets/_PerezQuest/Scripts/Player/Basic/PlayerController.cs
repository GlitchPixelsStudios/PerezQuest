using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpHeightForce = 10.0f;
    [SerializeField] private float slideForce = 2.0f;

    private Rigidbody2D myRigidBody;
    private bool grounded;
    private bool sliding;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Comprobar si el jugador está en el suelo
        grounded = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Suelo"));

        // Movimiento horizontal
        float movement = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(movement * movementSpeed, myRigidBody.velocity.y);

        // Salto
        if (grounded && Input.GetButtonDown("Jump"))
        {
            myRigidBody.AddForce(Vector2.up * jumpHeightForce, ForceMode2D.Impulse);
        }

        // Deslizarse
        if (Input.GetButtonDown("Slide"))
        {
            sliding = true;
        }
        else if (Input.GetButtonUp("Slide"))
        {
            sliding = false;
        }

        if (sliding)
        {
            myRigidBody.velocity = new Vector2(movement * slideForce, myRigidBody.velocity.y);
        }

        // Resetear la velocidad horizontal en el suelo
        if (grounded)
        {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        }
    }
}
