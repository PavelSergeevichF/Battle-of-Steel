using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUser : MonoBehaviour
{
    public int test_User_time=100;
    public string checkUsr;
    private SaveDataPrefs Save_DataPrefs;
    private information_menu Information_menu;
    void Start()
    {
        test_User_time=100;
        Save_DataPrefs=GameObject.Find("SaveDataPrefsObj").GetComponent<SaveDataPrefs>();
        Information_menu=GameObject.Find("information_menuObj").GetComponent<information_menu>();
        GetDateUser();//___
    }

    // Update is called once per frame
    void Update()
    {
        if(test_User_time>1){test_User_time--;}
        else {test_User_time=100;GetDateUser();}
    }
    private void GetDateUser()
    {
        StartCoroutine(checkUser(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
    }
    private IEnumerator checkUser(string N,string P)
    {
         WWWForm form=new WWWForm();
        form.AddField("Name",N);
        form.AddField("Password",P);
        WWW www=new WWW("https://fpgame.ru/DataBase/login.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_CheckUser: "+www.error);
            yield break;
        }
        checkUsr=www.text;
        if(checkUsr[0]=='!'&&!Information_menu.CheckShowPanel)
        {
            Debug.Log("+++");
            Information_menu.ShowInformation_Error_User();
            //Save_DataPrefs.ExitUser=true;
            //if(!Save_DataPrefs.RegUser) Save_DataPrefs.namePan.SetActive(true);
            //else Save_DataPrefs.RegUser=false;
        }
    }
}
