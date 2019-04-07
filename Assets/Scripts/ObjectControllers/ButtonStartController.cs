using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonStartController : MonoBehaviour {

    public float VolumeLevel;
    public Text VolumeDisplay;
    public Text MuteDisplay;
    public bool muted;




    public void loadSceneOne(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void quitApplication()
    {
        Debug.Log("Quits game");
        Application.Quit();
    }

    public void muteSound()
    {
        Debug.Log("Function called");
        if(muted == true)
        {
            MuteDisplay.text = "sound off";
            muted = false;
        }
        else if(muted == false)
        {
            MuteDisplay.text = "sound on";
            muted = true;
        }
    }




    // Difficulty will affect properties within LevelManager of gameplay scene

    public void easy()
    {
        Debug.Log("Changed to easy");
    }

    public void medium()
    {
        Debug.Log("Changed to medium");
    }
    public void hard()
    {
        Debug.Log("Changed to hard");
    }

    public void volumeUpController()
    {
        VolumeLevel++;
        Debug.Log("Volume Increased");
        if(VolumeLevel >= 10)
        {
            VolumeLevel = 10;
        }
    }

    public void volumeDownController()
    {
        VolumeLevel--;
        Debug.Log("Volume decreased");
        if(VolumeLevel <= 0)
        {
            VolumeLevel = 0;
        }
    }

    // Use this for initialization
    void Start () {
        VolumeLevel = 5;
	}
	
	// Update is called once per frame
	void Update () {
        VolumeDisplay.text = "" + VolumeLevel;

    }
}
