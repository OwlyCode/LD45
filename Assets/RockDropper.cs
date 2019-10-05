using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDropper : MonoBehaviour
{
    public GameObject rock;

    bool insideSpawn = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        insideSpawn = other.gameObject.name == "HeroContainer";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "HeroContainer")
        {
            insideSpawn = false;
        }
    }

    public void Jump()
    {
        if (insideSpawn)
        {
            Vector2 rockTarget = Random.insideUnitCircle * 4;

            while (Vector2.Distance(rockTarget, transform.position) < 1f)
            {
                rockTarget = Random.insideUnitCircle * 4;
            }

            GameObject rockInstance = Instantiate(rock, rockTarget + new Vector2(0, 15f), Quaternion.identity);
            rockInstance.GetComponent<Rock>().Drop(rockTarget, true);
        }
    }
}
