using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundTrackManager : MonoBehaviour
{
    void Start()
    {
        Scene curentScene = SceneManager.GetActiveScene();
        string curentSoundTrack = FindObjectOfType<AudioManager>().isPlaying;

        if (curentScene.name == "MainMenu")
        {
            Debug.Log("MainMenu");
            FindObjectOfType<AudioManager>().Stop("ST_MainTheme");

            FindObjectOfType<AudioManager>().Play("ST_MainMenu");
            FindObjectOfType<AudioManager>().isPlaying = "ST_MainMenu";
        }

        if (curentScene.name != "MainMenu")
        {
            if (curentSoundTrack == "ST_MainMenu") //Checking if the song is already playing
            {
                FindObjectOfType<AudioManager>().Stop("ST_MainMenu");

                FindObjectOfType<AudioManager>().Play("ST_MainTheme");
                FindObjectOfType<AudioManager>().isPlaying = "ST_MainTheme";
            }

            else if (curentSoundTrack != "ST_MainTheme") //Checking if the song is already playing
            {
                FindObjectOfType<AudioManager>().Play("ST_MainTheme");
                FindObjectOfType<AudioManager>().isPlaying = "ST_MainTheme";
            }
        }
    }
}
