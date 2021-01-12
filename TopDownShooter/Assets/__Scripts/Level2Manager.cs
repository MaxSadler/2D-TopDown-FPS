using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    public static int step = 0;
    public GameObject block;
    void Start()
    {
        
    }

    void Update()
    {
        if(step == 1)
        {
            block.SetActive(false);
        }
    }
}
