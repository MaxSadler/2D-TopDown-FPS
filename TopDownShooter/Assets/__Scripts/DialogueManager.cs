using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueItem
{
    public string personName;
    public string dialogue;

}

public class DialogueManager : MonoBehaviour
{
    public List<DialogueItem> dialoguesItems;
    public Queue<DialogueItem> dialogues = new Queue<DialogueItem>();
    public GameObject dialogueGroup;
    public Text nameText;
    public Text dialogueText;
    public bool isActive = false;

    public void Init()
    {
        foreach (DialogueItem dialogueItem in dialoguesItems)
        {
            dialogues.Enqueue(dialogueItem);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = false;
        }

        dialogueGroup.SetActive(true);
        DialogueItem dialogue = dialogues.Dequeue();
        nameText.text = dialogue.personName;
        dialogueText.text = dialogue.dialogue;
        isActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isActive)
            {
                if (dialogues.Count > 0)
                {
                    DialogueItem dialogue = dialogues.Dequeue();
                
                    nameText.text = dialogue.personName;
                    dialogueText.text = dialogue.dialogue;
                }
                else
                {
                    Close();
                }
            }
        }
    }

    public void Close (){
        dialogues.Clear();
        dialogueGroup.SetActive(false);
        isActive = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
    }

}
