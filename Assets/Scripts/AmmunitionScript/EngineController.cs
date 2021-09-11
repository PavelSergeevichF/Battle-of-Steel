using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineController : MonoBehaviour {

	public int PowerData, PowerDataTemp;
	public int MassEngine=0;
	public int ID,tempID;
	public double DataMass;
	double PriceG=0,PriceS=0,PriceC=0;
	double PriceRepairG=0,PriceRepairS=0,PriceRepairC=0;
	bool ChecSelekt=true;
	public Slider Slider_Power;
	public Text Power_ls_Txt, Mass_Data, Power_Data;
	public Text PriceTxtGold,PriceTxtSilver,PriceTxtCupper;
	public Text PriceRepairTxtGold,PriceRepairTxtSilver,PriceRepairTxtCupper;
	public Menu_Ammunition MenuAmmunition;
	public SetDataBot setDataBot;
	public ExchangeNet exchangeNet;
	public information_menu InfoMenu;
	void Start () 
	{
		Slider_Power.value=60;
		ID=tempID=PlayerPrefs.GetInt("IdBot");
	}

	void Update ()
	{
		PowerData=(int)Slider_Power.value;
		if(MenuAmmunition.panelEngine)
		{
			ID=PlayerPrefs.GetInt("IdBot");
			UpDataEngin();
		    SetSlider();
		}
	}
	void UpDataEngin()
	{
		
		if(PowerData!=PowerDataTemp)
		{
		if(PowerData<600) {DataMass=PowerData*1.5/1000;}
		if(PowerData>600 && PowerData<1500) {DataMass=PowerData*1.1/1000;}
		if(PowerData>1500) {DataMass=PowerData*0.8/1000;}

		SetPrice();
		SetDataEngin();
		PowerDataTemp=PowerData;
		}
		if(MenuAmmunition.panelEngine)
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
						if(MenuAmmunition.apply&&MenuAmmunition.PanelEngine)
						{
			     	        MenuAmmunition.apply=false;
							if(PlayerPrefs.GetInt("PowerEngineIdBot"+PlayerPrefs.GetInt("IdBot"))==PowerData)
							{
								InfoMenu.ShowInformationEnginSmollErr();
							}
							else
							{
								ChecSelekt=true;
								exchangeNet.moneySet(PriceG,PriceS,PriceC,0);
								PlayerPrefs.SetFloat("MassEngine"+ID,(float)DataMass);
							    exchangeNet.GetDate();
			      	            PlayerPrefs.SetInt("PowerEngineIdBot"+PlayerPrefs.GetInt("IdBot"),PowerData);
							}
		                }
					}
				}
			}
		}
	}
	void SetMinMaxSlider()
	{
		switch(ID)
		{
			case 0: break;
			case 1: Slider_Power.minValue=60; Slider_Power.maxValue=200; break;
			case 2: Slider_Power.minValue=80; Slider_Power.maxValue=400; break;
			case 3: Slider_Power.minValue=120; Slider_Power.maxValue=800; break;
			case 4: Slider_Power.minValue=200; Slider_Power.maxValue=2500; break;
		}
	}
	void SetPrice()//__________________________________________________функция установки цены
	{
		int importance=100;
		double Vol;
		Vol=importance/Slider_Power.maxValue*PowerData;
		if(Vol<50)
		{
			SetMachPrice(0,0);
		}
		if(Vol>=50&&Vol<=75)
		{
			SetMachPrice(1,0);
		}
		if(Vol>75)
		{
			SetMachPrice(1,1);
		}
	}
	void SetMachPrice(int resS,int resG)
	{
		    //---------------------------------------------медь
			PriceC=PowerData*ID/2;
			PriceTxtCupper.text=PriceC.ToString();
			PriceRepairC=PriceC;
		    PriceRepairTxtCupper.text=PriceRepairC.ToString();
			//---------------------------------------------серебро
			PriceS=PowerData/10*ID/2*resS;
			PriceTxtSilver.text=PriceS.ToString();
			PriceRepairS=PriceS/10*resS;
		    PriceRepairTxtSilver.text=PriceRepairS.ToString();
			//---------------------------------------------Золото
			PriceG=PowerData/100*ID/2*resG;
			PriceTxtGold.text=PriceG.ToString();
			PriceRepairG=PriceG/100*resG;
		    PriceRepairTxtGold.text=PriceRepairG.ToString();
	}
	void SetSlider()
	{
		if(MenuAmmunition.panelEngine)
		{
			SetMinMaxSlider();
			if(tempID!=ID){ChecSelekt=true;}
			if(ChecSelekt)
			{
				ChecSelekt=false;
				tempID=ID;
				Slider_Power.value=PlayerPrefs.GetInt("PowerEngineIdBot"+PlayerPrefs.GetInt("IdBot"));
			}
		}
		if(!MenuAmmunition.panelEngine)
		    {
			    Slider_Power.value=60;
		    }
	}
	void SetDataEngin()
	{
		double tempM=DataMass*1000;
		int tempMInt=(int)tempM;
		string str=(tempMInt).ToString();
		Mass_Data.text=(tempMInt).ToString();
		Power_Data.text=PowerData.ToString();
		Power_ls_Txt.text=PowerData.ToString();
        MassEngine=tempMInt;
	}
}
