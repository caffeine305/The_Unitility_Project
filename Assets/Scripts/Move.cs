using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Rigidbody2D rigidbody;
    public Vector2 movement; 
    public bool isDamaged;
    public bool isGrounded;
    public bool isPunching;
    public bool isKicking;
    public float recoil;
    private float jumpForce;
    private float groundLevel;
    private Vector3 velocity;
    private float gravity;
     
    void Awake()
    {
        isDamaged = false;
        recoil = 2.0f;
        jumpForce = 10f;
        gravity = -20f;
        groundLevel = transform.position.y;
        isKicking = false;
        isPunching = false;
    }
    
    void FixedUpdate()
    {
        Movement();
        Debug.Log(isDamaged);
    }

    void Movement()
    {
   // Captura la entrada del jugador
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");

    // Actualiza los parámetros del animador
    animator.SetFloat("Horizontal", movement.x);
    animator.SetFloat("Vertical", movement.y);
    animator.SetFloat("Speed", movement.sqrMagnitude);
    animator.SetBool("isGrounded", isGrounded);
    animator.SetBool("isPunching", isPunching);
    animator.SetBool("isKicking", isKicking);

    // Movimiento horizontal
    Vector3 horizontalMovement = new Vector3(movement.x * moveSpeed * Time.deltaTime, 0,0);
    transform.position += horizontalMovement;

    // Movimiento vertical (en el plano 2D, eje Z)
    Vector3 verticalMovement = new Vector3(0, movement.y * moveSpeed * Time.deltaTime,0);
    transform.position += verticalMovement;

    //Golpe
    if(Input.GetButton("Fire2") && isGrounded)
    {
        isPunching = true;
    }
    else
    {
        isPunching = false;
    }

    //Aquí va un If Not Grounded and Punching

    // Salto
    if (Input.GetButton("Jump") && isGrounded)
    {
        velocity.y = jumpForce; // Aplica fuerza inicial de salto
        isGrounded = false; // Marca que está en el aire
        groundLevel = transform.position.y;
    }

    // Aplica la gravedad mientras el personaje está en el aire
    if (!isGrounded)
    {
        velocity.y += gravity * Time.deltaTime;
        if(Input.GetButton("Fire2"))
        {
            isKicking = true;
        }
        else
        {
            isKicking = false;
        }
    }

    // Aplica el movimiento vertical al personaje
    transform.position = transform.position + (velocity * Time.deltaTime);

    // Verifica si el personaje ha aterrizado
    if (!isGrounded && transform.position.y <= groundLevel)
    {
        isGrounded = true;
        velocity.y = 0f; // Resetea la velocidad al aterrizar
        //transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
        transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);        

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8.0f, 8.0f),
            Mathf.Clamp(transform.position.y, -4.3f, 0f),
            Mathf.Clamp(transform.position.z, -1f, 1f)
        ); 
    }
    
    if(isGrounded)
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8.0f, 8.0f),
            Mathf.Clamp(transform.position.y, -4.3f, -2.5f),
            Mathf.Clamp(transform.position.z, -1f, 1f)
        ); 
    }

        if(isDamaged){
            rigidbody.MovePosition(rigidbody.position - movement * moveSpeed * recoil * Time.fixedDeltaTime);
        }else{
            rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        
    }
    //Hurt and Damage Dynamics

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "harsh") )
        {
            isDamaged = true;
            //Destroy(this.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "harsh") )
        {
            isDamaged = false;
            //Destroy(this.gameObject);
        }
    }

}