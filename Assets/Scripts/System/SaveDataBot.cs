using UnityEngine;

public class SaveDataBot : MonoBehaviour 
{
	public GameObject LBT;
    public GameObject SBT;
    public GameObject LT;
    public GameObject TT;

    public int SaveBotId;
	public bool Activ;
	private menu_bot SBotId;

	// Use this for initialization
	void Start () 
	{
		SBotId=GameObject.Find("BotController").GetComponent<menu_bot>();
	}

	
	
	// Update is called once per frame
	void Update () 
	{
		if(SBotId.triggerBt) setIDBot();
	}
	void setIDBot()
	{
		PlayerPrefs.SetInt("IdBot", SBotId.IDBot);
	}
	
}
