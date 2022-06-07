using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume",1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = Slider.value;
        Save();
    }

    private void Load()
    {
        Slider.value = PlayerPrefs.GetFloat("musicvolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", Slider.value);
    }

}