using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOBJ : MonoBehaviour 
{
	public int timeLive;
	public float SpeedObj;
	public Transform targetPosition;
	void Start()
	{
		Destroy(gameObject,timeLive);
	}
	void Update () 
	{
		transform.position+=transform.forward* Time.deltaTime*SpeedObj;
		//transform.position=Vector3.MoveTowards(transform.position,targetPosition.position,SpeedObj);//*Time.deltaTime*10
	}
}
