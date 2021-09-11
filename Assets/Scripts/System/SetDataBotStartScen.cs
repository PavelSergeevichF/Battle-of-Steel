using UnityEngine;
using UnityEngine.UI;

public class SetDataBotStartScen : MonoBehaviour 
{

	public int[,,] Data_Armor_MM=new int[2,5,5];

	public int IdBotData;
	int i,j,TipArm;
	int ArmMin=5;
	float mass;
	private ArmorController Armor_Controller;
	private EngineController EngineController;
	private MassData massData;
	

	void Start () 
	{
		EngineController=GameObject.Find("EngineController").GetComponent<EngineController>();
		IdBotData=PlayerPrefs.GetInt("IdBot");
		Armor_Controller=GameObject.Find("ArmorController").GetComponent<ArmorController>();
		massData=GameObject.Find("MassDataObj").GetComponent<MassData>();
	}
	
	
	void Update () 
	{
		SetDataArmor();
		setDataEngine();
		BotMass();
	}
	//!!!!!!!!!!!!!!переделать передачу брони!!!!!!!!
	void SetDataArmor()
	{
		string str;
		for(int i=0;i<2;i++)
		{
			for(int j=0;j<5;j++)
			{	
				str="DataArmorMM"+i+j+IdBotData;
				if(PlayerPrefs.GetInt(str)<5){PlayerPrefs.SetInt(str, ArmMin);}
                PlayerPrefs.SetInt(str, Data_Armor_MM[i,j,IdBotData]);
			}
		}
		i=Armor_Controller.i;
		j=Armor_Controller.j;
		TipArm=Armor_Controller.TipArmor;
		Data_Armor_MM[i,j,IdBotData]=Armor_Controller.ArmorDataMM[i,j];
		str="DataArmorMM"+i+j+IdBotData;
        PlayerPrefs.SetInt(str, Data_Armor_MM[i,j,IdBotData]);
		PlayerPrefs.SetInt("TipArmor", TipArm);
	}
	public void setDataEngine()
    {
        PlayerPrefs.SetInt("PowerEngine", EngineController.PowerData);
    }
	void BotMass()
	{
		mass=massData.Mass;
		PlayerPrefs.SetFloat("Mass", mass);
	}
}
