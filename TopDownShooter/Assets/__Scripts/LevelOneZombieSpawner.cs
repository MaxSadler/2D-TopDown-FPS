using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneZombieSpawner : MonoBehaviour
{
    public List<GameObject> zombies;
    private bool hasSpawned = false;
    public void Update()
    {
        if (hasSpawned)
        {
            bool finished = true;
            foreach (GameObject zombie in zombies)
            {
                if (zombie.GetComponent<Zombie>().isAlive == true)
                {
                    finished = false;
                    break;
                }
            }
            if (finished == true)
            {
                LevelOneManager.stage = 3;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && LevelOneManager.stage == 2)
        {
            foreach(GameObject zombie in zombies)
            {
                zombie.SetActive(true);
            }
            hasSpawned = true;
        }
    }
}
