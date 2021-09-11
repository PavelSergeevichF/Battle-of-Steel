using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckData : MonoBehaviour 
{
	public information_menu InfoMenu;
	int TimeCheck=20,
	    time=0;
	bool CheckError=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		time++;
		if(time>=TimeCheck)
		{
			time=0;
			DataUser();
		}
	}
	void DataUser()
	{ 
		StartCoroutine(DataUserInNet());
	}
	private IEnumerator  DataUserInNet()
	{
		WWWForm form= new WWWForm();
		form.AddField("WelcomeMsg","Дороу");
		WWW www=new WWW("https://fpgame.ru/ChekNet/",form);
		yield return www;
		if(www.error !=null)
		{
			CheckError=true;
			yield break;
		}
		if(www.error ==null)CheckError=false;
	}
}
