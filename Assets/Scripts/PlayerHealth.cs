using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public LifebarManager healthBar;

    void Start()
    {
        currentHealth = maxHealth;    
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void TakeDamage(int damage) //creo que esto debe ser Public
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Enemy") )
            {
                TakeDamage(15);
            }
    }

}
