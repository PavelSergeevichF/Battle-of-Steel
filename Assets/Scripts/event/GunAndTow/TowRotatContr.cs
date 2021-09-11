using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TowRotatContr : MonoBehaviour
{
	private Vector3 TargetVector;
	private CamTargetController CamTarget;
	public float SpeedRotatTow = 5f;
	float  tempX, tempY, tempZ;
	private PhotonView photonView;
	// Use this for initialization
	void Start () 
	{
		CamTarget=GameObject.Find("CentrCam").GetComponent<CamTargetController>();
		photonView = GetComponent<PhotonView>();
		tempX =CamTarget.Sent_X;
		tempZ=CamTarget.Sent_Z;
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		TargetTow();
	}
	void TargetTow()
	{
        if(tempX<CamTarget.Sent_X) tempX+=0.01f*SpeedRotatTow;
		if(tempX>CamTarget.Sent_X) tempX-=0.01f*SpeedRotatTow;
		if(tempZ<CamTarget.Sent_Z) tempZ+=0.01f*SpeedRotatTow;
		if(tempZ>CamTarget.Sent_Z) tempZ-=0.01f*SpeedRotatTow;
		TargetVector.x=tempX*Time.deltaTime*SpeedRotatTow;
		TargetVector.z=tempZ*Time.deltaTime*SpeedRotatTow;
		Vector3 direct=Vector3.RotateTowards(transform.forward, TargetVector, SpeedRotatTow, 0.0f);
		transform.rotation = Quaternion.LookRotation(direct);	
	}
}
