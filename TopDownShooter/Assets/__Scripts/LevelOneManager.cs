using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneManager : MonoBehaviour
{

    public static int stage = 0;
    public GameObject dialogue2;
    public GameObject level2Trigger;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<DialogueManager>().Init();
    }

    private void Update()
    {
        if(stage == 0 && this.gameObject.GetComponent<DialogueManager>().isActive == false)
        {
            this.GetComponent<AudioSource>().Play();
            stage++;
            dialogue2.GetComponent<DialogueManager>().Init();
        }
        else if(stage == 3)
        {
            level2Trigger.SetActive(true);
            stage++;
        }
    }

}
