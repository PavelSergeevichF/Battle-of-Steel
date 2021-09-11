using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNet : MonoBehaviour 
{
	public GameObject PanelNotInNet;
	public GameObject SignalPanelConsol;
	public GameObject TextOffLine;
	int TimeCheck=50,
	    time=0;
	bool ClosePanelNotNet=false;
	bool CheckError=false;
	void Start () 
	{
		PanelNotInNet.SetActive(false);
		TextOffLine.SetActive(false);
		WorkInNet();
	}
	
	void Update () 
	{
		WorkInNet();
		time++;
		if(time>=TimeCheck)
		{
			time=0;
			CheckNetFunk();
		}
	}
	void WorkInNet()
	{
		if(CheckError&&!ClosePanelNotNet)PanelNotInNet.SetActive(true);
		if(CheckError)SignalPanelConsol.SetActive(false);
		if(!CheckError)
		{
			SignalPanelConsol.SetActive(true);
			TextOffLine.SetActive(false);
			PanelNotInNet.SetActive(false);
			ClosePanelNotNet=false;
		}
	}
	public void WorkOffLine()
	{
		ClosePanelNotNet=true;
		TextOffLine.SetActive(true);
		PanelNotInNet.SetActive(false);
	}
	
	public void ExitGame()
	{
			Application.Quit();
	}
	private IEnumerator Send()
	{
		WWWForm form= new WWWForm();
		form.AddField("WelcomeMsg","Дороу");
		WWW www=new WWW("https://fpgame.ru/ChekNet/",form);
		yield return www;
		if(www.error !=null)
		{
			Debug.Log("Ошибка: "+www.error);
			CheckError=true;
			yield break;
		}
		if(www.error ==null)CheckError=false;
		//Debug.Log("сервер ответил: "+www.text);
	}
	public void CheckNetFunk()
	{
		StartCoroutine(Send());
	}
}
