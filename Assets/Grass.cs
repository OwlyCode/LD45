using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    const float GROWING_TIME = 2.5f;
    const float GROW_RANGE = 1f;
    const int GROW_COUNT = 1;
    const int MAX_GENERATION = 1;
    const float GROWTH_SPEED = 0.1f;

    float cooldown = GROWING_TIME;
    int growCount = 0;

    public float growth = 0f;

    public GameObject grass;
    public GameObject bud;
    public int generation = 0;

    void Start()
    {
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

        transform.localScale = Vector2.one * Mathf.Lerp(0f, 0.15f, growth);

        if (GameObject.Find("Global").GetComponent<GlobalLogic>().IsRaining())
        {
            generation = 0;
            growCount = GROW_COUNT;
        }

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

        growth = Mathf.Clamp(growth, - 0.1f, 1f);


        if (growCount == 0 || generation >= MAX_GENERATION || growth < 1f)
        {
            return;
        }

        cooldown -= Time.deltaTime;

        if (cooldown < 0)
        {
            Vector2 pos = transform.position + (Vector3)(Random.insideUnitCircle * GROW_RANGE);

            GameObject prefab = Random.Range(0f, 100f) > 85f ? bud : grass;

            GameObject child = Instantiate(prefab, pos, Quaternion.identity);

            if (prefab == grass)
            {
                child.GetComponent<Grass>().generation = generation + 1;
            }

            cooldown = GROWING_TIME;
            growCount--;
        }
    }
}
