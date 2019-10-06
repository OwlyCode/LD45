﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalLogic.cakeDiscovered = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "HeroContainer")
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
