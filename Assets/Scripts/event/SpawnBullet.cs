using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour 
{
	public FireController FireCont;
	public GameObject BullObj;

	// Use this for initialization
	void Start ()
	{
		FireCont = GameObject.Find("FireControllerObj").GetComponent<FireController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(FireCont.SpawnMG)
		{
			FireCont.SpawnMG=false;
			GameObject Shell=Instantiate(BullObj,transform.position,transform.rotation) as GameObject;
		}
	}
}
