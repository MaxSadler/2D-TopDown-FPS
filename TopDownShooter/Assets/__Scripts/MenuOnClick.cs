using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOnClick : MonoBehaviour
{
    public GameObject maleText;  // Initializing the text objects that will be modified 
    public GameObject femaleText;
    public GameObject medicText;
    public GameObject boltText;
    [HideInInspector]
    public static bool isCurrentlyMale = false; // Keeping track of what options the user has selected
    [HideInInspector]
    public static bool isCurrentlyFemale = false;
    [HideInInspector]
    public static bool isMedic = false;
    [HideInInspector]
    public static bool isBolt = false;
 
    public void SelectedPlayer(GameObject caller) // Method responsible for character selection
    {
        if((caller == maleText) && !isCurrentlyMale) // If the caller of this method is equal to the maleText object then proceed
        {
            maleText.GetComponent<Text>().color = Color.green; // Changes maleText to green 
            femaleText.GetComponent<Text>().color = Color.white; // Changes FemaleText to white 
            isCurrentlyMale = true; // Sets the selected player to male
            isCurrentlyFemale = false;
        }
        else if ((caller == femaleText) && !isCurrentlyFemale)
        {
            femaleText.GetComponent<Text>().color = Color.green;
            maleText.GetComponent<Text>().color = Color.white;
            isCurrentlyFemale = true;
            isCurrentlyMale = false;
        } 
    }

    public void SelectedArchetype(GameObject caller) // Sets the selected archetype 
    {
        if((caller == medicText) && !isMedic) // If caller of this method equals the medicText obect then proceed
        {
            medicText.GetComponent<Text>().color = Color.green; // Sets the medic text to green 
            boltText.GetComponent<Text>().color = Color.white; // Sets the boltText to white 
            isMedic = true; // Sets the selected archetype to medic 
            isBolt = false;
        }
        else if ((caller == boltText) && !isBolt)
        {
            boltText.GetComponent<Text>().color = Color.green;
            medicText.GetComponent<Text>().color = Color.white;
            isBolt = true;
            isMedic = false;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Demo"); // Loads the first scene in playable game
    }
}
