using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource ClickSund;
        void Start()
    {
        ClickSund=GameObject.Find("ClickSoundOb").GetComponent<AudioSource>();
    }
    public void ClickPlay()
	{
        //AudioElevator.Play();
		ClickSund.Play();
	}
}
