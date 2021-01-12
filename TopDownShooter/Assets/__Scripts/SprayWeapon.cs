using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWeapon : Weapon
{
    
    public float damage = 1f;
    public override void shoot()
    {
        ammoInMag--;
        this.GetComponent<Animator>().SetTrigger("Shoot");
        shootGunSound.GetComponent<AudioSource>().Play();
        weaponEffects.activate();
        timeSinceLastShot = 0f;

        List<Vector3> sprayPositions = new List<Vector3>();
        sprayPositions.Add(weaponEffectsObject.transform.right);
        sprayPositions.Add(weaponEffectsObject.transform.right + 0.1f * weaponEffectsObject.transform.up);
        sprayPositions.Add(weaponEffectsObject.transform.right - 0.1f * weaponEffectsObject.transform.up);
        sprayPositions.Add(weaponEffectsObject.transform.right + 0.05f * weaponEffectsObject.transform.up);
        sprayPositions.Add(weaponEffectsObject.transform.right - 0.05f * weaponEffectsObject.transform.up);

        foreach (Vector3 destination in sprayPositions)
        {
            RaycastHit2D hit = Physics2D.Raycast(weaponEffectsObject.transform.position, destination);
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    hit.collider.gameObject.GetComponent<Health>().AddDamage(damage);
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-hit.collider.transform.up * 100f);
                }
            }
        }
    }

}
