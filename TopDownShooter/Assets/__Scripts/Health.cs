using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour {
    //Variables
    [Header("Set Dynamically")]
    [SerializeField]
    private float maxHealth = 5; //Arbitrarily picked number
    private float health; //Starts the same as maxHealth
    public static int score;

    public void Start()
    {
        health = maxHealth;
        score = 0;
    }

    public void Update()
    {
        if (this.gameObject.GetComponent<Boss>() != null)
        {
            GameObject.FindGameObjectWithTag("BossHealth").transform.localScale = new Vector3((float)health / maxHealth, 1, 1);
        }
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
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.transform.position = this.gameObject.transform.position - new Vector3(0, 0, -0.1f);

            score = score + 100;    // killed regular enemy, score increases by 100 points
        }
        else if(this.gameObject.GetComponent<Boss>() != null)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("die");
            this.gameObject.GetComponent<Boss>().isAlive = false;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.transform.position = this.gameObject.transform.position - new Vector3(0, 0, -0.1f);

            score = score + 750;        // killed boss enemy, score increases by 750 points 
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

    public float getHealth()
    {
        return health;
    }

    public void setMaxHealth(float medicHealth)
    {
        maxHealth = medicHealth;
    }

    public void setHealth(float amount)
    {
        health = amount;
    }

  
}
