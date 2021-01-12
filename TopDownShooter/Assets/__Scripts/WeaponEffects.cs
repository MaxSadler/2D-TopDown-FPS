using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    public float duration = 0.1f;
    public GameObject muzzleFlash;
    public GameObject bulletTracer;

    protected bool isActive = false;
    protected float timeElapsed = 0f;
    private LineRenderer lr;

    private void Start()
    {
        if (bulletTracer != null)
            lr = bulletTracer.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isActive)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > duration)
            {
                isActive = false;
                timeElapsed = 0f;
                muzzleFlash.SetActive(false);
                bulletTracer.SetActive(false);
            }
        }
    }

    public virtual void activate()
    {
        muzzleFlash.SetActive(true);
        lr.SetPosition(1, (Vector3.right) * 20f);
        bulletTracer.SetActive(true);
        timeElapsed = 0f;
        isActive = true;
    }

    public void activate(Vector3 target)
    {
        muzzleFlash.SetActive(true);
        lr.SetPosition(1, target);
        bulletTracer.SetActive(true);
        timeElapsed = 0f;
        isActive = true;
    }
}
