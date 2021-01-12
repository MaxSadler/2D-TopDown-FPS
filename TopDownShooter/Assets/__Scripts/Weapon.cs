using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public int magAmmo;
    public int maxCarryAmmo;
    public int defaultStartAmmo;
    public int BPM;//Bullets per minuet
    public bool isAutomatic = false;

    public GameObject weaponEffectsObject;
    protected WeaponEffects weaponEffects;
    protected int ammoInMag;
    protected int totalAmmo;
    //was private
    [HideInInspector]
    public bool selected = false;
    public Text text;
    public GameObject ammoBar;
    public GameObject reloadingGunSound;
    public GameObject shootGunSound;
    public GameObject muzzleFlash;

    protected float timeSinceLastShot;
    void Start()
    {
        ammoInMag = magAmmo;
        totalAmmo = defaultStartAmmo;
        text = GameObject.FindGameObjectWithTag("AmmoCounter").GetComponent<Text>();
        if(weaponEffectsObject != null)
            weaponEffects = weaponEffectsObject.GetComponent<WeaponEffects>();
        timeSinceLastShot = (60f / BPM);
    }

    void Update()
    {
        if (selected)
        {
            
            text.text = "<color=#FFE100>" + ammoInMag + "</color> <size=24>/ " + totalAmmo + "</size>";
            ammoBar.GetComponent<Image>().fillAmount = ((float)ammoInMag / magAmmo);
        }

        if (timeSinceLastShot < (60f / BPM))
            timeSinceLastShot += Time.deltaTime;
    }

    public virtual void shoot()
    {
        ammoInMag--;
        this.GetComponent<Animator>().SetTrigger("Shoot");
        shootGunSound.GetComponent<AudioSource>().Play();
        if (weaponEffectsObject != null)
            weaponEffects.activate();
        timeSinceLastShot = 0f;

        RaycastHit2D hit = Physics2D.Raycast(weaponEffectsObject.transform.position, weaponEffectsObject.transform.right);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.GetComponent<Health>() != null)
            {
                hit.collider.gameObject.GetComponent<Health>().AddDamage(1);
                hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-hit.collider.transform.up*100f);
            }
        }
    }

    public void reload()
    {
        if (totalAmmo > (magAmmo - ammoInMag))
        {
            totalAmmo -= (magAmmo - ammoInMag);
            ammoInMag = magAmmo;
            reloadingGunSound.GetComponent<AudioSource>().Play();
            this.GetComponent<Animator>().SetTrigger("Reload");
        }
        else
        {
            if (totalAmmo > 0)
            {
                ammoInMag += totalAmmo;
                totalAmmo = 0;
                reloadingGunSound.GetComponent<AudioSource>().Play();
                this.GetComponent<Animator>().SetTrigger("Reload");
            }
        }
    }

    public void equip()
    {
        selected = true;
    }

    public bool canShoot()
    {
        if (ammoInMag <= 0)
            return false;
        else if (timeSinceLastShot < (60f / BPM))
            return false;

        return true;
    }

    public bool isEmpty()
    {
        if (ammoInMag <= 0)
            return true;

        return false;
    }
}
