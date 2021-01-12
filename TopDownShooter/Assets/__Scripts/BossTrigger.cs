using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHealth;
    public GameObject[] spawners;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boss.SetActive(true);
            bossHealth.SetActive(true);
            foreach (GameObject spawner in spawners)
            {
                spawner.SetActive(true);
            }
        }
    }
}
