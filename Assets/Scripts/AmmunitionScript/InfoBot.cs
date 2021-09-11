using UnityEngine;
using UnityEngine.UI;

public class InfoBot : MonoBehaviour 
{
	public Text TipTxet, MassTxet, PowerTxet, SpeedTxet, MachinGunSetTxet, MachinGunColibrTxet;
	public Text MachinGunLongTxet,MachinGunTempTxet, GunSetTxet, GunLongTxet, GunColibrTxet, GunTempTxet;
	public Text OPBullets, TPBullets, BPBullets, BZPBullets, BTPBullets, ZPBullets, BZTPBullets;
	public Text BBShells, PBShells, OFShells, OFTShells, KSShells, PSShells, SBShells;
	public Text FrontTowArm, BackTowArm, TopTowArm, DownTowArm, BoardTowArm;
	public Text FrontBodyArm, BackBodyArm, TopBodyArm, DownBodyArm, BoardBodyArm;
	int IdBot;
	int pow;
	double mass;
	EngineController EngineController;
	Menu_Ammunition MenuAmmunition;
	// Use this for initialization
	void Start () 
	{
		EngineController=GameObject.Find("EngineController").GetComponent<EngineController>();
		MenuAmmunition = GameObject.Find("Menu_AmmunitionObj").GetComponent<Menu_Ammunition>();
		IdBot=PlayerPrefs.GetInt("IdBot");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(MenuAmmunition.infoBot)setData();
	}
	void setData()
	{
		IdBot=PlayerPrefs.GetInt("IdBot");
		setTipBot();
		setPowerBot();
		setMassBot();
		setSpeedBot();
		setMachinGunBot();
		setBulletsBot();
		setGunBot();
		setShellsBot();
		setArmBot();
	}
	void setTipBot()
	{
		switch(PlayerPrefs.GetInt("IdBot"))
		{
			case 0:break;
			case 1:TipTxet.text="Легкая  бронемашина.";break;
			case 2:TipTxet.text="Тяжелая бронемашина.";break;
			case 3:TipTxet.text="Легкий  танк.";       break;
			case 4:TipTxet.text="Тяжелый танк.";       break;
		}
	}
	void setMassBot()
	{
		/*бтр 40 5,3 */
		/*бтр 80 13,6 */
		/*БРМ-3К «Рысь» 19 */
		MassTxet.text=PlayerPrefs.GetFloat("MasBot"+IdBot).ToString();
	}
	void setPowerBot()
	{
		pow=PlayerPrefs.GetInt("PowerEngineIdBot"+IdBot);
		PowerTxet.text=PlayerPrefs.GetInt("PowerEngineIdBot"+IdBot).ToString();
	}
	void setSpeedBot()
	{
		mass=PlayerPrefs.GetFloat("MasBot"+IdBot);
		pow=PlayerPrefs.GetInt("PowerEngineIdBot"+IdBot);
		int speed=(int)(pow/mass);
		if(speed>80)speed=80;
		SpeedTxet.text=speed.ToString();
	}
	void setMachinGunBot()
	{
		if( PlayerPrefs.GetInt("MachinGunIdBot"+IdBot)==1)
		{
			MachinGunSetTxet.text="Установлен";
			MachinGunColibrTxet.text=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+   IdBot).ToString();
			MachinGunLongTxet.text=PlayerPrefs.GetInt("LongMachinGunIdBot"+        IdBot).ToString();
			MachinGunTempTxet.text=PlayerPrefs.GetInt("rate_of_fireMachinGunIdBot"+IdBot).ToString();
		}
		else
		{
			MachinGunSetTxet.text="Не установлен";
			MachinGunColibrTxet.text=0.ToString();
			MachinGunLongTxet.text=0.ToString();
			MachinGunTempTxet.text=0.ToString();
		}
	}
	void setBulletsBot()
	{}
	void setGunBot()
	{
		if( PlayerPrefs.GetInt("GunIdBot"+IdBot)==1)
		{
			GunSetTxet.text="Установлена";
			GunColibrTxet.text=PlayerPrefs.GetInt("CaliberGunIdBot"+   IdBot).ToString();
			GunLongTxet.text=PlayerPrefs.GetInt("LongGunIdBot"+        IdBot).ToString();
			GunTempTxet.text=PlayerPrefs.GetInt("rate_of_fireGunIdBot"+IdBot).ToString();
		}
		else
		{
			GunSetTxet.text="Не установлена";
			GunColibrTxet.text=0.ToString();
			GunLongTxet.text=0.ToString();
			GunTempTxet.text=0.ToString();
		}
	}
	void setShellsBot()
	{}
	void setArmBot()
	{}
}
