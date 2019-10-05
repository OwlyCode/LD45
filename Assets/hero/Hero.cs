using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    float speed = 2f;

    GameObject carried = null;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !carried)
        {
            transform.Find("hero").gameObject.GetComponent<Animator>().SetTrigger("jump");

            GameObject.Find("Spawn").GetComponent<RockDropper>().Jump();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (carried)
            {
                DropItem();
            } else
            {

                PickItem();
            }
        }
    }

    void DropItem()
    {
        carried.transform.parent = null;
        carried.GetComponent<DraggableItem>().Drop(transform.Find("hero").transform.position);
        carried.layer = LayerMask.NameToLayer("Droppable");
        carried.GetComponent<Depth>().parentBased = false;
        carried.GetComponent<Depth>().depthOffset = 0f;
        carried = null;
    }

    void PickItem()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();

        GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders);

        int i = 0;
        bool found = false;

        while (i < colliders.Count && !found)
        {
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Droppable"))
            {
                found = true;
                carried = colliders[i].gameObject;
                carried.layer = LayerMask.NameToLayer("Dragged");
                carried.transform.parent = transform;
                carried.transform.position = transform.position + new Vector3(0f, 0.098f);
                carried.GetComponent<Depth>().parentBased = true;
                carried.GetComponent<Depth>().depthOffset = -0.1f;
            }

            i++;
        }
    }
}
