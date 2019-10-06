using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtOnGround : MonoBehaviour
{
    float ellapsedTime = 0;
    public bool fadeIn = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            ellapsedTime += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), ellapsedTime / 10f);
        }
    }
}
