using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public GameObject splashedDirt;

    void OnJumpedOn()
    {
        GameObject dirt = Instantiate(splashedDirt, transform.position, Quaternion.identity);
        dirt.GetComponent<DirtOnGround>().fadeIn = false;
        Destroy(gameObject);
        GlobalLogic.splashDirtDiscovered = true;
    }
}
