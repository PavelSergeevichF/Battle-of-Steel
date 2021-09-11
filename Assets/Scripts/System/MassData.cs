using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassData : MonoBehaviour 
{
	int IdBotData;
	float[]  MassArmTxt=new float[5];
	public float  Mass=0f;
	private MassArmData Mass_Arm_Data;
	private EngineController EngineController;
	public Text Mass_Data;

	// Use this for initialization
	void Start () 
	{
		EngineController=GameObject.Find("EngineController").GetComponent<EngineController>();
		Mass_Arm_Data=GameObject.Find("MassArmDataObj").GetComponent<MassArmData>();
		IdBotData=PlayerPrefs.GetInt("IdBot");
	}
	
	// Update is called once per frame
	void Update () 
	{
		MassMach();
		PrintOutMassArm();
	}
	void MassMach()
	{
		/*бтр 40 5,3 */
		/*бтр 80 13,6 */
		/*БРМ-3К «Рысь» 19 */
		IdBotData=PlayerPrefs.GetInt("IdBot");
		MassArmTxt[IdBotData]=0f;
		if(IdBotData==1)MassArmTxt[IdBotData]+=1.5f;
		if(IdBotData==2)MassArmTxt[IdBotData]+=4f;
		if(IdBotData==3)MassArmTxt[IdBotData]+=5f;
		if(IdBotData==4)MassArmTxt[IdBotData]+=5f;
		MassArmTxt[IdBotData]+=(float)Mass_Arm_Data.MassArmSent;
		double tempEngin=EngineController.MassEngine/1000;
		Mass=(float)tempEngin+MassArmTxt[IdBotData];
		float tempMass=MassArmTxt[IdBotData]+PlayerPrefs.GetFloat("MassEngine"+IdBotData)+PlayerPrefs.GetFloat("MassMGun"+IdBotData)+PlayerPrefs.GetFloat("MassGun"+IdBotData);
		PlayerPrefs.SetFloat("MasBot"+IdBotData,tempMass);
	}
	
	void PrintOutMassArm()
	{
		Mass_Data.text=(Mass).ToString();
	}
}
