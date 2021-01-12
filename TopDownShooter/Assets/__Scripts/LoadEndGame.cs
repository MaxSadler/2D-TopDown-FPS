using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndGame : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            Invoke("EndTheGame", 1);
    }

    void EndTheGame()
	{
        SceneManager.LoadScene("EndGame");
    }
}
