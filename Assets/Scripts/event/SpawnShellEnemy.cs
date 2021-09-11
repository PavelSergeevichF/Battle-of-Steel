using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShellEnemy : MonoBehaviour
{
   public FindTheEnemy Fire;
   public SoundFire[] Sound;
	public GameObject ShellObj;
    

	void Start ()
	{
		
	}
	
	void Update () 
	{
		if(Fire.SpawnG)
		{
			Fire.SpawnG=false;
            Sound[0].ShotPlay();
			GameObject Shell=Instantiate(ShellObj,transform.position,transform.rotation) as GameObject;
		}
	}
}
