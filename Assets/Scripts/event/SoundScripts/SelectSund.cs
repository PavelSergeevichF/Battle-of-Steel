using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSund : MonoBehaviour
{
    public GameObject[] Sound;
    public Charecter_Mechanics charecter_Mechanics;
    int temp=0;

    // Start is called before the first frame update
    void Start()
    {
        SetSound(temp);
    }

    // Update is called once per frame
    void Update()
    {
        //if(temp!= charecter_Mechanics.EngineSwitchSound){temp= charecter_Mechanics.EngineSwitchSound;SetSound(temp);}

    }
    void ResetSound()
    {
        for(int i=0;i<Sound.Length;i++)
        {Sound[i].SetActive(false);}
    }
    void SetSound(int i)
    {
        ResetSound();
        if(i>=1) Sound[1].SetActive(true);
        else Sound[0].SetActive(true);
    }
}
