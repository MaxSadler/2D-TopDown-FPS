using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWeaponEffect : WeaponEffects
{

    //public float duration = 0.1f;
    //public GameObject muzzleFlash;
    public List<GameObject> bulletTracers;

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
                foreach (GameObject tracer in bulletTracers)
                {
                    tracer.SetActive(false);
                }
            }
        }
    }

    //TODO: set all the tracer to active
    //TODO: in the commented section below set the destination of the bullet tracers to:
    //Vector3.transform.right + 0.1f * Vector3.transform.up
    //Vector3.transform.right - 0.1f * Vector3.transform.up
    //Vector3.transform.right + 0.05f * Vector3.transform.up
    //Vector3.transform.right - 0.05f * Vector3.transform.up
    //Vector3.transform.right

    public override void activate()
    {
        muzzleFlash.SetActive(true);
        int i = -2;
        foreach (GameObject tracer in bulletTracers)
        {
            tracer.SetActive(true);
            tracer.GetComponent<LineRenderer>().SetPosition(1, ((Vector3.right) + (i*0.05f* Vector3.up)) * 20f);
            i++;
        }
        timeElapsed = 0f;
        isActive = true;
    }

}
