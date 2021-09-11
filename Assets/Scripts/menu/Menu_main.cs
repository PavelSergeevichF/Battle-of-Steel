using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_main : MonoBehaviour 
{
public GameObject PanelMenu;
public GameObject PanelExitMenu;
public GameObject PanelAkkauntMenu;
public GameObject PanelLegionMenu;
public GameObject PanelShopMenu;
public GameObject PanelSettingsMenu;
public GameObject PanelEventsMenu;
public ClickSound clickSound;
public bool Aktiv_setting=false;

public void ClearPanel()
	{
		clickSound.ClickPlay();
		PanelExitMenu.SetActive(false);
		PanelAkkauntMenu.SetActive(false);
		PanelLegionMenu.SetActive(false);
		PanelSettingsMenu.SetActive(false);
		PanelEventsMenu.SetActive(false);
		PanelShopMenu.SetActive(false);
		Aktiv_setting=false;
	}
	public void closeMenu()
	{
		ClearPanel();
		PanelMenu.SetActive(false);
	}
	public void ShowePanelExitMenu()
	{
		ClearPanel();
		PanelExitMenu.SetActive(true);
	}
	public void ShowePanelShopMenu()
	{
		ClearPanel();
		PanelShopMenu.SetActive(true);
	}
	public void ShowePanelMenu()
	{
		ClearPanel();
		PanelMenu.SetActive(true);
	}
	public void ShowePanelAkkauntMenu()
	{
		ClearPanel();
		PanelAkkauntMenu.SetActive(true);
	}
	public void ShowePanelLegionMenu()
	{
		ClearPanel();
		PanelLegionMenu.SetActive(true);
	}
	public void ShowePanelSettingsMenu()//**********************************
	{
		ClearPanel();
		Aktiv_setting=true;
		PanelSettingsMenu.SetActive(true);
	}
	public void ShowePanelEventsMenu()
	{
		ClearPanel();
		PanelEventsMenu.SetActive(true);
	}
	public void ExitGame()
	{
		clickSound.ClickPlay();
		Application.Quit();
	}
}
