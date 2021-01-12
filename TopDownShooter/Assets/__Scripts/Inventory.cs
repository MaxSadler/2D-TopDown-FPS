using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject weaponSlot1; // Initializing gameObjects from inspector to populate array with 
    public GameObject weaponSlot2;
    public GameObject weaponSlot3;
    public GameObject weaponSlot4;
    private GameObject[] weaponsList = new GameObject[4]; // Array of gameObjects that holds the weapons
    private int currentWeapon = 0; // Tracks index of currently equipped weapon in array
    public int unlockedWeapons = 1;

    public Animator weaponPanelAnimator;
    public List<GameObject> weaponButtons = new List<GameObject>();

    private GameObject currentWeaponObject;
    private GameObject selectedWeapon;

    private GameObject currentButton;
    private GameObject nextButton;

    public int currentButtonIndex = 0;
    private int nextButtonIndex = 0;

    private Animator currentWeaponAnimator;

    private Animator currentButtonAnimator;
    private Animator nextButtonAnimator;


    private void Start()
    {
        weaponsList[0] = weaponSlot1; //Populating weaponsList at start of scene
        weaponsList[1] = weaponSlot2;
        weaponsList[2] = weaponSlot3;
        weaponsList[3] = weaponSlot4;

        for(int i = 0; i < weaponsList.Length; i++)
        {
            if(weaponsList[i].GetComponent<Weapon>().selected == true)
            {
                currentWeapon = i;
            }
        }

        for (int i = unlockedWeapons; i < weaponButtons.Count; i++)
        {
            weaponButtons[i].SetActive(false);

        }
    }
    public void EquipWeapon(int i)
    {
        

        if (i != currentWeapon && i < unlockedWeapons) // Executes if block when the weapon to be equipped is not the same as the currently used weapon
        {
            currentButton = weaponButtons[currentWeapon];
            nextButton = weaponButtons[i];

            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play("WS Fade-out");

            nextButtonAnimator = nextButton.GetComponent<Animator>();
            nextButtonAnimator.Play("WS Fade-in");

            weaponPanelAnimator.Play("WS Fade-in");

            weaponsList[i].SetActive(true); // Activates the associated gameObject at weaponsList[i]
            weaponsList[currentWeapon].SetActive(false); // Deactivates the current weapon gameObject
            gameObject.GetComponent<Player>().selectedWeapon = weaponsList[i]; // Updating field variables in Player
            gameObject.GetComponent<Player>().weapon = weaponsList[i].GetComponent<Weapon>();
            weaponsList[i].GetComponent<Weapon>().equip(); // Calling equip method in the weapons class 
            currentWeapon = i; // updating current weapon to the newly equipped weapon
        }

        Invoke("CoolDown", 0.2f); // Calls cooldown method 0.2 seconds after weapon has been equipped
    }
    private void CoolDown() // Responsible for ensuring the EquipWeapon Method is called once per user input
    {
        gameObject.GetComponent<Player>().oneTime = true; // Allows player to toggle inventory from Player.cs Update method again
    }

    public GameObject getCurrentWeapon()
    {
        return weaponsList[currentWeapon]; // Returns the gameObject that represents the currently equipped weapon
    }

    public void unlockWeapon()
    {
        unlockedWeapons++;
        weaponButtons[unlockedWeapons-1].SetActive(true);
        EquipWeapon(unlockedWeapons-1);
    }
}
