using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bud : MonoBehaviour
{
    const float GROWTH_SPEED = 0.1f;

    public float growth = 0f;

    public GameObject flower;

    void Start()
    {
        GlobalLogic.budDiscovered = true;
        growth = 0.1f;
        transform.localScale = Vector2.one * Mathf.Lerp(0f, 0.15f, growth);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            return;
        }

        if (growth >= 1f && !GameObject.Find("Global").GetComponent<GlobalLogic>().IsRaining())
        {
            Instantiate(flower, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        transform.localScale = Vector2.one * Mathf.Lerp(0f, 0.15f, growth);

        if (!GlobalLogic.Overlaps(gameObject, "Ground"))
        {
            growth -= Time.deltaTime * GROWTH_SPEED;

            if (growth <= 0)
            {
                Destroy(gameObject);
            }
            return;
        }

        growth += Time.deltaTime * GROWTH_SPEED;

        if (GameObject.Find("Global").GetComponent<GlobalLogic>().IsRaining())
        {
            growth += Time.deltaTime * GROWTH_SPEED;
        }

        growth = Mathf.Clamp(growth, -0.1f, 1f);
    }
}
