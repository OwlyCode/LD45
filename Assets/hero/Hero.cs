using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    float speed = 2f;

    float jumpDuration = 0.25f;

    GameObject carried = null;

    public GameObject snow;

    public GameObject dirt;

    void Update()
    {
        GetComponent<Animator>().SetBool("moving", false);
        GetComponent<Animator>().SetBool("grabbing", carried != null);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + Vector3.up * speed * Time.deltaTime;
            GetComponent<Animator>().SetBool("moving", true);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + Vector3.down * speed * Time.deltaTime;
            GetComponent<Animator>().SetBool("moving", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
            GetComponent<Animator>().SetBool("moving", true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
            GetComponent<Animator>().SetBool("moving", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !carried)
        {
            GetComponent<Animator>().SetTrigger("jump");

            GameObject.Find("Spawn").GetComponent<RockDropper>().Jump();

            StartCoroutine(DoJump());
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

    IEnumerator DoJump()
    {
        yield return new WaitForSeconds(jumpDuration);

        foreach (GameObject jumpedOn in GlobalLogic.GetOverlapped(gameObject, "Droppable"))
        {
            jumpedOn.SendMessage("OnJumpedOn");
        }
    }

    void DropItem()
    {
        carried.transform.parent = null;
        carried.GetComponent<DraggableItem>().Drop(transform.Find("Body").transform.position + Vector3.down * 0.3f);
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
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Droppable") && colliders[i].gameObject.GetComponent<DraggableItem>().type != "fire")
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

        i = 0;

        while (i < colliders.Count && !found)
        {
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Ground") || colliders[i].gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                Instantiate(dirt, transform.position, Quaternion.identity);
                PickItem();

                return;
            }

            i++;
        }

        if (!found)
        {
            Instantiate(snow, transform.position, Quaternion.identity);
            PickItem();
        }
    }
}
