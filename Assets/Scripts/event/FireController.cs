using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour //, ISentDataWeapon
{
	private int SpeedFireGun=0,
	            SpeedFireMGun=0,
				booletShot=1;
    [Header("данные")]
	public int caliber,
	           num=0,
	           MGBullet=1000;
			   //FrameNumG=0,FrameNumMG=0;
	int caliberTyp,ID;
	public Image ImageLoadGun;
	public Image ImageLoadMGun;
	public float SpeedLoadGun=0.01f,
	            SpeedMGLoadGun=0.3f;
	private float MGProgress=0,
	              Progress=1.1f,
				  FullGun,
				  FrameInMin=1800,
				  FrameOnСharging=0;
	private bool Fire=false,
 	            RedyFireMG=true,
 	            FireMG=false;
	public bool RedyFire=true,
	            Spawn=false,
	            SpawnMG=false;
	public SoundFire[] SoundFireType;
	void Start()
	{
		Progress=1.1f;
		ID=PlayerPrefs.GetInt("IdBot");
		SpeedFireGun=PlayerPrefs.GetInt("rate_of_fireGunIdBot"+ID);
		SpeedFireMGun=PlayerPrefs.GetInt("rate_of_fireMachinGunIdBot"+ID);
		caliber=PlayerPrefs.GetInt("CaliberGunIdBot"+ID);
		machGunTime();
		machMGunTime();
		SetTypCaliber();
	}
	
	void Update () 
	{
		if(Progress<=1)
		{
			loadGun ();
		}
		else RedyFire=true;
		if(MGProgress<=1)
		{
			loadMGun ();
		}
		else RedyFireMG=true;
	}

    public void Shoot()
    {
		if(RedyFire)
		{
			RedyFire=false;
			Fire=false;
			Spawn=true;
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//bool ISentDataWeapon.SpawnG { set => Spawn; }//ругается не существует в текущем контексте
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			SoundShot();
			Progress=0f;
		}
    }
	public void ShootMachinGun()
	{
		if(RedyFireMG)
		{
			num++;
			RedyFireMG=false;
			SpawnMG=true;
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//bool ISentDataWeapon.SpawnMG { set => Spawn; }//ругается не существует в текущем контексте
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			SoundFireType[0].ShotPlay();
			booletShot=1;
			MGBullet -=booletShot;
			MGProgress=0f;
			
		}
	}
	void loadGun ()
	{
		Progress +=SpeedLoadGun;
		ImageLoadGun.fillAmount = Progress;
	}
	void loadMGun ()
	{
		
		MGProgress +=(float)SpeedMGLoadGun;
		FullGun=MGBullet/10;
		ImageLoadMGun.fillAmount=FullGun/100;
	}

	void SetTypCaliber()
	{
		if(caliber<36              )caliberTyp=1;
		if(caliber>35 &&caliber<90 )caliberTyp=2;
		if(caliber>89 &&caliber<130)caliberTyp=3;
		if(caliber>125&&caliber<210)caliberTyp=4;
		if(caliber>209&&caliber<250)caliberTyp=5;
		if(caliber>249             )caliberTyp=6;
	}
	void SoundShot()
	{
		switch(caliberTyp)
		{
			case 1:
			    SoundFireType[0].ShotPlay();
			    break;
			case 2:
			    SoundFireType[1].ShotPlay();
			    break;
			case 3:
			    SoundFireType[2].ShotPlay();
			    break;
			case 4:
			    SoundFireType[3].ShotPlay();
			    break;
			case 5:
			    SoundFireType[4].ShotPlay();
			    break;
			case 6:
			    SoundFireType[5].ShotPlay();
			    break;
		}
	}
	void machGunTime()
	{
		int FrameNumG=0;
		float tg=FrameInMin/SpeedFireGun;
		FrameNumG=(int)tg;
		float t=1.0f;
		if(FrameNumG<1)FrameNumG=1;
		SpeedLoadGun=t/FrameNumG;
	}
	void machMGunTime()
	{
		int FrameNumMG=0;
		float tmg=FrameInMin/SpeedFireMGun;
		if(tmg>1)
		{
			tmg=1.0f;
			float b=SpeedFireMGun/FrameInMin;
			booletShot=(int)b;
		}
		FrameNumMG=(int)tmg;
		float t=1.0f;
		SpeedMGLoadGun=t/FrameNumMG;
	}
}
