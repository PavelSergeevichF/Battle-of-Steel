using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_temp : MonoBehaviour {
    public GameObject ButtonMenu;
	public GameObject ButtonAmmunition;
	public GameObject ButtonDevelop;

	public void ClearPanel()
	{
        ButtonMenu.SetActive(false);
		ButtonAmmunition.SetActive(false);
		ButtonDevelop.SetActive(false);
	}
	public void ShowePanelMenu()
	{
			ClearPanel();
			ButtonMenu.SetActive(true);
	}
	public void ShowePanelAmmunition()
	{
			ClearPanel();
			ButtonAmmunition.SetActive(true);
	}
	public void ShowePanelDevelop()
	{
			ClearPanel();
			ButtonDevelop.SetActive(true);
	}
	public void LoadMap()
	{
		Application.LoadLevel("load_scens");
	}
}
