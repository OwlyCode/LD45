using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
    const float MOVE_SPEED = 1f;
    const float MOVE_RADIUS = 0.5f;
    const int NEEDED_SEEDS = 3;

    GameObject target;

    Vector2 randomTarget = Vector2.zero;

    int eatedSeeds = 0;
    public GameObject seeds;
    public GameObject chicken;

    float eatCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        {
            RefreshRandomTarget();
            return;
        }

        if (eatCooldown >= 0f)
        {
            eatCooldown -= Time.deltaTime;

            RandomWalk();
            return;
        }

        if(!target)
        {
            target = GetTarget();

            RandomWalk();
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * MOVE_SPEED);
        FixDepth();

        if (Vector2.Distance(target.transform.position, transform.position) < 0.01f)
        {
            // eat
            GlobalLogic.seedsDiscovered = true;
            Instantiate(seeds, target.transform.position, Quaternion.identity);
            Destroy(target);
            eatCooldown = 10f;
            RefreshRandomTarget();
            eatedSeeds++;
        }

        if (eatedSeeds == NEEDED_SEEDS)
        {
            Destroy(gameObject);
            Instantiate(chicken, transform.position, Quaternion.identity);
            GameObject.Find("Global").GetComponent<GlobalLogic>().Puff(transform.position, 0.5f);
        }
    }

    void RandomWalk()
    {
        if (randomTarget == Vector2.zero || (Vector2.Distance(randomTarget, transform.position) < 0.01f))
        {
            RefreshRandomTarget();
        }

        transform.position = Vector2.MoveTowards(transform.position, randomTarget, Time.deltaTime * MOVE_SPEED);
        FixDepth();
    }

    void RefreshRandomTarget()
    {
        float x = Mathf.Clamp(Random.Range(-MOVE_RADIUS, MOVE_RADIUS), -7f, 7f);
        float y = Mathf.Clamp(Random.Range(-MOVE_RADIUS, MOVE_RADIUS), -7f, 7f);
        randomTarget = (Vector2)transform.position + new Vector2(x, y);
    }

    void FixDepth()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 1f);
    }


    GameObject GetTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Flower");

        if (targets.Length == 0)
        {
            return null;
        }

        return targets[0];
    }
}
