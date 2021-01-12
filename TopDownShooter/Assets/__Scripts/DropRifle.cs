using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRifle : MonoBehaviour
{
    public GameObject nPC;
    public GameObject spawnerStatus;
    public GameObject rifle;

    // Update is called once per frame
    void Update()
    {
        if (spawnerStatus.activeSelf == false && gameObject.GetComponent<EncounterTrigger>().getHelped() && (spawnerStatus.GetComponent<ZombieSpawner>().getLastZombieHealth() == 0f) && nPC.activeSelf)
        {
            rifle.SetActive(true);
            Destroy(gameObject);
        }

    }
}