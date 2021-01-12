using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombies = new GameObject[4];
    public GameObject player;
    public GameObject[] toAttack;
    public int numberToSpawn = 20;
    private GameObject spawner;
    private int index = 0;
    private int zombiesSpawned = 0;
    public float spawnerDelay = 1f;
    public List<GameObject> zombiesSpawnedList;
    private void Start()
    {
        Invoke("SpawnZombie", 0f);
    }

    public void SpawnZombie()
    {
        zombiesSpawned++;

        if (zombiesSpawned <= numberToSpawn)
        {
            spawner = Instantiate(zombies[index]);
            zombiesSpawnedList.Add(spawner);
            spawner.SetActive(true);
            if (toAttack.Length > 0 && toAttack[0] != null && toAttack[1] != null)
            {
                print("enetered");
                spawner.GetComponent<Zombie>().player = toAttack[(int)Random.Range(0, 2)];
            }
            else
            {
                print("entered2");
                spawner.GetComponent<Zombie>().player = player;
            }
            spawner.transform.position = gameObject.transform.position;
            if (index < zombies.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            Invoke("SpawnZombie", spawnerDelay);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public float getLastZombieHealth()
    {
        foreach(GameObject spawn in zombiesSpawnedList)
        {
            if (spawn.GetComponent<Health>().getHealth() > 0 )
            {
                return spawn.GetComponent<Health>().getHealth();
            }
        }
        return 0f;
    }
}
