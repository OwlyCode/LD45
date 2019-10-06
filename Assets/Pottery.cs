using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pottery : MonoBehaviour
{
    public GameObject potteryInWater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null && GlobalLogic.Overlaps(gameObject, "Water"))
        {
            Destroy(gameObject);
            Instantiate(potteryInWater, transform.position, Quaternion.identity);
        }
    }
}
