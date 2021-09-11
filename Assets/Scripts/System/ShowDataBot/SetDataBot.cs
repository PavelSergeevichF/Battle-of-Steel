using UnityEngine;
using UnityEngine.UI;

public class SetDataBot : MonoBehaviour 
{
    public int IdBotData;
	public int PowerData;
	//public double gold, silver,copper;
	public double[,] Body=new double[3,5];

	public double[,] Tow=new double[3,5];
	public double MassArmor;
	double Plotn=7.874;//г/см3 или т/м3
	public int[,] ArmorMM=new int[2,5];
	public Text GoldTxt,SilverTxt,CopperTxt;
	public Text PointTxt;
	public Text IDBotData;
    public Text Engine;
	public Text TipArmor;
	public Text TowArmorF, TowArmorBeck, TowArmorBort, TowArmorTop, TowArmorDown;
	public Text BodyArmorF, BodyArmorBeck, BodyArmorBort, BodyArmorTop, BodyArmorDown;
	public GameObject PointErr, GoldErr, SilverErr, CopperErr;
	ExchangeNet exchangeNet;
	

	
	void Start () 
	{
		exchangeNet=GameObject.Find("ExchangeNetOBJ").GetComponent<ExchangeNet>();
		IdBotData=PlayerPrefs.GetInt("IdBot");
		PowerData=PlayerPrefs.GetInt("PowerEngine");
        SetDataArmor();
		SetDataEngin();
		ClearPanelErr();
	}
	
	// Update is called once per frame
	void Update ()
	{
		DataMany();
		sentData();
	}
	public void ClearPanelErr()
	{
        PointErr.SetActive(false);
		GoldErr.SetActive(false);
		SilverErr.SetActive(false);
		CopperErr.SetActive(false);
	}
	public void SetActivePointErr()   { PointErr.SetActive(true);}
	public void SetActiveGoldErr()    {  GoldErr.SetActive(true);}
	public void SetActiveSilverErr()  {SilverErr.SetActive(true);}
	public void SetActiveCopperErr()  {CopperErr.SetActive(true);}
	public void ReSetActivePointErr() { PointErr.SetActive(false);}
	public void ReSetActiveGoldErr()  {  GoldErr.SetActive(false);}
	public void ReSetActiveSilverErr(){SilverErr.SetActive(false);}
	public void ReSetActiveCopperErr(){CopperErr.SetActive(false);}
	void DataMany()
	{
		GoldTxt.text  =exchangeNet.Gold  .ToString();
		SilverTxt.text=exchangeNet.Silver.ToString();
		CopperTxt.text=exchangeNet.Copper.ToString();
		PointTxt.text =exchangeNet.Point .ToString();
	}
	public void sentData()
	{
		IdBotData=PlayerPrefs.GetInt("IdBot");
		IDBotData.text=IdBotData.ToString();
		PowerData=PlayerPrefs.GetInt("PowerEngineIdBot"+PlayerPrefs.GetInt("IdBot"));
		Engine.text=PowerData.ToString();
		SetDataArmor();
		SetTextArmor();
	}
	void SetDataEngin()//_______________________________________________________________________________инициализируем данные по двигателям
	{
		if(PlayerPrefs.GetInt("PowerEngineIdBot1")<60) PlayerPrefs.SetInt("PowerEngineIdBot1",60 );
		if(PlayerPrefs.GetInt("PowerEngineIdBot2")<80)PlayerPrefs.SetInt("PowerEngineIdBot2",80);
		if(PlayerPrefs.GetInt("PowerEngineIdBot3")<200)PlayerPrefs.SetInt("PowerEngineIdBot3",200);
		if(PlayerPrefs.GetInt("PowerEngineIdBot4")<300)PlayerPrefs.SetInt("PowerEngineIdBot4",300);
	}
	void SetDataArmor()
	{
		for(int i=0;i<2;i++)
		for(int j=0;j<5;j++)
		{
			ArmorMM[i,j]=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData);
		}
		
	}
	void SetTextArmor()
	{
		TipArmor.text=PlayerPrefs.GetInt("TipArmor"+IdBotData).ToString();
		for(int i=0;i<2;i++)
		{
			if(i==0)
			{
				for(int j=0;j<5;j++)
		        {
					switch(j)
					{
						case 0: TowArmorF.text   =PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
                        case 1: TowArmorBeck.text=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break; 
						case 2: TowArmorBort.text=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
						case 3: TowArmorTop.text =PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
						case 4: TowArmorDown.text=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
					}
		        }
			}
			if(i==1)
			{
				for(int j=0;j<5;j++)
		        {
			       switch(j)
					{
						case 0: BodyArmorF.text   =PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
                        case 1: BodyArmorBeck.text=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
						case 2: BodyArmorBort.text=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
						case 3: BodyArmorTop.text =PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
						case 4: BodyArmorDown.text=PlayerPrefs.GetInt("DataArmorMM"+i+j+IdBotData).ToString(); break;
					}
		        }
			}
		}
		
		
	}
	void SetSize()
	{
		//1=x,          2=y,            3=z
		Tow[1,1]=0.8;   Tow[2,1]=0.8;   Tow[3,1]=0.3;  //бтр40 размеры
		Body[1,1]=5;    Body[2,1]=1.9;  Body[3,1]=1;

		Tow[1,2]=1.3;   Tow[2,2]=1.3;   Tow[3,2]=0.8;  //бтр80 размеры
		Body[1,2]=7.5;  Body[2,2]=2.5;  Body[3,2]=1.8;

		Tow[1,3]=1.3;   Tow[2,3]=1.3;   Tow[3,3]=0.7;  //танк бт
		Body[1,3]=5.8;  Body[2,3]=1.7;  Body[3,3]=1.1;

		Tow[1,4]=4.6;   Tow[2,4]=3;     Tow[3,4]=0.9;  //танк абрамс
		Body[1,4]=7.9;  Body[2,4]=3.6;  Body[3,4]=1.2;
	}
}
