using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Charecter_Mechanics : MonoBehaviour 
{
	public float MassBot,timeSound=30;
	int PowerEngine;
	int countN=0;
	int EngineSwitchSound=0;
	public float speedMove,speed=0,speedStep;
	public float Rotation=0.0f, tempRot;
	private Vector3 TargetVectorTower;
	private Vector3 TargetVectorGun;
	private float tempTowerX, tempTowerY, tempTowerZ;
	private float tempGunX, tempGunY, tempGunZ;
	int tempSound = 0;

	//параметры геймплея для персонажа
	private int[,,] armorPlane=new int[2,2,4];//[башня=0/корпус=1,верх=0/низ=1,корма=0/Пборт=1/нос=2/Лборт=3]
	private float gravityForce;
	public float gravity=20;
	private Vector3 moveVector;
	private float moveTemp;
	static public Vector3 MovePoint;
	public float SpeedRotatTow = 2f;
	public float SpeedRotatGun = 0.1f;

	//Ссылки на компоненты
	public  GameObject[] Sound;
	private CamTargetController CamTarget;
	public  GameObject towerCentr;
	public  GameObject gunCentr;
	private FireController FireCont;
	public  GameObject ShellObj;
	public  GameObject BullObj;
	public  GameObject spawnShellObj;
	public  GameObject spawnBulletObj;
	private CharacterController ch_controller;
	private MobilController mContr;
	private PhotonView photonView;//компанент для фотона

	private void Start()
	{
		SetSpeed();
		ch_controller = GetComponent<CharacterController>();
		mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobilController>();
		CamTarget = GameObject.Find("CentrCam").GetComponent<CamTargetController>();
		FireCont = GameObject.Find("FireControllerObj").GetComponent<FireController>();
		photonView = GetComponent<PhotonView>();
		speedStep =speedMove/50;
		moveTemp=0;
		speed=0;
		tempTowerX = CamTarget.Sent_X;
		tempTowerZ = CamTarget.Sent_Z;

		tempGunY = CamTarget.Sent_Y;
		tempGunZ = CamTarget.Sent_Z;

		SetSound(tempSound);
	}

	private void FixedUpdate ()
	{
		if (!photonView.IsMine) return;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		CharacterMove();
		GamingGravity();
		TargetTow();
		TargetGun();
		SoundVoid();
		Spawn();
	}
    //Метод перемещение объекта
    #region move objekt
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
				EngineSwitchSound=1;
			}
			else
			{
				EngineSwitchSound=0;
			}
			moveVector.x=mContr.Horizontal()*speedMove/2;
			if(mContr.Horizontal()!=0)
			{
				if (Rotation >  360) Rotation -= 360;
				if (Rotation < -360) Rotation += 360;
				if ( mContr.Vertical() < -0.05)
                {
					Rotation -= mContr.Horizontal() * speedMove / 20;
				}
                else 
				{
					Rotation += mContr.Horizontal() * speedMove / 20;
				}
				
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
		if(mContr.Vertical()>0.05|| mContr.Vertical()<-0.05)
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
	#endregion
	//Методы управления башней
	#region tower maneger
	void TargetTow()
	{
		if (tempTowerX < CamTarget.Sent_X) TargetVectorTower.x += 0.01f * Time.deltaTime * SpeedRotatTow * -1;
		if (tempTowerX > CamTarget.Sent_X) TargetVectorTower.x -= 0.01f * Time.deltaTime * SpeedRotatTow * -1;
		if (tempTowerZ < CamTarget.Sent_Z) TargetVectorTower.z += 0.01f * Time.deltaTime * SpeedRotatTow * -1;
		if (tempTowerZ > CamTarget.Sent_Z) TargetVectorTower.z -= 0.01f * Time.deltaTime * SpeedRotatTow * -1;
		Vector3 direct = Vector3.RotateTowards(transform.forward, TargetVectorTower, SpeedRotatTow, 0.0f);
		direct.x = CamTarget.TransformCam.x;
		direct.z= CamTarget.TransformCam.z;
		towerCentr.transform.rotation = Quaternion.LookRotation(direct);
	}
	#endregion
	//Методы управлением орудия
	#region gun menager
	void TargetGun()
	{
		if (tempGunY < CamTarget.Sent_Y) tempGunY += 0.01f * SpeedRotatGun;
		if (tempGunY > CamTarget.Sent_Y) tempGunY -= 0.01f * SpeedRotatGun;
		if (tempGunZ < CamTarget.Sent_Z) tempGunZ += 0.01f * SpeedRotatGun;
		if (tempGunZ > CamTarget.Sent_Z) tempGunZ -= 0.01f * SpeedRotatGun;

		TargetVectorGun.y = tempGunY * Time.deltaTime * SpeedRotatGun * -1;
		TargetVectorGun.z = tempGunZ * Time.deltaTime * SpeedRotatGun * -1;
		Vector3 direct = Vector3.RotateTowards(transform.forward, TargetVectorGun, SpeedRotatGun, 0.0f);
		
		direct = CamTarget.TransformCam*-1;
		if (direct.y < -0.2) direct.y = -0.15f;

		gunCentr.transform.rotation = Quaternion.LookRotation(direct);
	}
	#endregion
	//Методы управления звуком двигателя
	#region sound engine
	void SoundVoid()
    {
		if (tempSound != EngineSwitchSound) 
		{ 
			tempSound = EngineSwitchSound;
			SetSound(tempSound); 
		}
	}
	void ResetSound()
	{
		for (int i = 0; i < Sound.Length; i++)
		{ Sound[i].SetActive(false); }
	}
	void SetSound(int i)
	{
		ResetSound();
		if (i >= 1) Sound[1].SetActive(true);
		else Sound[0].SetActive(true);
	}
    #endregion
    //Методы управления стрельбой
    #region shot
	void Spawn()
    {
		if (FireCont.SpawnMG)
		{
			FireCont.SpawnMG = false;
			GameObject Shell = Instantiate(BullObj, spawnBulletObj.transform.position, spawnBulletObj.transform.rotation) as GameObject;
			Debug.Log("- - -");
		}
		if (FireCont.Spawn)
		{
			FireCont.Spawn = false;
			GameObject Shell = Instantiate(ShellObj, spawnShellObj.transform.position, spawnShellObj.transform.rotation) as GameObject;
			Debug.Log("+ + +");
		}
	}
    #endregion
}
