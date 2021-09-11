using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorController : MonoBehaviour {

	public GameObject PanelArmor1;
	public GameObject PanelArmor2;
	public GameObject PanelArmor3;
	double PriceG=0,PriceS=0,PriceC=0;
	double PriceRepairG=0,PriceRepairS=0,PriceRepairC=0;

    public int[,] ArmorDataMM=new int[2,5];
	public int ArmorDataMm;
	public int IdBotData;
	public int GetArmorDataMm;
	public int i=1,j,TipArmor;//j-плоскость брони бота, i-выбор башня или корпус
	int StartArmor=5;
	int TempI=0;
	bool IsRedy=true;
	bool startScript=false;
	public Slider Slider_Armor;
	public Text ArmorData,ArmorObjBody,ArmorPanel;
	public Text PriceTxtGold,PriceTxtSilver,PriceTxtCupper;
	public Text PriceRepairTxtGold,PriceRepairTxtSilver,PriceRepairTxtCupper;
	public Menu_Ammunition MenuAmmunition;
	private MassArmData MassArmData;
	public ClickSound clickSound;
	 void Start () 
	 {
		startScript=false;
		clickSound=GameObject.Find("ClickSoundObj").GetComponent<ClickSound>();
		Check_Typ_Arm();
		MassArmData=GameObject.Find("MassArmDataObj").GetComponent<MassArmData>();
	    IdBotData=PlayerPrefs.GetInt("IdBot");
		SelectPlanArm();
		SelectElemArm();
		SetStartMinArmor();
		SetSlider();
		ShovTipArmor(PlayerPrefs.GetInt("TipArmor"+IdBotData ));
		startScript=true;
	 }
	 void Update ()
	{
		if(PlayerPrefs.GetInt("IdBot")!=IdBotData)IdBotData=PlayerPrefs.GetInt("IdBot");
		SetArmor();
		if(MenuAmmunition.panelArmor)SetPrice();
	}
	void Check_Typ_Arm()
	{
		if(TipArmor>0)
		{
			ShovTipArmor(TipArmor);
		}
		else
		{
			ShowePanelArmor1();
		}
	}
	public void ClearPanel()
	{
        PanelArmor1.SetActive(false);
		PanelArmor2.SetActive(false);
		PanelArmor3.SetActive(false);
	}
	void ShovTipArmor(int i)
	{
		switch(i)
		{
			case 1: ShowePanelArmor1(); break;
			case 2: ShowePanelArmor2(); break;
			case 3: ShowePanelArmor3(); break;
		}
	}

	public void ShowePanelArmor1()
	{
		if(startScript)clickSound.ClickPlay();
		ClearPanel();
		PanelArmor1.SetActive(true);
		TipArmor=1;
		PlayerPrefs.SetInt("TipArmor"+IdBotData, 1 );
	}
	public void ShowePanelArmor2()
	{
		if(startScript)clickSound.ClickPlay();
		ClearPanel();
		PanelArmor2.SetActive(true);
		TipArmor=2;
		PlayerPrefs.SetInt("TipArmor"+IdBotData, 2 );
	}
	public void ShowePanelArmor3()
	{
		if(startScript)clickSound.ClickPlay();
		ClearPanel();
		PanelArmor3.SetActive(true);
		TipArmor=3;
		PlayerPrefs.SetInt("TipArmor"+IdBotData, 3 );
	}
	void SetArmor()
	{
		SliderInArmor(Slider_Armor.value);
		ArmorData.text=ArmorDataMm.ToString();
	    ArmorDataMM[i,j]=ArmorDataMm;
		//под переделку для покупки
		PlayerPrefs.SetInt("DataArmorMM"+i+j+IdBotData,ArmorDataMm);
	}
	void SetSlider()
	{
		ArmorSetSlider(PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData));
	}
	void SetPrice()//__________________________________________________функция установки цены
	{
		double Vol=0;
		switch (IdBotData)
		{
			case 1: Vol=Slider_Armor.value*6.67; break;
			case 2: Vol=Slider_Armor.value*4; break;
			case 3: Vol=Slider_Armor.value*2; break;
			case 4: Vol=Slider_Armor.value/3; break;
			default: IdBotData=1; break;
		}
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
		float temp_k=1;
		switch (TipArmor)
		{
			case 1:temp_k=1f;break;
			case 2:temp_k=1.4f;break;
			case 3:temp_k=1.9f;break;
			default:break;
		}
		//---------------------------------------------медь
		PriceC=MassArmData.MassArmSent*temp_k*PlayerPrefs.GetInt("IdBot")/2;
		PriceTxtCupper.text=PriceC.ToString();
		PriceRepairC=PriceC;
		PriceRepairTxtCupper.text=PriceRepairC.ToString();
		//---------------------------------------------серебро
		PriceS=MassArmData.MassArmSent/10*temp_k*PlayerPrefs.GetInt("IdBot")/2*resS;
		PriceTxtSilver.text=PriceS.ToString();
		PriceRepairS=PriceS/10*resS;
		PriceRepairTxtSilver.text=PriceRepairS.ToString();
		//---------------------------------------------Золото
		PriceG=MassArmData.MassArmSent/100*temp_k*PlayerPrefs.GetInt("IdBot")/2*resG;
		PriceTxtGold.text=PriceG.ToString();
		PriceRepairG=PriceG/100*resG;
		PriceRepairTxtGold.text=PriceRepairG.ToString();
	}
	void SliderInArmor(float DataValArmor)
	{
		if(IdBotData==1){Slider_Armor.maxValue=15;  Slider_Armor.minValue=1;  if(DataValArmor<1){DataValArmor=1;}  ArmorDataMm=(int)DataValArmor;}
		if(IdBotData==2){Slider_Armor.maxValue=25;  Slider_Armor.minValue=1;  if(DataValArmor<5){DataValArmor=5;}  ArmorDataMm=(int)DataValArmor;}
		if(IdBotData==3){Slider_Armor.maxValue=50;  Slider_Armor.minValue=10; if(DataValArmor<10){DataValArmor=10;}ArmorDataMm=(int)DataValArmor;}
		if(IdBotData==4){Slider_Armor.maxValue=300; Slider_Armor.minValue=10; if(DataValArmor<15){DataValArmor=15;}ArmorDataMm=(int)DataValArmor;}
	}
	void ArmorSetSlider(float ArmData)
	{
		if(IdBotData==1)Slider_Armor.value=ArmData;
		if(IdBotData==2)Slider_Armor.value=ArmData;
		if(IdBotData==3)Slider_Armor.value=ArmData;
		if(IdBotData==4)Slider_Armor.value=ArmData;
	} 
	void SetMinArmor (int I, int J, int ID)
	{
		GetArmorDataMm=PlayerPrefs.GetInt("DataArmorMM"+I+J+ID);
		if(ID==1&&GetArmorDataMm< 1){PlayerPrefs.SetInt("DataArmorMM"+I+J+ID, 1 );}//Debug.Log("ID_1");
		if(ID==2&&GetArmorDataMm< 4){PlayerPrefs.SetInt("DataArmorMM"+I+J+ID, 5 );}
		if(ID==3&&GetArmorDataMm< 8){PlayerPrefs.SetInt("DataArmorMM"+I+J+ID, 8 );}
		if(ID==4&&GetArmorDataMm<20){PlayerPrefs.SetInt("DataArmorMM"+I+J+ID, 20);}
	}
	void SetStartMinArmor()
	{
		for(int a=1; a<6; a++)//____________________________________перебераем ID
		{
			for(int b=0; b<2; b++)//________________________________перебераем башня или корпус
			{
				for(int d=0; d<5; d++)//____________________________перебераем плоскостя
				{
					SetMinArmor(b,d,a); 
				}
			}
		}
	}
	//ArmorDataMM[i,j]
	void SetArmorTow()   {i=0;SetSlider();ArmorObjBody.text="башни";  }
	void SetArmorBody()  {i=1;SetSlider();ArmorObjBody.text="корпуса";}
	void SetArmorFront() {j=0;SetSlider();ArmorPanel.text  ="Лоб";    }
	void SetArmorBack()  {j=1;SetSlider();ArmorPanel.text  ="Корма";  }
	void SetArmorBort()  {j=2;SetSlider();ArmorPanel.text  ="Борт";   }
	void SetArmorTop()   {j=3;SetSlider();ArmorPanel.text  ="Верх";   }
	void SetArmorDown()  {j=4;SetSlider();ArmorPanel.text  ="Дно";    }//____________
	//Button_Select_elem_arm_text,Button_Select_plan_arm_text
	short plan_n=0;
	short elem_n=0;
	public void SelectPlanArm()//___________________функция переключения по кнопке плоскостей брони
	{
		if(startScript)clickSound.ClickPlay();
		plan_n++;
		if(plan_n>5)plan_n=1;
		switch (plan_n)
		{
			case 1: SetArmorFront(); break;
			case 2: SetArmorBort() ; break;
			case 3: SetArmorBack() ; break;
			case 4: SetArmorTop()  ; break;
			case 5: SetArmorDown() ; break;
			default: plan_n=1; break;
		}
	}
	public void SelectElemArm()//___________________функция переключения по кнопке частей брони
	{
		if(startScript)clickSound.ClickPlay();
		elem_n++;
		if(elem_n>2)elem_n=1;
		switch (elem_n)
		{
			case 1: SetArmorTow() ; break;
			case 2: SetArmorBody(); break;
			default: elem_n=1; break;
		}
	}

}
