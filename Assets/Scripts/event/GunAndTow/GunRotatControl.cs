using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotatControl : MonoBehaviour 
{
	private Vector3 TargetVector;
	private CamTargetController CamTarget;
	public float SpeedRotatGun = 0.1f;
	float  tempY, tempZ;
	// Use this for initialization
	void Start () 
	{
		CamTarget=GameObject.Find("CentrCam").GetComponent<CamTargetController>();
        tempY = CamTarget.Sent_Y;
		tempZ = CamTarget.Sent_Z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		TargetGun();
	}
	void TargetGun()
	{
        if (tempY < CamTarget.Sent_Y) tempY += 0.01f * SpeedRotatGun;
		if (tempY > CamTarget.Sent_Y) tempY -= 0.01f * SpeedRotatGun;
		if (tempZ < CamTarget.Sent_Z) tempZ += 0.01f * SpeedRotatGun;
		if (tempZ > CamTarget.Sent_Z) tempZ -= 0.01f * SpeedRotatGun;

		TargetVector.y = tempY * Time.deltaTime * SpeedRotatGun * -1;
		TargetVector.z = tempZ * Time.deltaTime * SpeedRotatGun * -1;
		Vector3 direct=Vector3.RotateTowards(transform.forward, TargetVector, SpeedRotatGun, 0.0f);
		Debug.Log("TargetVector:"+TargetVector+"_direct"+direct);
		transform.rotation = Quaternion.LookRotation(direct);	
	}
}
