using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    private AudioSource Sund;
	void Start () 
	{
		Sund=GetComponent<AudioSource>();
	}
	
	public void Play()
	{
		Sund.Play();
	}
}