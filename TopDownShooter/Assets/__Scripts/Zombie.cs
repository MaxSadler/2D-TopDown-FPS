using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject player;
    public float speed = 1f;
    public float attackCoolDown = 2f;
    public float attackCoolDownElapsed = 2f;
    public bool isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg) + 90f);
            transform.GetComponent<Rigidbody2D>().MovePosition(transform.position + (-transform.up * speed * Time.deltaTime));

            if (Vector3.Distance(player.transform.position, transform.position) < 2f)
            {
                //Debug.Log("Attack");
                Attack();
            }
            if (attackCoolDownElapsed < attackCoolDown)
            {
                attackCoolDownElapsed += Time.deltaTime;
            }
        }
    }

    void Attack()
    {
        if(attackCoolDownElapsed >= attackCoolDown)
        {
            this.GetComponent<Animator>().SetTrigger("attack");
            player.GetComponent<Health>().AddDamage(1);
            attackCoolDownElapsed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Player.bodyChecks > 0)
        {
            if (other.tag == "Player" && (Player.isDashing == true) && (MenuOnClick.isBolt == true))
            {
                this.GetComponent<Health>().AddDamage(5);
                Debug.Log("Player in contact with zombie:" + this);
                (Player.bodyChecks)--;
                Debug.Log("Number of Body Checks Remaining:" + (Player.bodyChecks));
            }
        }
    }
}
