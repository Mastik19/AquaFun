using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{


    

    public AudioMixer mixer;

    public Slider master;
    public Slider music;
    public Slider sfx;

    public TMP_Dropdown drop; 


    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("volume"));
        }

        else
        {
            mixer.SetFloat("MasterVol", 0);
        }
    }



    public void ChangeGraphicsQuality()
    {

        QualitySettings.SetQualityLevel(drop.value);

    }










    public void ChangeMasterVol()
    {

        mixer.SetFloat("MasterVol", master.value);

        PlayerPrefs.SetFloat("volume", master.value);
    }

    public void ChangeMusicVol()
    {

        mixer.SetFloat("MusicVol", music.value);

        PlayerPrefs.SetFloat("volume", music.value);
    }


    public void ChangeSFXVol()
    {

        mixer.SetFloat("SFXVol", sfx.value);

        PlayerPrefs.SetFloat("volume", sfx.value);
    }
}
