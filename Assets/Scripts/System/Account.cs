using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour 
{
	public Text Nic;
	private SaveDataPrefs Save_DataPrefs;
	// Use this for initialization
	void Start () 
	{
		Nic.text=PlayerPrefs.GetString("Name");
		Save_DataPrefs=GameObject.Find("SaveDataPrefsObj").GetComponent<SaveDataPrefs>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void BeckReg()
	{
		Save_DataPrefs.ExitUser=true;
		Save_DataPrefs.namePan.SetActive(true);
	}
}
