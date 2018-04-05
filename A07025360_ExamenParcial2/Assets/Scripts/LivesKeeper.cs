using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesKeeper : MonoBehaviour {

    public static int lives = 3;            //	Player lives
    private Text scoreText;                 //	So we can modify the lives Text

    // Use this for initialization
    void Start()
    {
        //	We dynamicaly point to the Text object in our UI.
        scoreText = GetComponent<Text>();
        scoreText.text = lives.ToString();
    }

    // Update is called once per frame
    void Update(){

    }

    public void Hurt()
    {
        lives --;
        scoreText.text = lives.ToString();
        if (lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        LevelManager levelManager = GameObject.Find("LevelMngr").GetComponent<LevelManager>();
        levelManager.LoadLevel("GameOver");
        Destroy(gameObject);

    }

}
