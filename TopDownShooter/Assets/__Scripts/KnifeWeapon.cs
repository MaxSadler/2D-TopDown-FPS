using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeWeapon : Weapon {

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float damage = 1f;

    public override void shoot()
    {
        this.GetComponent<Animator>().SetTrigger("Shoot");

        //Detect enemies in range of attacks
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponent<Health>().AddDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public virtual void reload()
    {

    }

    public void Update()
    {
        if (selected)
        {

            text.text = "<color=#FFE100>∞</color> <size=24>/ ∞</size>";
            ammoBar.GetComponent<Image>().fillAmount = 1f;
        }

        if (timeSinceLastShot < (60f / BPM))
            timeSinceLastShot += Time.deltaTime;
    }

}
