using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Vector3 target;
    bool dropped = true;
    bool puff = false;

    public void Drop(Vector2 target, bool puff = false)
    {
        this.target = target;
        dropped = false;
        this.puff = puff;
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
            if (dropped == false && puff == true)
            {
                GameObject.Find("Global").GetComponent<GlobalLogic>().Puff(transform.position, 0.5f);
            }

            dropped = true;
        }
    }
}
