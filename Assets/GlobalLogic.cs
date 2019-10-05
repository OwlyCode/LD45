using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLogic : MonoBehaviour
{
    public GameObject puff;

    public void Puff(Vector2 position, float scale)
    {
        GameObject instance = Instantiate(puff, position, Quaternion.identity);
        instance.transform.localScale = Vector3.one * scale;

        Destroy(instance, instance.GetComponent<ParticleSystem>().main.duration);
    }
}
