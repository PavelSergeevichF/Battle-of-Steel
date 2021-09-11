using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music_play : MonoBehaviour
{
    public AudioSource[] track;
    public int N_Track=0;
    int track_n=0;
    public Toggle Musick_Togget;
    public Toggle Sound_Togget;
    public Slider Musick_Slider;
    public Slider Sound_Slider;
    public Text Musick_String;
    public Text Sound_String;
    public Menu_main menu_main;
    bool Check_Play=false,
         check_OpenSetting=false;
    void Start()
    {
        if(PlayerPrefs.GetInt("TrackN")<0){track_n=0;track[track_n].Play();}
        else 
        {
            N_Track=track_n=PlayerPrefs.GetInt("TrackN");
            track[track_n].Play();
        }
        if(PlayerPrefs.GetInt("MusicVol")<10)PlayerPrefs.SetInt("MusicVol",10);
        else 
        {
            Musick_Slider.value=PlayerPrefs.GetInt("MusicVol")/100;
            AudioListener.volume=Sound_Slider.value;
        }
        if(PlayerPrefs.GetInt("SoundVol")<10)PlayerPrefs.SetInt("SoundVol",10);
        else 
        {
            Sound_Slider.value=PlayerPrefs.GetInt("SoundVol")/100;
            //AudioListener.volume=Sound_Slider.value;
        }
    }
    void Update()
    {
        if(menu_main.Aktiv_setting)
        {
            if(!Musick_Togget.isOn)StopTrack();
            else 
            {
                if(!Check_Play)
                {
                    Check_Play=true;
                    track[track_n].Play();
                }
            }
            GetDataSliderMusic();
        }
        else {if(check_OpenSetting)check_OpenSetting=false;}
    }
    void GetDataSliderMusic()
    {
        if(check_OpenSetting)
        {
            //******************************************искать косяк тут.
            check_OpenSetting=false;
            int ii=PlayerPrefs.GetInt("MusicVol");
            float ff=ii;
            Musick_Slider.value=ff/100;
        };
        int ji=PlayerPrefs.GetInt("MusicVol");
        float t_f=Musick_Slider.value*100;
        int t_i=(int)t_f;
        Musick_String.text=t_i.ToString();
        if(Musick_Slider.value!=ji/100)
        {PlayerPrefs.SetInt("MusicVol",t_i);}
        AudioListener.volume=Musick_Slider.value;
    }
    void GetDataSliderSound()
    {
        float t_f=Sound_Slider.value*100;
        int t_i=(int)t_f;
        Sound_String.text=t_i.ToString();
        PlayerPrefs.SetInt("SoundVol",t_i);
        AudioListener.volume=Sound_Slider.value;
    }
    void StopTrack()
    {
        Check_Play=false;
        for(int i=0;i<track.Length;i++)
        {track[i].Stop();}
    }
    void SelecktTrack()
    {
        StopTrack();
        track[track_n].Play();
        N_Track=track_n;
        PlayerPrefs.SetInt("TrackN",track_n);
    }
    public void NextTrack()
    {
        if(track.Length>0)
        {
            if(track_n>=track.Length-1)
            {
                track_n=0;
                SelecktTrack();
            }
            else
            {
                track_n++;
                SelecktTrack();
            }
        }
    }
    public void BackTrack()
    {
        if(track.Length>0)
        {
            if(track_n<=0)
            {
                track_n=track.Length-1;
                SelecktTrack();
            }
            else
            {
                track_n--;
                SelecktTrack();
            }
        }
    }
}
