using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Vector3 target;
    bool dropped = false;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!dropped)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 15f * Time.deltaTime);
        }

        if (transform.position == target)
        {
            if (dropped == false)
            {
                GameObject.Find("Global").GetComponent<GlobalLogic>().Puff(transform.position, 0.5f);
            }

            dropped = true;
        }
    }
}
