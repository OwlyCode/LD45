using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    int stage;
    const float TEXT_DELAY = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(stage)
        {
            case 0:
                SetText("Feeling lonely? Let's make a cake! Use space to jump.");
                if(Input.GetKey("space"))
                {
                    stage++;
                }
                break;
            case 1:
                SetText("Oh, it caused a rock to crash. I wonder what happens if you drag two of them in here. Use E to pick up an item.");
                if (GlobalLogic.fireDiscovered)
                {
                    stage++;
                }
                break;
            case 2:
                SetText("Well I see you got this. Can you make a cake by combining items ? You can ask for help by pressing H.", TEXT_DELAY);
                stage++;
                break;
        }

        if (stage > 2 && Input.GetKeyDown(KeyCode.H))
        {
            Help();
        }
    }

    void Help()
    {
        if (!GlobalLogic.rainDiscovered)
        {
            SetText("Maybe you could build up some steam? Grab some snow!", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.potteryDiscovered)
        {
            SetText("We could use some sort of pot to carry the water.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.grassDiscovered)
        {
            SetText("I think it's time to make some mud.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.splashDirtDiscovered)
        {
            SetText("I wonder what would happen if you jumped on a pile of dirt...", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.budDiscovered)
        {
            SetText("You should spread that grass around. Don't forget some water too, the weather is dry nowadays.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.chickDiscovered)
        {
            SetText("This egg will never hatch without a nest.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.seedsDiscovered)
        {
            SetText("Have you noticed you can smash grass? Make some space for the flowers!", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.flourDiscovered)
        {
            SetText("Those seeds over there can't be used for the cake without being refined.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.chickenDiscovered)
        {
            SetText("This chick will need to feed on flowers in order to grow.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.eggDiscovered)
        {
            SetText("Chicken fact n°1445: there are plenty of good nutriments in seeds.", TEXT_DELAY);
            return;
        }

        if (!GlobalLogic.cakeDiscovered)
        {
            SetText("Carry on! Now there's everything you need to make the cake.", TEXT_DELAY);
            return;
        }

        if (GlobalLogic.cakeDiscovered)
        {
            SetText("Go get your cake!", TEXT_DELAY);
            return;
        }
    }

    GlobalLogic GetGlobalLogic()
    {
        return GameObject.Find("Global").GetComponent<GlobalLogic>();
    }

    string currentMessage;

    void SetText(string text, float fadeDelay = 0f)
    {
        if(text == "")
        {
            transform.parent.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
        } else
        {
            transform.parent.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
        }

        if (text == currentMessage)
        {
            return;
        }

        currentMessage = text;

        StopAllCoroutines();
        StartCoroutine(TypeSentence());

        if (fadeDelay > 0f)
        {
            StartCoroutine(AutoHide(fadeDelay));
        }
    }
    IEnumerator AutoHide(float fadeDelay)
    {
        yield return new WaitForSeconds(fadeDelay);

        SetText("");
    }

    IEnumerator TypeSentence()
    {
        GetComponent<Text>().text = "";
        int i = 0;

        foreach (char letter in currentMessage.ToCharArray())
        {
            GetComponent<Text>().text += letter;
            if (i % 3 == 0)
            {
                yield return new WaitForSeconds(0.1f);
            }

            i++;
        }
    }
}
