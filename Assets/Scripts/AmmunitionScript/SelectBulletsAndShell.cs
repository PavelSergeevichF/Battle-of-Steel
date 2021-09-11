using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBulletsAndShell : MonoBehaviour 
{
	const int Shelltyp=7;
	const int Bulletstyp=7;
	public int Type_Shell=0, Type_Bull=0;
	public GameObject BulletsPanel;
	public GameObject ShellPanel;
	public GameObject[] BulletsTyp =new GameObject[Bulletstyp];
	public GameObject[] ShellTyp =new GameObject[Shelltyp];
	public bool bullOrShell=false;
	bool startScript=false;
	int i=0;
	public GunController gunController;
    public ClickSound clickSound;
	void Start()
    {
		startScript=false;
		clickSound=GameObject.Find("ClickSoundObj").GetComponent<ClickSound>();
        ShoweBullets();
		startScript=true;
    }
	public void ShoweBullets()
	{
		if(startScript)clickSound.ClickPlay();
		bullOrShell=false;
		ShellPanel.SetActive(false);
		BulletsPanel.SetActive(true);
		
	}
	public void ShoweShell()
	{
		clickSound.ClickPlay();
		if(PlayerPrefs.GetInt("CaliberGunIdBot"+PlayerPrefs.GetInt("IdBot"))>35)
		{
			bullOrShell=true;
			//Bullets.SetActive(false);
			//Shell.SetActive(true);
			ShellPanel.SetActive(true);
			BulletsPanel.SetActive(false);
		}
		else
		{
			bullOrShell=true;
			//Shell.SetActive(false);
			//Bullets.SetActive(true);
			ShellPanel.SetActive(false);
			BulletsPanel.SetActive(true);
		}
	}
	//операции над пулями
	public void ShoweClerBullets()
	{
		for(int i=0;i<Bulletstyp;i++)
		BulletsTyp[i].SetActive(false);
	}
	
	public void SelectBulletNext()
	{
		ShoweClerBullets();
		i++;
		if(i>Bulletstyp-1)i=0;
		Type_Bull=i;
		BulletsTyp[i].SetActive(true);
	}
	public void SelectBulletBeck()
	{
		ShoweClerBullets();
		i--;
		if(i<0)i=Bulletstyp-1;
		Type_Bull=i;
		BulletsTyp[i].SetActive(true);
	}
	//операции над снарядами
	public void ShoweClerShell()
	{
		for(int i=0;i<Shelltyp;i++)
		ShellTyp[i].SetActive(false);
	}
	public void SelectShellNext()
	{
		int calibr=PlayerPrefs.GetInt("CaliberGunIdBot"+PlayerPrefs.GetInt("IdBot"));
		if(calibr>35)
		{
			ShoweClerShell();
		    i++;
			if(calibr>=200){if(i>Shelltyp-1)i=0;}
			else
		    if(calibr>=150&&calibr<200){if(i>Shelltyp-2)i=0;}
			else if(i>Shelltyp-3)i=0;
		    Type_Shell=i;
		    ShellTyp[i].SetActive(true);
		}
		else
		{
			ShoweClerShell();
			SelectBulletNext();
		}
	}
	public void SelectShellBeck()
	{
		int calibr=PlayerPrefs.GetInt("CaliberGunIdBot"+PlayerPrefs.GetInt("IdBot"));
		if(calibr>35)
		{
			ShoweClerShell();
			i--;
			if(calibr>=200){if(i<0)i=Shelltyp-1;}
			else 
			{
				if(calibr>=150&&calibr<200){if(i<0)i=Shelltyp-2;}
				else
				{if(i<0)i=Shelltyp-3;}
			}
			Type_Shell=i;
			ShellTyp[i].SetActive(true);
		}
		else
		{
			ShoweClerShell();
			SelectBulletBeck();
		}
	}
}
