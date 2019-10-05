using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Depth : MonoBehaviour
{
    public float depthOffset = 0f;
    public GameObject target = null;

    void Update()
    {
        GameObject manipulated = gameObject;

        if (target)
        {
            manipulated = target;
        }

        manipulated.transform.position = new Vector3(manipulated.transform.position.x, manipulated.transform.position.y, depthOffset + manipulated.transform.position.y * 1f);
    }
}
