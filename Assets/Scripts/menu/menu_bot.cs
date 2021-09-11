using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_bot : MonoBehaviour 
{
  private TriggerEnter Trigger_Enter;
  public GameObject LBT;
  public GameObject SBT;
  public GameObject LT;
  public GameObject TT;
  public AudioSource AudioElevator;
  public bool Move=false; 
  public bool triggerBot;
  public bool triggerBt;
  public bool RedyTriggerBt;
  public bool UpEnter=true;
  public Animator ElevatorAnim;
  public int IDBot=1;
 
  private int SetI,SetJ;
  
  public int i=40;
  public int j=10;
  public int timeMoveElevator=45;
 
    public void Start()
	{
		SetI=i;
		SetJ=j;
		IDBot=PlayerPrefs.GetInt("IdBot");
		if(IDBot<1)IDBot=1;
		triggerBot=false;
		Trigger_Enter=GameObject.Find("TriggerEnter").GetComponent<TriggerEnter>();
		AudioElevator=GameObject.Find("Audio_Elevator").GetComponent<AudioSource>();
		triggerBt=false;
		RedyTriggerBt=false;
		UpEnter=true;
		SetActiveBot ();
	}
    public void ClearPanel()
	{
        LBT.SetActive(false);
		SBT.SetActive(false);
		LT.SetActive(false);
		TT.SetActive(false);
	}
	void Update () 
	{
		if(Move)
		{
			i--;
			if(i<1) { i=SetI;}
			ElevatorAnim.SetBool("move",true);
		}
		if(triggerBt)
		{
			j--;
			if(j<1) {triggerBt=false; j=SetJ;}
		}
		if(!UpEnter)
		{
			timeMoveElevator--;
			if(timeMoveElevator<=0){timeMoveElevator=45;Move=false;UpEnter=true;}
		}
		triggerBot=Trigger_Enter.Enter;
		if(triggerBot) {triggerBt=true;triggerBot=false;ElevatorAnim.SetBool("move",false);}
		if(RedyTriggerBt==true && triggerBt==true) SetActiveBot ();
	}
	
	void SondElevator()
	{
		AudioElevator.Play();
	}
	
	public void triggerLBT()	{	if(UpEnter==true) SetTypBot(1);  }

	public void triggerSBT()	{	if(UpEnter==true) SetTypBot(2);	}

	public void triggerLT() 	{	if(UpEnter==true) SetTypBot(3);	}

	public void triggerTT()  	{	if(UpEnter==true) SetTypBot(4);	}
	void SetTypBot(int a)
	{
		 if(IDBot!=a&&Move==false&&!triggerBt)
		{
			UpEnter=false;
			Move=true;
		    IDBot=a;
			PlayerPrefs.SetInt("IdBot", a);
		    RedyTriggerBt=true;
			SondElevator();
		}
	}
	public void SelectBotNext()
	{ 
	}
	public void SelectBotBeck()
	{ }
	public void SetActiveBot ()
	{
		triggerBt=false;
		RedyTriggerBt=false;
		ClearPanel();
		if(IDBot==1)LBT.SetActive(true);
		if(IDBot==2)SBT.SetActive(true);
		if(IDBot==3) LT.SetActive(true);
	    if(IDBot==4) TT.SetActive(true);
	}
}
