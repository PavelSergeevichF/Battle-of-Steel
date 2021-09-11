using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassArmData : MonoBehaviour 
{
	public int IdBotData;
	int tempID;
	public float MassArmBot=1f;
	public double MassArmSent;
	public float[] Mass=new float[5];
	float[,] tempMassPlan=new float[5,2];
	float[,] X_Plan=new float[5,2];
	float[,] Y_Plan=new float[5,2];
	float coofMassArmor=1;
	float density=7.67f; //плотность стали т/м^3
	float tempMassArm=0f, MassArmTxt=0f;
	float x,y;
	private ArmorController Armor_Controller;
	private EngineController EngineController;
	public Text Mass_Data;

	// Use this for initialization
	void Start () 
	{
		EngineController=GameObject.Find("EngineController").GetComponent<EngineController>();//MassEngine
		Armor_Controller=GameObject.Find("ArmorController").GetComponent<ArmorController>();
		IdBotData=PlayerPrefs.GetInt("IdBot");
	}
	
	// Update is called once per frame
	void Update () 
	{
		MassMach();
		dataMassArm();
		PrintOutMassArm();
	}
	void MassMach()
	{
		if(Armor_Controller.TipArmor==2) coofMassArmor=0.9f;
		if(Armor_Controller.TipArmor==3) coofMassArmor=1f;
		else coofMassArmor=1.1f;
	}
	void MassBot1()
	{
		if(tempID!=IdBotData)tempMassArm=0f;
		/*бтр 40 */
		/*корпус длинна 5м ширина1,9м высота 1,45м */
		/*башня длинна01,3 ширина 1,3 высота 0,4  */
		float Tx=1.3f,Ty=1.3f,Tz=0.4f, Bx=5f,By=1.9f, Bz=1.45f;
		SetSize(Tx, Ty, Tz, Bx, By, Bz);
		MachMassArm(ref MassArmBot);
		MassArmTxt=MassArmBot;
		Mass[1]=MassArmBot;
		ResetSize();
		tempID=IdBotData;
	}
	void MassBot2()
	{
		if(tempID!=IdBotData)tempMassArm=0f;
		/*бтр 80 */
		/*корпус длинна 7,5м ширина2,95м высота 1,45м */
		/*башня длинна01,5 ширина 1,5 высота 0,7  */
		float Tx=1.5f,Ty=1.5f,Tz=0.7f, Bx=7.5f,By=2.95f, Bz=1.45f;
		SetSize(Tx, Ty, Tz, Bx, By, Bz);
		MachMassArm(ref MassArmBot);
		MassArmTxt=MassArmBot;
		Mass[2]=MassArmBot;
		ResetSize();
		tempID=IdBotData;
	}
	void MassBot3()
	{
		if(tempID!=IdBotData)tempMassArm=0f;
		/*бт */
		/*корпус длинна 5,6м ширина3,0м высота 1,3м */
		/*башня длинна 1,9 ширина 1,7 высота 0,8 */
		float Tx=1.9f,Ty=1.7f,Tz=0.8f, Bx=5.6f,By=3.0f, Bz=1.3f;
		SetSize(Tx, Ty, Tz, Bx, By, Bz);
		MachMassArm(ref MassArmBot);
		MassArmTxt=MassArmBot;
		Mass[3]=MassArmBot;
		ResetSize();
		tempID=IdBotData;
	}
	void MassBot4()
	{
		if(tempID!=IdBotData)tempMassArm=0f;
		/*тт */
		/*корпус длинна 8,9м ширина4,0м высота 1,6м */
		/*башня длинна 4,0 ширина 2,5 высота 0,8 */
		float Tx=4.0f,Ty=2.5f,Tz=0.8f, Bx=8.9f,By=4.0f, Bz=1.6f;
		SetSize(Tx, Ty, Tz, Bx, By, Bz);
		MachMassArm(ref MassArmBot);
		MassArmTxt=MassArmBot;
		Mass[4]=MassArmBot;
		ResetSize();
		tempID=IdBotData;
	}
	void MachMassArm(ref float massBot)
	{
		int ACi=Armor_Controller.i,ACj=Armor_Controller.j;
		//просчет массы башни
		if(ACi==0)
		{
			if(ACj==0){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//лоб
			if(ACj==1){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//корма
			if(ACj==2){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm*2;}//борт
			if(ACj==3){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//крыша
			if(ACj==4){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//дно
		}
		//просчет массы корпус
		if(ACi==1)
		{
			if(ACj==0){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//лоб
			if(ACj==1){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//корма
			if(ACj==2){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm*2;}//борт
			if(ACj==3){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//крыша
			if(ACj==4){MassPlan(X_Plan[ACj,ACi],Y_Plan[ACj,ACi]); if(tempMassPlan[ACj,ACi]!=tempMassArm) tempMassPlan[ACj,ACi]=tempMassArm;  }//дно
		}
		float temp=0f;
        foreach (float x in tempMassPlan)
		{
			temp +=x;
		}
		massBot=temp;
	}
	void MassPlan(float x, float y)
	{
		double tempArmMm=Armor_Controller.ArmorDataMm;
		tempArmMm=tempArmMm/1000;
		float ArmMM=(float)tempArmMm;
		tempMassArm=x*y*ArmMM*density*coofMassArmor; 
	}
	void ResetSize()
	{
		for(int i=0;i<2;i++)
		for(int j=0;j<5;j++)
		{
			X_Plan[j,i]=0;
		    Y_Plan[j,i]=0;
		}
	}
	void SetSize(float Tx=4.0f,float Ty=2.5f, float Tz=0.8f, float Bx=8.9f, float By=4.0f, float Bz=1.6f)
	{
		X_Plan[0,0]=Ty; Y_Plan[0,0]=Tz; //размер лобовой  плоскости башни
		X_Plan[1,0]=Ty; Y_Plan[1,0]=Tz; //размер кормовой плоскости башни
		X_Plan[2,0]=Tx; Y_Plan[2,0]=Tz; //размер бортовой плоскости башни
		X_Plan[3,0]=Tx; Y_Plan[3,0]=Ty; //размер верхней  плоскости башни
		X_Plan[4,0]=Tx; Y_Plan[4,0]=Ty; //размер нижней   плоскости башни
		X_Plan[0,1]=Ty; Y_Plan[0,1]=Bz; //размер лобовой  плоскости корпуса
		X_Plan[1,1]=Ty; Y_Plan[1,1]=Bz; //размер кормовой плоскости корпуса
		X_Plan[2,1]=Bx; Y_Plan[2,1]=Bz; //размер бортовой плоскости корпуса
		X_Plan[3,1]=Bx; Y_Plan[3,1]=Ty; //размер верхней  плоскости корпуса
		X_Plan[4,1]=Bx; Y_Plan[4,1]=Ty; //размер нижней   плоскости корпуса
	}
	void dataMassArm()
	{
		if(IdBotData==1)MassBot1();
		if(IdBotData==2)MassBot2();
		if(IdBotData==3)MassBot3();
		if(IdBotData==4)MassBot4();
	}
	void PrintOutMassArm()
	{
		MassArmSent=MassArmTxt;
		Mass_Data.text=(MassArmTxt).ToString();
	}
}
