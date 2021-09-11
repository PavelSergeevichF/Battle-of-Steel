using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour 
{
    public bool Enter;
	private int SetI;
	public GameObject col;
    [SerializeField]
    private int i=40; 
	void Start () 
	{
		Enter=false;
		SetI=i;
	}
	void Update ()
	{
		if(Enter)
		{
			i--;
			if(i<1) {Enter=false; i=SetI;}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if(col)Enter=true;
	}
}
