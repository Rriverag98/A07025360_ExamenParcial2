﻿using UnityEngine;
using UnityEngine.SceneManagement;			//	Required to handle scenes
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;     //Singleton used to load the music player

    public AudioClip startClip;             //Clip to play at the start menu
    public AudioClip loseClip;               //Clip to play at the lost screen
    public AudioClip winClip;               //Clip to play at the win screen
    public AudioClip gameClip;              //Clip to play during the game
    public AudioClip almost;               //Clip to play at the 4yh level
    public AudioClip ending;               //Clip to play at the bonus scene


    private AudioSource music;              //	We use it to play the right music

    void Start()
    {
        //	We register a delegate to SceneManager.sceneLoaded because the OnLevelWasLoaded() method
        //	was deprecated.
        SceneManager.sceneLoaded += this.OnLoadCallBack;

        //	If the music player already exists, then the new instance is destroyed; otherwise, we
        //	create an instance of the music player and set it to not be destroyed upon a new load.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructing!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);

            //	We also define our starting audio clip, because the OnLoadCallBack is not run until
            //	another scene is loaded.  We set our clip to play in a loop and then start playing it.
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }

    }

    //	This method is called everytime a scene is loaded
    void OnLoadCallBack(Scene scene, LoadSceneMode sceneMode)
    {
        //	Once the level is loaded, we stop any music being played
        music.Stop();

        //	Then we chose the clip to play, based on the loaded level
        if (scene.buildIndex == 0)
        {
            music.clip = startClip;
        }
        else if (scene.buildIndex == 1)
            {
                music.clip = loseClip;
            }
            else if (scene.buildIndex == 2)
                {
                    music.clip = winClip;
                }
                else if (scene.buildIndex == 6)
                    {
                        music.clip = almost;
                    }
                    else if (scene.buildIndex == 7)
                        {
                            music.clip = ending;
                        }
                        else
                        {
                            music.clip = gameClip;
                        }
                        //	We make sure the clip plays as 2D sound
                        music.spatialBlend = 0;

                        //	We make sure that the clip will play in a loop
                        music.loop = true;

                        //	We start playing the selected clip
                        music.Play();
                    }
                }
            
        
