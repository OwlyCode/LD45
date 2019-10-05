using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    List<GameObject> ingredients = new List<GameObject>();
    bool processing = false;

    void Update()
    {
        if (processing)
        {
            return;
        }

        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders);
        int i = 0;

        if (colliders.Count < 2)
        {
            return;
        }

        ingredients = new List<GameObject>();

        while (i < colliders.Count && ingredients.Count < 2)
        {
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Droppable"))
            {
                ingredients.Add(colliders[i].gameObject);
            }

            i++;
        }

        if (ingredients.Count == 2)
        {
            ingredients[0].GetComponent<DraggableItem>().Shake();
            ingredients[0].layer = LayerMask.NameToLayer("Reactant");
            ingredients[1].GetComponent<DraggableItem>().Shake();
            ingredients[1].layer = LayerMask.NameToLayer("Reactant");
            processing = true;
            StartCoroutine(Process());
        }
    }

    IEnumerator Process()
    {
        yield return new WaitForSeconds(3f);

        Destroy(ingredients[0]);
        Destroy(ingredients[1]);
        GameObject.Find("Global").GetComponent<GlobalLogic>().Puff(transform.position, 1f);
        processing = false;
    }
}
