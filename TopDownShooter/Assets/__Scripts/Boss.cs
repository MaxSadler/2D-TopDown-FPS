using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject player;
    public float speed = 10f;
    public float attackCoolDown = 2f;
    public float attackCoolDownElapsed = 2f;
    public float attack2CoolDown = 2f;
    public float attack2CoolDownElapsed = 2f;

    public float attack2Duration = 1.2f;
    public float attack2DurationElapsed = 1.2f;

    public bool attack2 = false;
    public bool isAlive = true;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (isAlive)
        {
            transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg) + 90f);
            //transform.GetComponent<Rigidbody2D>().MovePosition(transform.position + (-transform.up * speed * Time.deltaTime));

            if (Vector3.Distance(player.transform.position, transform.position) < 10f)
            {
                if (attack2CoolDownElapsed >= attack2CoolDown)
                {
                    if (Random.Range(0, 1000) <= 1f)
                    {
                        attack2 = true;
                        attack2DurationElapsed = 0f;
                        Attack2();
                    }
                }
            }
            if (!attack2)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < 5f)
                {
                    Attack();
                }
                else
                {
                    transform.GetComponent<Rigidbody2D>().MovePosition(transform.position + (-transform.up * speed * Time.deltaTime));
                }
            }
            if (attackCoolDownElapsed < attackCoolDown)
            {
                attackCoolDownElapsed += Time.deltaTime;
            }

            if (attack2CoolDownElapsed < attack2CoolDown)
            {
                attack2CoolDownElapsed += Time.deltaTime;
            }

            if (attack2DurationElapsed < attack2Duration)
            {
                attack2DurationElapsed += Time.deltaTime;
                if(attack2DurationElapsed >= attack2Duration)
                {
                    attack2 = false;
                }
            }
        }
    }

    void Attack()
    {
        if (attackCoolDownElapsed >= attackCoolDown)
        {
            this.GetComponent<Animator>().SetTrigger("attack");
            player.GetComponent<Health>().AddDamage(4);
            attackCoolDownElapsed = 0f;
            Debug.Log("Attack");
        }
    }

    void Attack2()
    {
        if (attackCoolDownElapsed >= attackCoolDown)
        {
            this.GetComponent<Animator>().SetTrigger("attack2");
            if (Vector3.Distance(player.transform.position, transform.position) < 5f)
            {
                player.GetComponent<Health>().AddDamage(3);
            }
            attack2CoolDownElapsed = 0f;
            Debug.Log("Attack2");
        }
    }
}
