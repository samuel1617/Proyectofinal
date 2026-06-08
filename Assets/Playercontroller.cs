using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] float speed = 7f;
    [SerializeField] float jumpForce = 7f;  
    [SerializeField] Rigidbody rb;
    bool isGrounded = true;

    private float moveX;

//lo primero que hace el juego al iniciar
    void Start()
    {
        if (rb == null)
        {
            //busca el componente si no fue colocado en el inspector
            rb = GetComponent<Rigidbody>();
        }
        //congela las rotaciones y bloquea la profundidad
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        //hay colision si hay un objeto con el Tag "Suelo"
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true; 
        }
    }

    void Update()
    {
        //movimiento en el eje x en las flechas
        moveX = Input.GetAxisRaw("Horizontal"); 

        //salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

//aca siempre van las fisicas 
    void FixedUpdate()
    {
       // desplazamiento en el eje x
        float desplazamientoX = moveX * speed * Time.fixedDeltaTime;

        Vector3 nuevaPosicion = new Vector3(rb.position.x + desplazamientoX, rb.position.y, rb.position.z);

        rb.MovePosition(nuevaPosicion);
    }
}