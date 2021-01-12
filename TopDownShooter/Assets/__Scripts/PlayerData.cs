using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public Health playerHealth;
    public Weapon pistol;
    public Weapon rifle;
    public Weapon shotgun;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void recordValues()
    {
        PlayerPrefs.SetFloat("health", playerHealth.getHealth());
        PlayerPrefs.SetInt("rifleAmmo", rifle.defaultStartAmmo);
        PlayerPrefs.SetInt("pistolAmmo", pistol.defaultStartAmmo);
        PlayerPrefs.SetInt("shotgunAmmo", shotgun.defaultStartAmmo);
        PlayerPrefs.Save();
    }

    public void applyValues()
    {
        playerHealth.setHealth(PlayerPrefs.GetFloat("health"));
        rifle.defaultStartAmmo = PlayerPrefs.GetInt("rifleAmmo");
        pistol.defaultStartAmmo = PlayerPrefs.GetInt("pistolAmmo");
        shotgun.defaultStartAmmo = PlayerPrefs.GetInt("shotgunAmmo");
    }
}
