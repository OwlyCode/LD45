using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lake : MonoBehaviour
{
    float growth = 0f;
    float growthSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Global").GetComponent<GlobalLogic>().IsRaining())
        {
            growth += growthSpeed * Time.deltaTime;
        }

        transform.localScale = Vector3.one * Mathf.Lerp(0f, 0.7f, growth);
    }
}
