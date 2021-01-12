using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterTrigger : MonoBehaviour
{
    [SerializeField] private Animator _myAnimation;
    public GameObject player;
    public GameObject initiateEvent;
    public GameObject noOption;
    public GameObject yesOption;
    public Text textBox;
    private bool helped;
    public GameObject continueText;
    public string[] dialogue = new string[4];
    int index = 0;
    private bool oneTime = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && oneTime)
        {
            oneTime = false;
            player.GetComponent<Player>().canMove = false;
            player.GetComponent<Player>().setCanShoot(false);
            _myAnimation.SetBool("goToPopUp", true);
            Invoke("IsShown", 1);
            Level2Manager.step = 1;
        }
    }

    IEnumerator SetText(string line)
    {
        if (index <= dialogue.Length - 1)
        {
            textBox.text = "";
            foreach (char character in line.ToCharArray())
            {
                textBox.text += character;
                yield return null;
            }
        }
    }

    public void IsShown()
    {
        if (index <= dialogue.Length - 1)
        {
            StartCoroutine(SetText(dialogue[index]));
            index++;
        }
        else
        {
            StopAllCoroutines();
            textBox.text = "";
            continueText.SetActive(false);
            noOption.SetActive(true);
            yesOption.SetActive(true);
        }
    }

    public void ChoiceHandler(GameObject caller)
    {
        if (caller == yesOption)
        {
            helped = true;
        }
        else
        {
            helped = false;
        }

        player.GetComponent<Player>().canMove = true;
        player.GetComponent<Player>().setCanShoot(true);
        _myAnimation.SetBool("goToPopUp", false);
        initiateEvent.SetActive(true);
    }

    public bool getHelped()
    {
        return helped;
    }
}
