using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public string type;
    Vector3 target;
    bool dropped = true;
    bool puff = false;

    public void Drop(Vector2 target, bool puff = false)
    {
        this.target = target;
        dropped = false;
        this.puff = puff;
    }

    void OnJumpedOn()
    {
        // noop
    }

    // Update is called once per frame
    void Update()
    {
        if (!dropped)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 15f * Time.deltaTime);
            GetComponent<Collider2D>().enabled = false;
        }

        if (transform.position == target)
        {
            if (dropped == false && puff == true)
            {
                GameObject.Find("Global").GetComponent<GlobalLogic>().Puff(transform.position, 0.5f);
            }

            dropped = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        transform.Rotate(0f, 0f, -8f);

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Rotate(0f, 0f, 16f);
            yield return new WaitForSeconds(0.1f);
            transform.Rotate(0f, 0f, -16f);
        }
    }
}
