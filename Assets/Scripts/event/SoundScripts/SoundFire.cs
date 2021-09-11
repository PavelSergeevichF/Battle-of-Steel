using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFire : MonoBehaviour {

	private AudioSource ShotSund;
	void Start () 
	{
		ShotSund=GetComponent<AudioSource>();
	}
	
	public void ShotPlay()
	{
		ShotSund.Play();
	}
}
