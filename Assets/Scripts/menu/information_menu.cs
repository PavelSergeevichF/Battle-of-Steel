using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class information_menu : MonoBehaviour
 {
	//information_menuObj
	public GameObject PanelInformation;
	public Text HeadTxt,BodyTxt;
	public bool CheckShowPanel;
	void Start () 
	{
		CheckShowPanel=false;
		ClerPanel();
	}
	void Update () 
	{
		
	}
	public void ClerPanel()
	{
		PanelInformation.SetActive(false);
	}
	public void ShowInformationEngin()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Двигатель";
		BodyTxt.text="От него зависит как быстро будет ехать техника.";
	}
	public void ShowInformationArmor()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Броня";
		BodyTxt.text="Чем толще броня тем сложнее пробить, но и при наращивании толщены она становится и тяжелее!  Не перестарайтесь!";
	}
	public void ShowInformationWorkOffLine()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Игра в оффлайне";
		BodyTxt.text="Игра не работает не в сети, но можно посмотреть возможности мастерской";
	}
	public void ShowInformationEnginSmollErr()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Повторная покупка";
		BodyTxt.text="Вы пытаетесь купить двигатель который уже есть.";
	}
	public void ShowInformationWeaponErr()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Без оружия";
		BodyTxt.text="Вы пытаетесь выключить все оружие.";
	}
	public void ShowInformation_many_WeaponErr()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Много оружия";
		BodyTxt.text="На легкую бронемашину нельзя установить одновременно пушку и пулимет.";
	}
	public void ShowInformation_WeaponErr_double_buy()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Двойная покупка";
		BodyTxt.text="Вы пытаетесь купить оружие которое уже есть.";
	}
	public void ShowInformation_Weapon()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Оружие";
		BodyTxt.text="Можно настроить оружие так как удобно, но учитывайте массу. Калибр и длинна влияют на точность и пробитие.";
	}
	public void ShowInformation_Ammunition_Bullet()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Патроны";
		BodyTxt.text="Невозможно купить, так как не установлен пулимет.";
	}
	public void ShowInformation_Ammunition_Shell()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Снаряды";
		BodyTxt.text="Невозможно купить, так как не установлена пушка.";
	}
	public void ShowInformation_Ammunition_Bullet_Full()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Патроны";
		BodyTxt.text="Невозможно купить, так как мало места.";
	}
	public void ShowInformation_Ammunition_Shell_Full()
	{
		PanelInformation.SetActive(true);
		HeadTxt.text="Снаряды";
		BodyTxt.text="Невозможно купить, так как мало места.";
	}
	public void ShowInformation_Error_User()
	{
		CheckShowPanel=true;
		PanelInformation.SetActive(true);
		HeadTxt.text="Ошибка!";
		BodyTxt.text="По какой то причине, имя пользователя не найдено в базе данных!.";
	}
}
