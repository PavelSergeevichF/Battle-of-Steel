using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Ammunition : MonoBehaviour 
{
	public GameObject PanelMenuAmmunition;
	public GameObject PanelArmor;
	public GameObject PanelEngine;
	public GameObject PanelGun;
	public GameObject PanelEquipment;
	public GameObject PanelAmmunition;
	public GameObject InfoBot;
	public bool panelMenuAmmunition=false, panelArmor=false , panelEngine=false, panelGun=false, panelEquipment=false, panelAmmunition=false,infoBot=false;
	public bool apply=false; 
	public SetDataBot setDataBot;
	public ClickSound clickSound;
	void Start ()
	{
		PanelArmor.SetActive(true);
		panelArmor=true;
	}

	public void ClearPanel()
	{
		clickSound.ClickPlay();
        PanelArmor.SetActive(false);
		PanelEngine.SetActive(false);
		InfoBot.SetActive(false);
		PanelGun.SetActive(false);
		PanelEquipment.SetActive(false);
		PanelAmmunition.SetActive(false);
		panelArmor=false; panelEngine=false; panelGun=false; panelEquipment=false; panelAmmunition=false;
		setDataBot.ReSetActiveGoldErr();
		setDataBot.ReSetActiveSilverErr();
		setDataBot.ReSetActiveCopperErr();
	}
	public void ExitMenuAmmunition()
	{
		ClearPanel();
		PanelMenuAmmunition.SetActive(false);
		panelMenuAmmunition=false;
	}
	public void ShoweAmmunitionMenu()
	{
		ClearPanel();
		ShowePanelArmor();
		PanelMenuAmmunition.SetActive(true);
		panelMenuAmmunition=true;
	}
	public void ShowePanelArmor()
	{
		ClearPanel();
		PanelArmor.SetActive(true);
		panelArmor=true;
	}
	public void ShowePanelEngine()
	{
		ClearPanel();
		PanelEngine.SetActive(true);
		panelEngine=true;
	}
	
	public void ShowePanelGun()
	{
		ClearPanel();
		PanelGun.SetActive(true);
		panelGun=true;
	}
	public void ShowePanelEquipment()
	{
		ClearPanel();
		PanelEquipment.SetActive(true);
		panelEquipment=true;
	}
	public void ShowePanelAmmunition()
	{
		ClearPanel();
		PanelAmmunition.SetActive(true);
		panelAmmunition=true;
	}
	public void ShowePanelInfoBot()
	{
		ClearPanel();
		InfoBot.SetActive(true);
		infoBot=true;
	}

	public void Apply()
	{
		clickSound.ClickPlay();
		apply=true;
	}
	
}
