using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    List<GameObject> ingredients = new List<GameObject>();
    bool processing = false;

    public GameObject fire;
    public GameObject dirt;
    public GameObject pottery;
    public GameObject grass;

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
            string recipe = GetRecipe(ingredients);

            if (recipe != "nothing")
            {
                ingredients[0].GetComponent<DraggableItem>().Shake();
                ingredients[0].layer = LayerMask.NameToLayer("Reactant");
                ingredients[1].GetComponent<DraggableItem>().Shake();
                ingredients[1].layer = LayerMask.NameToLayer("Reactant");
                processing = true;
                StartCoroutine(Process(recipe));
            }
        }
    }

    static string GetRecipe(List<GameObject> ingredients)
    {
        string typeA = ingredients[0].GetComponent<DraggableItem>().type;
        string typeB = ingredients[1].GetComponent<DraggableItem>().type;

        return GetRecipe(typeA, typeB);
    }

    static string GetRecipe(string typeA, string typeB, bool invert = true)
    {
        if (typeA == "rock" && typeB == "rock")
        {
            return "fire";
        }

        if (typeA == "snow" && typeB == "fire")
        {
            return "rain";
        }

        if (typeA == "filled_pottery" && typeB == "fire")
        {
            return "rain";
        }

        if (typeA == "rock" && typeB == "water")
        {
            return "dirt";
        }

        if (typeA == "dirt" && typeB == "fire")
        {
            return "pottery";
        }

        if (typeA == "dirt" && typeB == "water")
        {
            return "grass";
        }

        if (invert)
        {
            return GetRecipe(typeB, typeA, false);
        }

        return "nothing";
    }

    IEnumerator Process(string recipe)
    {
        yield return new WaitForSeconds(3f);

        Destroy(ingredients[0]);
        Destroy(ingredients[1]);
        GameObject.Find("Global").GetComponent<GlobalLogic>().Puff(transform.position, 1f);
        processing = false;

        switch (recipe)
        {
            case "fire":
                Instantiate(fire, transform.position, Quaternion.identity);
                break;
            default:
                Debug.LogError("Unknwon recipe " + recipe);
                break;
        }
    }
}
