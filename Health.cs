using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    //Variables
    [Header("Set Dynamically")]
    [SerializeField]
    private float maxHealth = 5; //Arbitrarily picked number
    private float health; //Starts the same as maxHealth

    public void Start()
    {
        health = maxHealth;
    }

    //Reduces health
    public void AddDamage(float amount) {
        //Decrease the current health by the specified damage
        health -= amount;
        //If the damage causes the current health to go equal to or below zero
        //call Die() function
        if (health <= 0) {
            Die();
        }
    }

    //Recover health
    public void AddHealth(float amount) {
        //Heal specified amount
        health += amount;
        //If current health is above max health
        //make current health equal to max health
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    //Increase max health
    void IncreaseMaxHealth(float amount) {
        //Increase max health by specified amount
        maxHealth += amount;
    }

    //If the player runs out of health, 
    //destroy the player (will likely be changed to respawn)
    void Die() {
        //Destroy the specified gameObject
        if (this.gameObject.GetComponent<Zombie>() != null)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("die");
            this.gameObject.GetComponent<Zombie>().isAlive = false;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.transform.position = this.gameObject.transform.position - new Vector3(0, 0, -0.1f);

        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public float getHealthPercentage()
    {
        return (float)health / maxHealth;
    }

    public void setMaxHealth(float medicHealth)
    {
        maxHealth = medicHealth;
    }
}
