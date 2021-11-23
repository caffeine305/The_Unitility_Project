using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Rigidbody2D rigidbody;
    public Vector2 movement;
    public bool isDamaged;
    public float recoil;
     
    void Awake()
    {
        isDamaged = false;
        recoil = 2.0f;        
    }
    
    void FixedUpdate()
    {
        Movement();
        Debug.Log(isDamaged);
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -8.0f, 8.0f),
            Mathf.Clamp(transform.position.y, -4.3f, 4.3f)
        );

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
