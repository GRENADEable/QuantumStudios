using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolActivate : MonoBehaviour
{

    public float maxSize;
    public float minSize;
    public float timer;

    private float growthSpeed;
	private float temp;
    public bool isActivated;
    void Start()
    {

    }


    void FixedUpdate()
    {
        if (isActivated)
        {
            ActivateWhirlpool();
        }
        if (growthSpeed > 1.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && isActivated)
            {
                temp = maxSize;
                maxSize = minSize;
                minSize = temp;
                growthSpeed = 0.0f;
                timer = 5.0f;
            }
            else if (timer == 5.0f && !isActivated)
                isActivated = false;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            isActivated = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isActivated = false;
        }
    }
    void ActivateWhirlpool()
    {
        transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSpeed), Mathf.Lerp(minSize, maxSize, growthSpeed), 0.06062245f);
        growthSpeed += 0.1f;
    }


}
