using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador
    public float jumpForce = 10f; // Fuerza de salto
    public float doubleJumpForce = 8f; // Fuerza de segundo salto
    public bool isGrounded; // verificacion de colision con el suelo
    public Rigidbody rb;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Obtener la entrada horizontal
        float moveY = Input.GetAxis("Vertical"); // Obtener la entrada vertical

        Vector3 movement = new Vector3(moveX, 0f, moveY); // Crear un vector de movimiento con las entradas

        // Normalizar el vector de movimiento si se está moviendo en diagonal
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // velocidad de movimiento 
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Verificar si el jugador está en el suelo
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        // Permitir salto doble si no está en el suelo y el jugador presiona el botón de salto
        else if (!isGrounded && Input.GetButtonDown("Jump"))
        {
            DoubleJump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplicar fuerza hacia arriba para el salto
        isGrounded = false; // Actualizar el flag de suelo
    }

    void DoubleJump()
    {
        rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse); // Aplicar fuerza hacia arriba para el segundo salto
    }

    // collision con el suelo
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Actualizar el flag de suelo cuando el jugador colisione
        }
    }
}

