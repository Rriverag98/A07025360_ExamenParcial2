using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider2 : MonoBehaviour { 
    public string level;



    // Method is triggered when object touches the Losecollider 
    //Then loads a screen
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            LevelManager levelMngr = new LevelManager();
            levelMngr.LoadLevel(level);
        }
    }
}