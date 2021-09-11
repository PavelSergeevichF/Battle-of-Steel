using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExchangeNet : MonoBehaviour
{
    public int Gold, Silver, Copper, Point,setUpDate=20; 
    public string BullShellStr, BotDataStr;
    void Start()
    {
        GetDate();
    }
    int i=0;
    void Update()
    {
        if(i<setUpDate)i++;
        if(i>setUpDate-2)
        {
            i=0;
            GetDate();
        }
    }
    public void GetDate()
    {
        for(int i=0;i<3;i++)
        {
            moneyGet();
            pointGet();
        }
    }
    void moneyGet()
    {
        StartCoroutine(moneyG(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
        StartCoroutine(moneyS(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
        StartCoroutine(moneyC(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
    }
    public void moneySet(double G, double S, double C,int OpDat)
    {
        StartCoroutine(moneySet(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password"),G,S,C,OpDat));
    }
    public void pointGet()
    {
        StartCoroutine(pointGetDate(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
    }
    public void pointSet(double P,int OpDat)
    {
        StartCoroutine(pointSetDate(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password"),P,OpDat));
    }
    public void botExchangeSet(int TypBot, string DataB)
    {
        StartCoroutine(BotSetDate(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password"),1,TypBot,DataB));
    }
    public void botExchangeGet()
    {
        StartCoroutine(BotGetDate(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
    }
    public void BullShellExchangeSet(string DataBS)
    {
        StartCoroutine(BullShellSetDate(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password"),1,DataBS));
    }
    public string BullShellExchangeGet()
    {
        StartCoroutine(BullShellGetDate(PlayerPrefs.GetString("Name"),PlayerPrefs.GetString("Password")));
        return BullShellStr;
    }
    private IEnumerator moneyG(string n, string p)
    {
         WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/moneyg.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Gold_Get: "+www.error);
            yield break;
        }
        Gold=transleteStrInt(www.text);
    }
    private IEnumerator moneyS(string n, string p)
    {
         WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/moneys.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Silver_Get: "+www.error);
            yield break;
        }
        Silver=transleteStrInt(www.text);
    }
    private IEnumerator moneyC(string n, string p)
    {
         WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/moneyc.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Copper_Get: "+www.error);
            yield break;
        }
        Copper=transleteStrInt(www.text);
    }
    private IEnumerator pointGetDate(string n, string p)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/point.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Point_Get: "+www.error);
            yield break;
        }
        Point=transleteStrInt(www.text);
    }
     private IEnumerator moneySet(string n, string p, double G, double S, double C, int OpD)
    {
         WWWForm form=new WWWForm();
        form.AddField("Name",       n);
        form.AddField("Password",   p);
        form.AddField("Gold"  ,(int)G);
        form.AddField("Silver",(int)S);
        form.AddField("Copper",(int)C);
        form.AddField("OperData", OpD);
        WWW www=new WWW("https://fpgame.ru/DataBase/money.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_money_Set: "+www.error);
            yield break;
        }
        Debug.Log("Покупка_money_Set: "+www.text);
    }
    private IEnumerator pointSetDate(string n, string p,double P,int OpD)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        form.AddField("Point",(int)P);
        form.AddField("OperData",OpD);
        WWW www=new WWW("https://fpgame.ru/DataBase/point.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Point_Set: "+www.error);
            yield break;
        }
        Debug.Log("Покупка_Point_Set: "+www.text);
        Point=transleteStrInt(www.text);
    }
    private IEnumerator BullShellGetDate(string n, string p)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/BullShell.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Point_Get: "+www.error);
            yield break;
        }
        BullShellStr=www.text;
    }
    private IEnumerator BullShellSetDate(string n, string p, int OpD, string BullShellD)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        form.AddField("OperData",OpD);
        form.AddField("DataBullShell",BullShellD);
        WWW www=new WWW("https://fpgame.ru/DataBase/BullShell.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Point_Set: "+www.error);
            yield break;
        }
        if(OpD==1)Debug.Log("данные_снаряды_отправка_Set: "+www.text);
        else Debug.Log("данные_снаряды_считывание: "+www.text);
    }
    private IEnumerator BotGetDate(string n, string p)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/BotData.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Point_Get: "+www.error);
            yield break;
        }
        BotDataStr=www.text;
    }
     private IEnumerator BotSetDate(string n, string p, int OpD, int typD, string BotD)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        form.AddField("OperData",OpD);
        form.AddField("TypBot",typD);
        form.AddField("DataBot",BotD);
        WWW www=new WWW("https://fpgame.ru/DataBase/BotData.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка_Point_Set: "+www.error);
            yield break;
        }
        //Debug.Log("Покупка_Point_Set: "+www.text);
    }
    int transleteStrInt(string inDate)
    {
        int LongM=inDate.Length;
        char[] monG = new char[LongM];
        int i=0, DataInt=0;
        foreach(char ch in inDate){monG[i]=ch; i++;}
        for(int a=0, b=LongM-1; a<LongM;a++, b--)
        {
            int intD=(int)monG[a]-48;
            DataInt+=intD*(int)Mathf.Pow(10,b);
        }
        return DataInt;
    }
}
