using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletEnemy : MonoBehaviour
{
    
	public FindTheEnemy Fire;
    public SoundFire[] Sound;
	public GameObject BullObj;
    int t=0;

	void Start ()
	{
	}
	
	void Update () 
	{
		if(Fire.SpawnMG&&t==0)
		{
            t=3;
			Fire.SpawnMG=false;
            Sound[0].ShotPlay();
			GameObject Shell=Instantiate(BullObj,transform.position,transform.rotation) as GameObject;
		}
        if(t>0)t--;
        else t=0;
	}
}
