using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolActivate : MonoBehaviour
{

    public float maxSize;
    public float minSize;
    public float timer;

    private float growthSize;
	private float temp;
    public bool isActivated;

    void Update()
    {
        if (isActivated)
        {
            Debug.Log(growthSize);
            transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
            growthSize += 0.1f;
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
            growthSize -= 0.1f;
        }
        growthSize = Mathf.Clamp01(growthSize);
        if (growthSize >= 1.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && isActivated)
            {

                timer = 5.0f;
                isActivated = false;
            }

        }
        else if (growthSize <= 0f)
        {
            timer -= Time.deltaTime;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E) && timer <=0 )
        {
            AudioManager.instance.AudioAccess(8);
            isActivated = true;
            timer = 5;
        }
    }

    void ActivateWhirlpool()
    {
        transform.localScale = new Vector3(Mathf.Lerp(minSize, maxSize, growthSize), Mathf.Lerp(minSize, maxSize, growthSize), 0.06062245f);
        growthSize += 0.1f;
        
    }
}
