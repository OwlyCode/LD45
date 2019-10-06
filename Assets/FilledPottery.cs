using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledPottery : MonoBehaviour
{
    public Sprite filledSprite;
    public Sprite droppedSprite;

    void Update()
    {
        if (transform.parent == null && GlobalLogic.Overlaps(gameObject, "Water"))
        {
            GetComponent<SpriteRenderer>().sprite = droppedSprite;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = filledSprite;
        }
    }
}
