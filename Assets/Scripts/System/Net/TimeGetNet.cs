using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;


public class TimeGetNet : MonoBehaviour
{
    public int i=0,hour=0,minutes=0,s=0;
    public bool CheckError=false;
    public string TimeGetStr, TimeGetStrH, TimeGetStrM,TimeGetStrS;
    public float Timef=0;
    public Text TimeText;
    void Update()
    {
        if(i<30)i++;
        else
        {
            if(i>28)
            {
                i=0;
                CheckTimeH();
                CheckTimeM();
                CheckTimeS();
                if(!CheckError)TimeMachP();
            }
        }
    }
    void CheckTime()
    {StartCoroutine(GetTime());}
    IEnumerator GetTime()
    {
        WWWForm form= new WWWForm();
        form.AddField("Msg","GetT");
        WWW www=new WWW("https://fpgame.ru/Time/time.php",form);
        yield return www;
		if(www.error !=null)
		{
			CheckError=true;
			yield break;
		}
		if(www.error ==null)CheckError=false;
        TimeGetStr=www.text;
    }
    void CheckTimeH()
    {StartCoroutine(GetTimeH());}
    IEnumerator GetTimeH()
    {
        WWWForm form= new WWWForm();
        form.AddField("Msg","GetT");
        WWW www=new WWW("https://fpgame.ru/Time/htime.php",form);
        yield return www;
		if(www.error !=null)
		{
			CheckError=true;
			yield break;
		}
		if(www.error ==null)CheckError=false;
        TimeGetStrH=www.text;
    }
    void CheckTimeM()
    {StartCoroutine(GetTimeM());}
    IEnumerator GetTimeM()
    {
        WWWForm form= new WWWForm();
        form.AddField("Msg","GetT");
        WWW www=new WWW("https://fpgame.ru/Time/mtime.php",form);
        yield return www;
		if(www.error !=null)
		{
			CheckError=true;
			yield break;
		}
		if(www.error ==null)CheckError=false;
        TimeGetStrM=www.text;
    }
    void CheckTimeS()
    {StartCoroutine(GetTimeS());}
    IEnumerator GetTimeS()
    {
        WWWForm form= new WWWForm();
        form.AddField("Msg","GetT");
        WWW www=new WWW("https://fpgame.ru/Time/stime.php",form);
        yield return www;
		if(www.error !=null)
		{
			CheckError=true;
			yield break;
		}
		if(www.error ==null)CheckError=false;
        TimeGetStrS=www.text;
    }
    void TimeMachP()
    {
        float TimeHf=float.Parse(TimeGetStrH,CultureInfo.InvariantCulture);
        hour=(int)TimeHf;
        float TimeMf=float.Parse(TimeGetStrM,CultureInfo.InvariantCulture);//float.Parse(TimeGetStrM,System.Globalization.CultureInfo.InvariantCulture);
        minutes=(int)TimeMf;
        float TimeSf=float.Parse(TimeGetStrS,CultureInfo.InvariantCulture);
        s=(int)TimeSf;
        string textPrint="";
        if(hour<10)textPrint+="0";
        textPrint+=TimeGetStrH;
        textPrint+=":";
        if(minutes<10)textPrint+="0";
        textPrint+=TimeGetStrM;
        //textPrint+=":";
        //textPrint+=TimeGetStrS;
        TimeText.text=textPrint;
        //Debug.Log("TimeGetStr "+TimeGetStr+"  Size "+TimeGetStr.Length);
    }
}
