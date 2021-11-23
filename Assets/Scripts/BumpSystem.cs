using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpSystem : MonoBehaviour
{
    // Reference to the box collider
    private BoxCollider2D bumper;
    public float initialBumperSize;
    public float finalBumperSize;
    public Vector2 size;
    public float incSpeed;
    public bool isBumping;
 
    private void Awake() {
        bumper = GetComponent<BoxCollider2D>(); //BoxCollider en lugar de Circle, y quitar el Circle.
        incSpeed = 45.0f;
        initialBumperSize = bumper.size.x;
        //size = bumper.size;
        finalBumperSize = 2.5f;
    }

    void FixedUpdate()
    {        
        size = bumper.size; 

        if(isBumping){
            if (size.x < finalBumperSize) {
                size += Vector2.one * incSpeed * Time.deltaTime;
                
                if (size.x > finalBumperSize) {
                size = new Vector2(finalBumperSize,finalBumperSize);
                }
            }
        }else{
            size = new Vector2(initialBumperSize,initialBumperSize);
        }

        bumper.size = size;
        Debug.Log(isBumping);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player") ){
            isBumping = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player") ){
            isBumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player") ){
            isBumping = false;
        }
    }

}
