using UnityEngine;

public class GetDataBot : MonoBehaviour 
{
	public GameObject LBT;
    public GameObject SBT;
    public GameObject LT;
    public GameObject TT;
    public int SaveBotId;
	// Use this for initialization
	void Start () 
	{
		SaveBotId=PlayerPrefs.GetInt("IdBot");
		SetActiveBot ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void ClearPanel()
	{
        LBT.SetActive(false);
		SBT.SetActive(false);
		LT.SetActive(false);
		TT.SetActive(false);
	}
	void SetActiveBot ()
	{
		ClearPanel();
		if(SaveBotId==1)LBT.SetActive(true);
		if(SaveBotId==2)SBT.SetActive(true);
		if(SaveBotId==3) LT.SetActive(true);
	    if(SaveBotId==4) TT.SetActive(true);
	}
}
