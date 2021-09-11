using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public Slider 
                 Slider_caliber,
                 Slider_long,
                 Slider_rate_of_fire;
    double PriceG=0,PriceS=0,PriceC=0,PriceGt=0,PriceSt=0,PriceCt=0;
    [SerializeField]
    float caliber;
    [SerializeField]
    int ID, IDtemp=0,
        long_G_MG,
        rate_of_fire,
        caliberTemp,
        longWeapTemp,
        rate_of_fireTemp;
        [SerializeField]
    bool GunSet=false,
         MGunSet=false,
         Activ_Type_Weapon=false,
         Activ_Type_Weapontemp=true,
         automatic_loader=false,
         inmenuGC=false,
         GetCalibr=true,
         startScript=false;
    public Text WeaponTyp,
                SetReset,
                caliber_text,
                Long_text,
                //rate_of_fire_text,
                Speed_Sell,
                Speed_Shot,
                massWeapon,
                PriceTxtGold,PriceTxtSilver,PriceTxtCupper;
    information_menu InfoMenu;
    SetDataBot setDataBot;
    Menu_Ammunition MenuAmmunition;
    ExchangeNet exchangeNet;
    public ClickSound clickSound;
    void Start()
    {
        startScript=false;
		clickSound=GameObject.Find("ClickSoundObj").GetComponent<ClickSound>();
        InfoMenu=GameObject.Find("information_menuObj").GetComponent<information_menu>();
        setDataBot= GameObject.Find("SetDataBotObj").GetComponent<SetDataBot>();
		MenuAmmunition = GameObject.Find("Menu_AmmunitionObj").GetComponent<Menu_Ammunition>();
		exchangeNet=GameObject.Find("ExchangeNetOBJ").GetComponent<ExchangeNet>();
        ID=PlayerPrefs.GetInt("IdBot");
        GetWeapon();
        BoottonMachingun();
        GetCalibr=true;
        startScript=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuAmmunition.panelGun)
        {
            ID=PlayerPrefs.GetInt("IdBot");
            inmenuGC=true;
            WeaponData();
            check_can_buy();
            if(GetCalibr){SetSlider();}
        }
        else {inmenuGC=false;GetCalibr=true;}
    }
    void check_can_buy()
    {
        if(exchangeNet.Gold<PriceG){setDataBot.SetActiveGoldErr();MenuAmmunition.apply=false;}
			else
			{
				setDataBot.ReSetActiveGoldErr();
				if(exchangeNet.Silver<PriceS){setDataBot.SetActiveSilverErr();MenuAmmunition.apply=false;}
				else
				{
					setDataBot.ReSetActiveSilverErr();
					if(exchangeNet.Copper<PriceC){setDataBot.SetActiveCopperErr();MenuAmmunition.apply=false;}
					else
					{
						setDataBot.ReSetActiveCopperErr();
						if(MenuAmmunition.apply&&MenuAmmunition.panelGun)
						{
			     	        MenuAmmunition.apply=false;
                            if(!Activ_Type_Weapon)//пулимет
                            {
                                if(
                                    PlayerPrefs.GetInt("CaliberMachinGunIdBot"+     PlayerPrefs.GetInt("IdBot"))==caliberTemp  &&
                                    PlayerPrefs.GetInt("LongMachinGunIdBot"+        PlayerPrefs.GetInt("IdBot"))==longWeapTemp &&
                                    PlayerPrefs.GetInt("rate_of_fireMachinGunIdBot"+PlayerPrefs.GetInt("IdBot"))==rate_of_fireTemp
                                  )
							    {
								    InfoMenu.ShowInformation_WeaponErr_double_buy();
							    }
							    else
							    {
								    exchangeNet.moneySet(PriceG,PriceS,PriceC,0);
                                    float tempMass=0.004f*caliberTemp*longWeapTemp/1000;
                                    PlayerPrefs.SetFloat("MassMGun"+ID,tempMass);
							        exchangeNet.GetDate(); 
			      	                PlayerPrefs.SetInt("CaliberMachinGunIdBot"+     ID,caliberTemp );
                                    PlayerPrefs.SetInt("LongMachinGunIdBot"+        ID,longWeapTemp);
                                    PlayerPrefs.SetInt("rate_of_fireMachinGunIdBot"+ID,rate_of_fireTemp);
                                    PlayerPrefs.SetInt("MachinGunIdBot"+            ID,PlayerPrefs.GetInt("WeaponMG"+ID));
							    }
                            }
                            else//пушка
                            {
                                if(
                                    PlayerPrefs.GetInt("CaliberGunIdBot"+     ID)==caliberTemp  &&
                                    PlayerPrefs.GetInt("LongGunIdBot"+        ID)==longWeapTemp &&
                                    PlayerPrefs.GetInt("rate_of_fireGunIdBot"+ID)==rate_of_fireTemp
                                  )
							    {
								    InfoMenu.ShowInformation_WeaponErr_double_buy();
							    }
							    else
							    {
								    exchangeNet.moneySet(PriceG,PriceS,PriceC,0);
                                    float tempMass=0.004f*caliberTemp*longWeapTemp/1000;
								    PlayerPrefs.SetFloat("MassGun"+ID,tempMass);
							        exchangeNet.GetDate(); 
			      	                PlayerPrefs.SetInt("CaliberGunIdBot"+     ID,caliberTemp );
                                    PlayerPrefs.SetInt("LongGunIdBot"+        ID,longWeapTemp);
                                    PlayerPrefs.SetInt("rate_of_fireGunIdBot"+ID,rate_of_fireTemp);
                                    PlayerPrefs.SetInt("GunIdBot"+            ID,PlayerPrefs.GetInt("WeaponG"+ID));
							    }
                            }
		                }
					}
				}
			}
    }
    public void BoottonGun()
    {
        if(startScript)clickSound.ClickPlay();
        Activ_Type_Weapon=true;
        WeaponTyp.text="Пушка";
        PrintTypWeapon();
        SetSlider();
    }
    public void BoottonMachingun()
    {
        if(startScript)clickSound.ClickPlay();
        Activ_Type_Weapon=false;
        WeaponTyp.text="Пулемет";
        PrintTypWeapon();
        SetSlider();
    }
    public void AddWeapon()
    {
        if(startScript)clickSound.ClickPlay();
        if(!Activ_Type_Weapon)
        {
            if(ID==1&&PlayerPrefs.GetInt("WeaponG"+ID)==1){InfoMenu.ShowInformation_many_WeaponErr();}
            else
            {
                MGunSet=true;
                PlayerPrefs.SetInt("WeaponMG"+ID,1);
            }
            PrintTypWeapon();
        }
        else 
        {
            if(ID==1&&PlayerPrefs.GetInt("WeaponMG"+ID)==1){InfoMenu.ShowInformation_many_WeaponErr();}
            else
            {
                GunSet=true;
                PlayerPrefs.SetInt("WeaponG"+ID,1);
            }
            PrintTypWeapon();
        }
    }
    public void RemovWeapon()
    {
        if(startScript)clickSound.ClickPlay();
        if(!Activ_Type_Weapon)
        {
            if(PlayerPrefs.GetInt("WeaponG"+ID)==0 && ID!=1)
            { InfoMenu.ShowInformationWeaponErr();}
            else
            {
                MGunSet=false;
                PlayerPrefs.SetInt("WeaponMG"+ID,0);
            }
            PrintTypWeapon();
        }
        else 
        {
            if(PlayerPrefs.GetInt("WeaponMG"+ID)==0 && ID!=1)
            { InfoMenu.ShowInformationWeaponErr();}
            else
            {
                GunSet=false;
                PlayerPrefs.SetInt("WeaponG"+ID,0);
            }
            PrintTypWeapon();
        }
    }
    void PrintTypWeapon()
    {
        if(!Activ_Type_Weapon)
        {
            if(MGunSet) 
            {
                SetReset.text="установлен";
            }
            else SetReset.text="не установлен";
        }
        else
        {
            if(GunSet) 
            {
                SetReset.text="установлена";
            }
            else SetReset.text="не установлена";
        }
    }
    void GetWeapon()
    {
        if(PlayerPrefs.GetInt("WeaponMG"+ID)==1) MGunSet=true;
        else MGunSet=false; 
        if(PlayerPrefs.GetInt("WeaponG"+ID)==1) GunSet=true;
        else GunSet=false;
        PrintTypWeapon();
    }
    public void automatic_loader_trigger()
    {

    }
    int rate_of_fireTempData()
    {
        int temp=0;
        if(!Activ_Type_Weapon)//пулемет
        {
            Slider_rate_of_fire.minValue=10;Slider_rate_of_fire.maxValue=1000;
            temp=(int)Slider_rate_of_fire.value*10;
        }
        else//пушка
        {
            if(caliberTemp<25)
            {
                Slider_rate_of_fire.minValue=10;
                Slider_rate_of_fire.maxValue=1000; 
                temp=(int)Slider_rate_of_fire.value;
            }
            if(caliberTemp>24 &&caliberTemp<80 )
            {
                Slider_rate_of_fire.minValue=1;
                Slider_rate_of_fire.maxValue=20;
                temp=(int)Slider_rate_of_fire.value;
            }
            if(caliberTemp>79 &&caliberTemp<110)
            {
                Slider_rate_of_fire.minValue=1;
                Slider_rate_of_fire.maxValue=10;
                temp=(int)Slider_rate_of_fire.value;
            }
            if(caliberTemp>109&&caliberTemp<160)
            {
                Slider_rate_of_fire.minValue=1;
                Slider_rate_of_fire.maxValue=3;
                temp=(int)(Slider_rate_of_fire.value);
            }
            if(caliberTemp>159                 )
            {
                Slider_rate_of_fire.minValue=1;
                Slider_rate_of_fire.maxValue=2;
                temp=(int)(Slider_rate_of_fire.value);
            }
        }
        return temp;
    }
    void WeaponData()
    {
        float tempMass=0;
        Slider_long.minValue=200;
        if(!Activ_Type_Weapon)//пулимет
        {
            if(Activ_Type_Weapon!=Activ_Type_Weapontemp)
            {
                Activ_Type_Weapontemp=Activ_Type_Weapon;
                Slider_caliber.value=50;
                Slider_long.value=200; 
            }
            Slider_caliber.minValue=50;
            Slider_caliber.maxValue=200;
            Slider_long.maxValue   =1200;
            caliberTemp=(int)(Slider_caliber.value*0.1);
        }
        else
        {
            Slider_caliber.minValue=21;
            if(Activ_Type_Weapon!=Activ_Type_Weapontemp)
            {
                Activ_Type_Weapontemp=Activ_Type_Weapon;
                Slider_caliber.value=15;
                Slider_long.value=200; 
            }
            switch(ID)
		        {
			    	case 0: break;
			    	case 1: Slider_caliber.maxValue=45;  Slider_long.maxValue=800; break;
		    		case 2: Slider_caliber.maxValue=130; Slider_long.maxValue=1800; break;
		    		case 3: Slider_caliber.maxValue=160; Slider_long.maxValue=4000; break;
		    		case 4: Slider_caliber.maxValue=260; Slider_long.maxValue=5000; break;
		    	}
            caliberTemp=(int)Slider_caliber.value;
        }
        int maxCalibr   =(int)Slider_caliber.     maxValue;
        int maxLong     =(int)Slider_long.        maxValue;
        int maxROF      =(int)Slider_rate_of_fire.maxValue;
        longWeapTemp    =(int)Slider_long.           value;
        rate_of_fireTemp  =rate_of_fireTempData() ;
        tempMass=0.004f*caliberTemp*longWeapTemp;
        massWeapon.text       =tempMass.ToString()    ;
        caliber_text.text     =caliberTemp.ToString() ;
        Long_text.text        =longWeapTemp.ToString();
        //rate_of_fire_text.text=rate_of_fireTemp.ToString();
        Speed_Shot.text       =rate_of_fireTemp.ToString();
        SetPrice(caliberTemp, longWeapTemp, rate_of_fireTemp, maxCalibr, maxLong,maxROF);
        PriceC=PriceCt;PriceS=PriceSt;PriceG=PriceGt;
        PriceTxtGold.text  =PriceG.ToString();
        PriceTxtSilver.text=PriceS.ToString();
        PriceTxtCupper.text=PriceC.ToString();
        PriceCt=0;PriceSt=0;PriceGt=0;
    }
    void SetPrice(int val_Slider_C, int val_Slider_L, int val_Slider_ROF,int max_Slider_C, int max_Slider_L, int max_Slider_ROF)//________функция установки цены
	{
		int importance=100;
		double Vol_C=importance*val_Slider_C/max_Slider_C;
        double Vol_L=importance*val_Slider_L/max_Slider_L;
        double Vol_ROF=importance*val_Slider_ROF/max_Slider_ROF;
		if(Vol_C<50              ){SetMachPrice(0,0,val_Slider_C);}
		if(Vol_C>=50&&Vol_C<=75  ){SetMachPrice(1,0,val_Slider_C);}
		if(Vol_C>75              ){SetMachPrice(1,1,val_Slider_C);}
        if(Vol_L<50              ){SetMachPrice(0,0,val_Slider_L/10);}
		if(Vol_L>=50&&Vol_C<=75  ){SetMachPrice(1,0,val_Slider_L/10);}
		if(Vol_L>75              ){SetMachPrice(1,1,val_Slider_L/10);}
        if(Vol_ROF<50            ){SetMachPrice(0,0,(int)Vol_ROF);}
		if(Vol_ROF>=50&&Vol_C<=75){SetMachPrice(1,0,(int)Vol_ROF);}
		if(Vol_ROF>75            ){SetMachPrice(1,1,(int)Vol_ROF);}
	}
    void SetSlider()
    {
        GetCalibr=false;
        if(Activ_Type_Weapon)//пушка
        {
            if(PlayerPrefs.GetInt("CaliberGunIdBot"+     ID)<Slider_caliber.minValue     ){Slider_caliber.value     =Slider_caliber.minValue;}          //установка калибра
            if(PlayerPrefs.GetInt("CaliberGunIdBot"+     ID)>Slider_caliber.maxValue     ){Slider_caliber.value     =Slider_caliber.maxValue;}          //установка калибра
            else {Slider_caliber.value=PlayerPrefs.GetInt("CaliberGunIdBot"+ID);}
            if(PlayerPrefs.GetInt("LongGunIdBot"+        ID)<Slider_long.minValue        ){Slider_long.value        =Slider_long.minValue;}             //установка длинны
            if(PlayerPrefs.GetInt("LongGunIdBot"+        ID)>Slider_long.maxValue        ){Slider_long.value        =Slider_long.maxValue;}             //установка длинны
            else {Slider_long.value =PlayerPrefs.GetInt("LongGunIdBot"+ID);}
            if(PlayerPrefs.GetInt("rate_of_fireGunIdBot"+ID)<Slider_rate_of_fire.minValue){Slider_rate_of_fire.value=Slider_rate_of_fire.minValue;}     //установка скорострельности
            if(PlayerPrefs.GetInt("rate_of_fireGunIdBot"+ID)<Slider_rate_of_fire.maxValue){Slider_rate_of_fire.value=Slider_rate_of_fire.maxValue;}     //установка скорострельности
            else {Slider_rate_of_fire.value =PlayerPrefs.GetInt("rate_of_fireGunIdBot"+ID );}
        }
        else//пулимет
        {
            if(PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID     )<Slider_caliber.minValue*0.1 ){Slider_caliber.value     =Slider_caliber.minValue;}     //установка калибра
            if(PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID     )>Slider_caliber.maxValue*0.1 ){Slider_caliber.value     =Slider_caliber.maxValue;}     //установка калибра
            else {Slider_caliber.value=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID)*10;}
            if(PlayerPrefs.GetInt("LongMachinGunIdBot"+ID        )<Slider_long.minValue        ){Slider_long.value        =Slider_long.minValue;}        //установка длинны
            if(PlayerPrefs.GetInt("LongMachinGunIdBot"+ID        )>Slider_long.maxValue        ){Slider_long.value        =Slider_long.maxValue;}        //установка длинны
            else {Slider_long.value =PlayerPrefs.GetInt("LongMachinGunIdBot"+ID);}
            if(PlayerPrefs.GetInt("rate_of_fireMachinGunIdBot"+ID)<Slider_rate_of_fire.minValue){Slider_rate_of_fire.value=Slider_rate_of_fire.minValue;}//установка скорострельности
            if(PlayerPrefs.GetInt("rate_of_fireMachinGunIdBot"+ID)<Slider_rate_of_fire.maxValue){Slider_rate_of_fire.value=Slider_rate_of_fire.maxValue;}//установка скорострельности
            else {Slider_rate_of_fire.value =PlayerPrefs.GetInt("rate_of_fireMachinGunIdBot"+ID );}
        }
    }
	void SetMachPrice(int resS,int resG, int Vol)
	{
		//---------------------------------------------медь
			PriceCt+=Vol*10;
			//---------------------------------------------серебро
			PriceSt+=Vol*2*resS/5;
			//---------------------------------------------Золото
			PriceGt+=Vol*2*resG/50;
	}
    //************************************************************************************работа
    //************************************************************************************
}