using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveDataPrefs : MonoBehaviour 
{
//ExitUser
    //**********************************************************************
    public GameObject namePan;
	public GameObject RegPanel;//панель регистрации
    public GameObject ButtonClose;//закрытие регистрационной панели
    public GameObject ButtonInToRegPanel;//зарегестрировать нового пользователя
    menu_bot menuBot;
    public ExchangeNet Exchange_Net;
    public Text ErrorText1;
    public Text ErrorText2;
    public string NameIn, password, PasswordCheck, EmailIn;
    char[] Em=new char[50];
    char AEm='@', PointEm='.';
    
    public bool ExitUser=false;
    public bool Enter=true;
    public bool  CheckNameIn=false, 
          CheckpasswordIn=false,
          CheckpasswordIn2=false,
          RegUser=false,
          FirstStart=true;
    bool SetActiveRegistrUser=false,
         SetRedyRegistrUser=false,
         checkAEm=false,
         checkPointEm=false,
         checkEmail=false;
    string CheckAnswerReg,
           CheckAnswerLogin,
           strShB="";
    
    private void Start()
    {
        menuBot=GameObject.Find("menu_botObj").GetComponent<menu_bot>();
        ErrorText1.text=" ";
        RegUser=false;
        CheckNameIn=false;
        CheckpasswordIn=false;
        SetActiveRegistrUser=false;
        SetRedyRegistrUser=false;
        FirstStart=true;
        ButtonClose.SetActive(false);
        if (!PlayerPrefs.HasKey("Name")) namePan.SetActive(true);
        CloseRegistr();
    }
    void Update () 
    {
        ExitUserSet();
    }
    void ExitUserSet()
    {
        if(ExitUser)
        {
            namePan.SetActive(true);
            PlayerPrefs.SetString("Name", null);
            PlayerPrefs.SetString("Password", null);
        } 
    }
    public void GetName         (string N)   {  NameIn=N; }
    public void GetPassword     (string P)   {  password=P; }
    public void GetPasswordCheck(string PCh) {  PasswordCheck=PCh; }
    public void GetEmailIn      (string E)
    {
        foreach(char ch in E)
        {
            if(ch==AEm)checkAEm=true;
            if(ch==PointEm)checkPointEm=true;
            if(!checkAEm)
            {ErrorText1.text="Вы пропустили '@'";}
            else
            {
                if(!checkPointEm)ErrorText1.text="Вы пропустили '.'";
            }
        }
        if(checkAEm && checkPointEm)
        {
            ErrorText1.text=" ";
            checkEmail=true;
            EmailIn=E;
        }
        else
        {
            checkEmail=false;
        }
    }
    void InUser()
    {
        if (!string.IsNullOrEmpty(NameIn) && NameIn.Length >= 3)
        {
            ErrorText1.text=" ";
            ErrorText2.text=" ";
            CheckNameIn=true;
        }
        else 
        {
            ErrorText1.text="Имя слишком короткое!";
            CheckNameIn=false;
        }
        if(CheckNameIn)
        {
            if (!string.IsNullOrEmpty(password) && password.Length >= 6)
            {
                ErrorText1.text=" ";
                ErrorText2.text=" ";
                CheckpasswordIn=true;
            }
            else 
            {
                ErrorText1.text="Пароль слишком короткий!";
                CheckpasswordIn=false;
            }
            if(CheckNameIn && CheckpasswordIn){SentUserData(NameIn,password);}// && !Enter
        }
    }
    void RegistrUser()
    {
        if (!string.IsNullOrEmpty(NameIn) && NameIn.Length >= 3)
        {
            if(checkAEm && checkPointEm)
            {
                ErrorText1.text=" ";
                ErrorText2.text=" ";
            }
            CheckNameIn=true;
        }
        else 
        {
            ErrorText1.text="Имя слишком короткое!";
            CheckNameIn=false;
        }
        if(CheckNameIn)
        {
            if (!string.IsNullOrEmpty(password) && password.Length >= 6)
            {
                if(checkAEm && checkPointEm)
                {
                    ErrorText1.text=" ";
                    ErrorText2.text=" ";
                }
                CheckpasswordIn=true;
                CheckEntorPasswopds();
                if(CheckNameIn && CheckpasswordIn && CheckpasswordIn2 && checkEmail){SentNewUserData(NameIn,password,EmailIn);}
            }
            else 
            {
                ErrorText1.text="Пароль слишком короткий!";
                CheckpasswordIn=false;
            }
        }
    }
    void CheckEntorPasswopds()
    {
        if(password!=PasswordCheck){CheckpasswordIn2=false;ErrorText1.text="Пароли не совпадают!";}
        else {CheckpasswordIn2=true;}
    }
    void SentUserData(string N, string P)
    {
        StartCoroutine(GetUserConnectNet(N,P));
    }
    void SentNewUserData(string N,string P, string E)
    {
        ExitUser=false;
        StartCoroutine(RegistrUserConnectNet(N,P,E));
    }
    private IEnumerator RegistrUserConnectNet(string n, string p, string e)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        form.AddField("Email",e);
        string ConvertToStringCalibr(int CalibrTemp)
        {
            string str,C;
            str="#";
            if(CalibrTemp<10){C="00";C+=CalibrTemp;}
            else
            {
                if(CalibrTemp<100){C="0";C+=CalibrTemp;}
                else{C=CalibrTemp.ToString();}
            }
            str+=C;
            return str;
        }
        string ConvertToStringTypeVol(int TypeBullShellTemp, int VolBullShellTemp)
        {
            string str,V;
            str="*";
            str+=TypeBullShellTemp;
            if(VolBullShellTemp<10){V="000";V+=VolBullShellTemp;}
            else
            {
                if(VolBullShellTemp<100){V="00";V+=VolBullShellTemp;}
                else
                {
                    if(VolBullShellTemp<1000){V="0";V+=VolBullShellTemp;}
                    else{V=VolBullShellTemp.ToString();}
                }
            }
            str+=V;
            return str;
        }
        for(int ti=0;ti<262; ti++)
        {
            strShB+=ConvertToStringCalibr(ti);
            for(int j=0;j<7;j++)
            {
                strShB+=ConvertToStringTypeVol(j,0);
            }
        }
        form.AddField("BS",strShB);
        WWW www=new WWW("https://fpgame.ru/DataBase/register.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка: "+www.error);
            yield break;
        }
        CheckAnswerReg=www.text;
        PlayerPrefs.SetInt("TrackN",0);
        Debug.Log(CheckAnswerReg);
        char[] RegText=new char[120000];
        int i=0;
        foreach(char ch in CheckAnswerReg) 
        {
            RegText[i]=ch;
            i++;
        }
        if(RegText[0]=='0')
        {//**********************************************************************
            RegUser=true;
            ErrorText1.text=" ";
            PlayerPrefs.SetString("Name",NameIn);
            PlayerPrefs.SetString("Password",password);
            PlayerPrefs.SetInt("IdBot",1);
            menuBot.SetActiveBot ();
            Exchange_Net.GetDate();
            namePan.SetActive(false);
        }//**********************************************************************
        else 
        {
            ErrorText1.text="Логин занят!";
        }
    }
    private IEnumerator GetUserConnectNet(string n, string p)
    {
        WWWForm form=new WWWForm();
        form.AddField("Name",n);
        form.AddField("Password",p);
        WWW www=new WWW("https://fpgame.ru/DataBase/login.php",form);
        yield return www;
        if(www.error!=null)
        {
            Debug.Log("Ошибка: "+www.error);
            yield break;
        }
        CheckAnswerLogin=www.text;
        ArrayList LogInText=new ArrayList();
        //char[] LogInText=new char[500];
        int i=0;
        foreach(char ch in CheckAnswerLogin) 
        {
            LogInText.Add(ch);
            //LogInText[i]=ch;
            //i++;
        }
        if((char)LogInText[0]=='!')
        {
            ErrorText1.text="Логин не существует!";
        }
        else
        {
            if((char)LogInText[0]=='#')
            {
                ErrorText1.text="Пароль не верен!";
            }
            else
            {
                PlayerPrefs.SetString("Name",NameIn);
                PlayerPrefs.SetString("Password",password);
                PlayerPrefs.SetInt("IdBot",1);
                menuBot.SetActiveBot ();
                ExitUser=false;
                Exchange_Net.GetDate();
                namePan.SetActive(false);
                PlayerPrefs.SetInt("TrackN",0);
                Debug.Log("Сервер ответил: "+CheckAnswerLogin);
            }
        }
        
    }
    public void EnterDataUser()
    {
        if(Enter)
        {InUser();}
        else
        {RegistrUser();}
    }
    public void InToRegButton()
    {
        Enter=false;
        RegPanel.SetActive(true);
        ButtonClose.SetActive(true);
        ButtonInToRegPanel.SetActive(true);
    }
    public void CloseRegistr()
    {
        Enter=true;
        RegPanel.SetActive(false);
        ButtonClose.SetActive(false);
        ButtonInToRegPanel.SetActive(true);
    }
}
