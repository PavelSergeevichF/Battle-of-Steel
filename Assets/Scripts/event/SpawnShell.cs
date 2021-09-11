using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShell : MonoBehaviour 
{
	public FireController FireCont;
	public GameObject ShellObj;

	// Use this for initialization
	void Start ()
	{
		FireCont = GameObject.Find("FireControllerObj").GetComponent<FireController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (FireCont.Spawn)
		{
			FireCont.Spawn=false;
			GameObject Shell=Instantiate(ShellObj,transform.position,transform.rotation) as GameObject;
		}
	}
}
