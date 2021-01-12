using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float followSpeed = 1f;
    private bool shake = false;
    private Vector3 defaultPos;
    public float duration = 0.05f;
    public float shakeStrength = 0.05f;
    private float timePassed = 0f;
    void Start()
    {
        defaultPos = this.transform.position;

    }

    private void Update()
    {
        if (shake)
        {
            if(timePassed < duration)
            {
                this.transform.position = defaultPos + Random.onUnitSphere * shakeStrength;
                timePassed += Time.deltaTime;
            }
            else
            {
                this.transform.position = defaultPos;
                shake = false;
                timePassed = 0f;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), followSpeed);
        defaultPos = this.transform.position;
    }

    public void startShake()
    {
        shake = true;
        timePassed = 0f;
    }
}
