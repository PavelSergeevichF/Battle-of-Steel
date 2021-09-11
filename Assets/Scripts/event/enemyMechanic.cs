using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class enemyMechanic : MonoBehaviour 
{
	public float MassBot,timeSound=30;
	int PowerEngine;
	int countN=0;
	public int EngineSwitchSound=0;
	public float speedMove,speed=0,speedStep;
	public float Rotation=0.0f, tempRot;
	private float tempX, tempY, tempZ;

    //параметры геймплея для персонажа
	private float gravityForce;
	public float gravity=20;
	private Vector3 moveVector;
	private float moveTemp;
	static public Vector3 MovePoint;

	//Ссылки на компоненты
	private CharacterController ch_controller;
	private MobilController mContr;
	private PhotonView photonView;//компанент для фотона

	private void Start()
	{
		SetSpeed();
		ch_controller = GetComponent<CharacterController>();
		mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobilController>();
		photonView = GetComponent<PhotonView>();
		speedStep =speedMove/50;
		moveTemp=0;
		speed=0;
	}

	private void FixedUpdate ()
	{
		if (!photonView.IsMine) return;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		CharacterMove();
		GamingGravity();
	}
	//Метод перемещение объекта
	private void CharacterMove()
	{
		//перемещение по поверхности
		 if (ch_controller.isGrounded)
		{
		    moveVector=Vector3.zero;
			float tempF=mContr.Vertical()*speed;
			float tmp;
			if(tempF==0)tmp=0.001f;
			else tmp=tempF;
			if(mContr.Vertical()>0||mContr.Vertical()<0)
			{
				EngineSwitchSound=4;
			}
			else
			{
				EngineSwitchSound=0;
			}
			moveVector.x=mContr.Horizontal()*speedMove/2;
			if(mContr.Horizontal()!=0)
			{
				if(Rotation>360)Rotation-=360;
				Rotation+=mContr.Horizontal()*speedMove/20;
			}
			if(tempRot!=Rotation)
			{
				tempRot=Rotation;
			}
            if(Vector3.Angle(Vector3.forward,moveVector)>1f||Vector3.Angle(Vector3.forward,moveVector)==0)
		    {
				transform.rotation =Quaternion.Euler(0,Rotation, 0);
			}
		}
		moveVector=transform.forward*mContr.Vertical();//*-1;
        moveVector.y = gravityForce;
		ch_controller.Move(moveVector * Time.deltaTime*speedMove/1.5f);//метод передвижения по направлению
		MovePoint=transform.position;//передача данных камере
	}

	//метод гравитации
	private void GamingGravity()
	{
		if(!ch_controller.isGrounded) 
		{
			gravityForce -=gravity*Time.deltaTime;
		}
        else 
		{
			gravityForce = -1f;
		}
	}
	void SetSpeed()
	{
		
		MassBot=PlayerPrefs.GetFloat("Mass");
		PowerEngine=PlayerPrefs.GetInt("PowerEngine");
		speedMove=(int)(PowerEngine/MassBot);
		if(speedMove>80)speedMove=80;
		if(speedMove<1)speedMove=1;
	}
}
