using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLogic : MonoBehaviour
{
    public GameObject puff;
    bool raining = false;
    public static bool fireDiscovered = false;
    public static bool rainDiscovered = false;
    public static bool potteryDiscovered = false;
    public static bool splashDirtDiscovered = false;
    public static bool grassDiscovered = false;
    public static bool flourDiscovered = false;
    public static bool chickDiscovered = false;
    public static bool budDiscovered = false;
    public static bool chickenDiscovered = false;
    public static bool eggDiscovered = false;

    public static List<GameObject> GetOverlapped(GameObject source, string layer)
    {
        List<GameObject> items = new List<GameObject>();

        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();

        source.GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders);

        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                items.Add(col.gameObject);
            }
        }

        return items;
    }

    public static bool Overlaps(GameObject source, string layer)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();

        source.GetComponent<Collider2D>().OverlapCollider(contactFilter, colliders);

        int i = 0;

        while (i < colliders.Count)
        {
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer(layer))
            {
                return true;
            }

            i++;
        }

        return false;
    }

    public bool IsRaining()
    {
        return raining;
    }

    public void Puff(Vector2 position, float scale)
    {
        GameObject instance = Instantiate(puff, position, Quaternion.identity);
        instance.transform.localScale = Vector3.one * scale;

        Destroy(instance, instance.GetComponent<ParticleSystem>().main.duration);
    }

    public void Rain(float duration = 15f)
    {
        StartCoroutine(DoRain(duration));
    }

    IEnumerator DoRain(float duration)
    {
        ParticleSystem rain = GameObject.Find("Rain").GetComponent<ParticleSystem>();
        rain.Play();
        raining = true;
        yield return new WaitForSeconds(duration);
        rain.Stop();
        raining = false;
    }
}
