using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
     
    [SerializeField] Slider Slider;                      //To serialize a private field
    [SerializeField] Button Sound;
    [SerializeField] Image soundIcon;
    [SerializeField] Image soundOffIcon;
    bool muted=false;
    // Start is called before the first frame update
    void Start()
    {
        soundIcon.enabled = true;
        soundOffIcon.enabled = false;
        Sound.onClick.AddListener(MuteOrUnMute);
        if(!PlayerPrefs.HasKey("musicvolume"))            //playerprefs stores and access player preferences between game sessions
        {
            PlayerPrefs.SetFloat("musicvolume",1);              //Intially the volume will be 100%
            Load();
        }
        else
        {
            Load();              //if their is saved data from the previous game session we will call load function
        }
    }

    public void ChangeVolume()                    
    {
        AudioListener.volume = Slider.value;    //Volume  of our game will be equals to the value of our slider
        Save();                                //calls the save function when ever the player changes the value of volume slider
    }

    private void Load()
    {
        //Get float funtion is used to retrive our data
        Slider.value = PlayerPrefs.GetFloat("musicvolume");
    }

    private void Save()
    {
        //We used set float function to save our data
        //It stores the value of volume slider into the "musicvolume" key name
        PlayerPrefs.SetFloat("musicvolume", Slider.value);    

    }
    private void MuteOrUnMute()
    {
        if(muted==false)
        {
            muted = true;
            soundOffIcon.enabled = true;
            soundIcon.enabled = false;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            soundIcon.enabled = true;
            soundOffIcon.enabled=false;
            AudioListener.pause=false;
        }
    }

}
