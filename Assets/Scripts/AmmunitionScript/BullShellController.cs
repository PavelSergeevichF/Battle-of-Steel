using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BullShellController : MonoBehaviour
{
    /* 
      реализовать сохранение данных в бд*/
    //char[] TytpeBull={'L','T','B','F','C','Z','Y'};
    //char[] TypeShell={'B','F','Y','Z'};
    string check_str_net;
    [SerializeField]
    int Summ_G, Summ_S, Summ_C,
        ID, temp_val, Load_Void_vol, mass_load;
    int[] Vol_Bull_Type_temp=new int[7];
    int[] Vol_Shell_Type_temp=new int[7];
    [SerializeField]
    short Calibr,typeBullShell, VolBullShell;

    float V_LBM=0.05f,  V_SBM=0.1f, V_LT=0.2f, V_TT=0.6f,
          Price_G, Price_S, Price_C;
    double Val_Sh_Bull_Data, Val_Sh_Bull_Data_Max,Val_Sh_Bull_Max,Mass=0,
           DevelopID1=1,DevelopID2=1,DevelopID3=1,DevelopID4=1;
    public Slider Val_Sh_Bull;
    public Text Val_Sh_Bull_Text,
                MassBullOrShel_Text,
                //Mass_Metric,
                PriceTxtGold,PriceTxtSilver,PriceTxtCupper;
    bool Full_Bull=false,
         Full_Shell=false,
         Full_All=false,
         Chek_Void_vol=false,
         check_data_net1=true,
         check_data_net2=true;
    public Menu_Ammunition menu_Ammunition;
    public information_menu InfoMenu;
    public SelectBulletsAndShell selectBulletsAndShell;
    public GunController gunController;
    public SetDataBot setDataBot;
    public ExchangeNet exchangeNet;
    public CalibrClass[] CalibrShB;
    void Start()
    {
        ID=PlayerPrefs.GetInt("IdBot");
    }
    void Update()
    {
        if(menu_Ammunition.panelAmmunition) 
        {
            ID=PlayerPrefs.GetInt("IdBot");
            SetMaxV();
            SetVol();
            SetPrice();
            PrintPrace();
            if(Chek_Void_vol)
            {
                Chek_Void_vol=false;
                Load_Void_vol=load_ammunition();
            }
            check_can_buy();
            if(check_data_net1)
            {
                getDataBullShellInfo();
            }
            else
            {
                GetDataDB();
            }
        }
        else {Chek_Void_vol=true; Mass=0;}
    }
    void SetPrice()
    {
        if(!selectBulletsAndShell.bullOrShell)//пулиметные и мелкоколиберные орудия
        {
             if(PlayerPrefs.GetInt("WeaponMG"+ID)==1)
            {
                int TypeBull_temp;
                float calibMG=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID);
                TypeBull_temp=selectBulletsAndShell.Type_Bull;
                switch(TypeBull_temp)
                {
                    case 0: SetPriceManey(0,0,0.015f,calibMG);          break;//простые
                    case 1: SetPriceManey(0,0,0.018f,calibMG);          break;//трасс
                    case 2: SetPriceManey(0,0.003f,0.021f,calibMG);     break;//бронебойн
                    case 3: SetPriceManey(0,0.005f,0.022f,calibMG);     break;//бронебойно-зажигательная
                    case 4: SetPriceManey(0,0.007f,0.025f,calibMG);     break;//бронебойно-трассирующая
                    case 5: SetPriceManey(0.002f,0.011f,0.028f,calibMG);break;//зажигательная
                    case 6: SetPriceManey(0.004f,0.012f,0.02f,calibMG); break;//бронебойно-трассирующая-зажигательная
                }
            }
        }
        else//орудийные
        {
            if(PlayerPrefs.GetInt("WeaponG"+ID)==1)
            {
                int TypeShell_temp;
                float calibG=PlayerPrefs.GetInt("CaliberGunIdBot"+ID);
                if(calibG<35){TypeShell_temp=selectBulletsAndShell.Type_Bull;}
                else{TypeShell_temp=selectBulletsAndShell.Type_Shell;}
                
                switch(TypeShell_temp)
                {
                    case 0: SetPriceManey(0,0,1.5f,calibG);      break;//бронебойные
                    case 1: SetPriceManey(0,0,1.8f,calibG);      break;//подкалиберные
                    case 2: SetPriceManey(0,0.03f,2.1f,calibG);  break;//осколочно-фугасные
                    case 3: SetPriceManey(0,0.5f,2.2f,calibG);   break;//осколочно-фугасные-трассирующие
                    case 4: SetPriceManey(0,0.7f,2.5f,calibG);   break;//кумулятивные
                    case 5: SetPriceManey(0.2f,1.1f,2.8f,calibG);break;//управляемые
                    case 6: SetPriceManey(0.4f,1.2f,3.1f,calibG);break;//спец
                }
            }
        }
    }
    void SetPriceManey(float gt, float st, float ct, float calib)
    {
        Price_G=calib*gt;
        Price_S=calib*st;
        Price_C=calib*ct;
    }
    void SetSuummPrice()
    {
        if(!selectBulletsAndShell.bullOrShell||PlayerPrefs.GetInt("CaliberGunIdBot"+ID)>=35)
        {
            float G=Price_G*temp_val/10;
            if(Price_G>0&&G<1&&temp_val>1)G=1;
            float S=Price_S*temp_val/10;
            if(Price_S>0&&S<1&&temp_val>1)S=1;
            float C=Price_C*temp_val/10;
            if(Price_C>0&&C<1&&temp_val>1)C=1;
            Summ_G=(int)G;
            Summ_S=(int)S;
            Summ_C=(int)C;
        }
        else
        {
            Summ_G=(int)Price_G*temp_val;
            Summ_S=(int)Price_S*temp_val;
            Summ_C=(int)Price_C*temp_val;
        }
    }
    void PrintPrace()
    {
        SetSuummPrice();
        PriceTxtGold.text=Summ_G.ToString();
        PriceTxtSilver.text=Summ_S.ToString();
        PriceTxtCupper.text=Summ_C.ToString();
    }
    //##################################################################################################################
    //в работе
    void PrintMass()
    {}
    //##################################################################################################################
    void SetVol()//просчет количества вмещающихся
    {
        if(VolShBull(selectBulletsAndShell.Type_Shell)>0) 
        {
            Val_Sh_Bull_Max=Val_Sh_Bull_Data_Max/VolShBull(selectBulletsAndShell.Type_Shell);//высчитываем значение количиства из объема методом VolShBull()
            Val_Sh_Bull_Max-=Load_Void_vol;
            if(Val_Sh_Bull_Max<1)Val_Sh_Bull_Max=0;
            if(!selectBulletsAndShell.bullOrShell||PlayerPrefs.GetInt("CaliberGunIdBot"+ID)<35)
            {
                if(Val_Sh_Bull_Max>4000)Val_Sh_Bull_Max=4000;
            }
        }
        else Val_Sh_Bull_Max=0;
        Val_Sh_Bull.maxValue=(float)Val_Sh_Bull_Max;//устанавливаем максимальное значение слайдера
        Val_Sh_Bull_Data=Val_Sh_Bull.value;//выставляем знвчение по слайдеру
        temp_val=(int)Val_Sh_Bull_Data;//переводим количество в целое число
        Val_Sh_Bull_Text.text=temp_val.ToString();//переопределяем в текст
    }
    void SetMaxV()//устанавливаем максимальный объем техники в зависимости от типа и развития
    {
        if(ID==1){Val_Sh_Bull_Data_Max=V_LBM*DevelopID1;}
        if(ID==2){Val_Sh_Bull_Data_Max=V_SBM*DevelopID1;}
        if(ID==3){Val_Sh_Bull_Data_Max=V_LT*DevelopID1;}
        if(ID==4){Val_Sh_Bull_Data_Max=V_TT*DevelopID1;}
    }
    double VolShBull(int TypeShell_temp)//просчет объема снаряда или пули в зависимости от типа
    {
        double temp=0,t=0;
        if(!selectBulletsAndShell.bullOrShell)//пули
        {
            if(PlayerPrefs.GetInt("WeaponMG"+ID)==1)
            {
                t=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID)*1.9f;
                t=t*t*t;
                temp=t/1000000000;
            }
            else temp=0;
            
        }
        else//снаряды
        {
            if(PlayerPrefs.GetInt("WeaponG"+ID)==1)
            {
                //TypeShell_temp=selectBulletsAndShell.Type_Shell;
                switch(TypeShell_temp)
                {
                    case 0: temp= Vol_Shell_V(1.9f); break;
                    case 1: temp= Vol_Shell_V(2f);   break;
                    case 2: temp= Vol_Shell_V(2.1f); break;
                    case 3: temp= Vol_Shell_V(2.1f); break;
                    case 4: temp= Vol_Shell_V(2f);   break;
                    case 5: temp= Vol_Shell_V(2.3f); break;
                    case 6: temp= Vol_Shell_V(2.3f); break;
                }
            }
            else temp=0;
        }
        return temp;
    }
     double MassShBull(int TypeShell_temp)//просчет массы снаряда или пули в зависимости от типа
    {
        double temp=0,m=0;
        if(!selectBulletsAndShell.bullOrShell)//пули
        {
            if(PlayerPrefs.GetInt("WeaponMG"+ID)==1)
            {
                m=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID);
                m=m*m*m*0.05;
                temp=m/1000000;
            }
            else temp=0;
            
        }
        else//снаряды
        {
            if(PlayerPrefs.GetInt("WeaponG"+ID)==1)
            {
                m=PlayerPrefs.GetInt("CaliberGunIdBot"+ID);
                switch(TypeShell_temp)
                {
                    case 0: temp= (m/0.005+m/0.2)/1000000;    break;
                    case 1: temp= (m/0.005+m/0.008)/1000000;  break;
                    case 2: temp= (m/0.005+m/0.006)/1000000;  break;
                    case 3: temp= (m/0.005+m/0.006)/1000000;  break;
                    case 4: temp= (m/0.005+m/0.008)/1000000;  break;
                    case 5: temp= (m/0.005+m/0.00212)/1000000;break;
                    case 6: temp= (m/0.005+m/0.1)/1000000;    break;
                }
            }
            else temp=0;
        }
        return temp;
    }
    double Vol_Shell_V(float K)//просчет объема снаряда с выстрелом
    {
        double t=0,temp=0;
        t=PlayerPrefs.GetInt("CaliberGunIdBot"+ID)*K;
        t=t*t*t;
        temp=t/1000000000;
        return temp;
    }
    void Chec_Vol_Bull_Calibr()//проверка уже купленых пуль данного калибра
    {
        if(PlayerPrefs.GetInt("WeaponG"+ID)==1)
        {
            for(int ii=0; ii<7; ii++ )
            {
                int calibr_Get=PlayerPrefs.GetInt("CaliberGunIdBot"+ID);
                string Get_Data_Calibr_Type=calibr_Get.ToString();
                Get_Data_Calibr_Type+=ii;
                Vol_Bull_Type_temp[ii]=PlayerPrefs.GetInt("Vol_Bull"+Get_Data_Calibr_Type);
            }
        }
    }
    void Chec_Vol_Shell_Calibr()//проверка уже купленых снарядов данного калибра
    {
        if(PlayerPrefs.GetInt("WeaponMG"+ID)==1)
        {
            for(int ii=0; ii<7; ii++ )
            {
                int calibr_Get=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID);
                string Get_Data_Calibr_Type=calibr_Get.ToString();
                Get_Data_Calibr_Type+=ii;
                Vol_Shell_Type_temp[ii]=PlayerPrefs.GetInt("Vol_Shell"+Get_Data_Calibr_Type);
            }
        }
    }
    int load_ammunition()//заполнение объема уже купленными снарядами
    {
        double Void_vol=0,Void_vol_Bull_Shell=0;
        int mass_b_sh=0, mass_l=0;
        //пули
        if(PlayerPrefs.GetInt("WeaponMG"+ID)==1)
        {
            int calibr_Get=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID);
            int[] Temp_type_Bull=new int [7];
            for(int ii=0; ii<7;ii++)
            {
                string Get_Data_Calibr_Type=calibr_Get.ToString();
                Get_Data_Calibr_Type+=ii;
                int vol_v=PlayerPrefs.GetInt("Vol_Bull"+Get_Data_Calibr_Type);
                Void_vol+=VolShBull(ii)*vol_v;
                mass_b_sh+=(int)(MassShBull(ii)*vol_v);
            }
            mass_l+=mass_b_sh;
        }
        Void_vol_Bull_Shell+=Void_vol;
        //снаряды
        if(PlayerPrefs.GetInt("WeaponG"+ID)==1)
        {
            int calibr_Get=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID);
            int[] Temp_type_Bull=new int [7];
            for(int ii=0; ii<7;ii++)
            {
                string Get_Data_Calibr_Type=calibr_Get.ToString();
                Get_Data_Calibr_Type+=ii;
                int vol_v=PlayerPrefs.GetInt("Vol_Shell"+Get_Data_Calibr_Type);
                Void_vol+=VolShBull(ii)*vol_v;
                mass_b_sh+=(int)(MassShBull(ii)*vol_v);
            }
            mass_l+=mass_b_sh;
        }
        mass_load=mass_l;
        Void_vol_Bull_Shell+=Void_vol;
        return (int)Void_vol_Bull_Shell;
    }
     void check_can_buy()//проверка на возможность покупки
    {
        if(exchangeNet.Gold<Summ_G)
        {
            setDataBot.SetActiveGoldErr();
            menu_Ammunition.apply=false;
        }
	    else
		{
			setDataBot.ReSetActiveGoldErr();
			if(exchangeNet.Silver<Summ_S){setDataBot.SetActiveSilverErr();menu_Ammunition.apply=false;}
			else
			{
				setDataBot.ReSetActiveSilverErr();
				if(exchangeNet.Copper<Summ_C){setDataBot.SetActiveCopperErr();menu_Ammunition.apply=false;}
				else
				{
					setDataBot.ReSetActiveCopperErr();
					if(menu_Ammunition.apply&&menu_Ammunition.panelAmmunition)
					{
			     	    menu_Ammunition.apply=false;
                        if(!selectBulletsAndShell.bullOrShell)//пулимет
                        {
                            /*
                             Full_Bull Full_Shell=false Full_All=false;
                            */
                            if(PlayerPrefs.GetInt("WeaponMG"+ID)==1)
                            {
                                if(Val_Sh_Bull_Max<1){InfoMenu.ShowInformation_Ammunition_Bullet_Full();}
                                else
                                {
                                    exchangeNet.moneySet(Summ_G,Summ_S,Summ_C,0);
                                    int calibr_Get=PlayerPrefs.GetInt("CaliberMachinGunIdBot"+ID);//берем калибр
                                    string Get_Data_Calibr_Type=calibr_Get.ToString();//преобразовываем калибр в строку
                                    Get_Data_Calibr_Type+=selectBulletsAndShell.Type_Bull;//добавляем тип в строку
                                    PlayerPrefs.SetInt("Vol_Bull"+Get_Data_Calibr_Type,temp_val);
                                    Mass=mass_load+MassShBull(selectBulletsAndShell.Type_Bull)*temp_val;
                                    PlayerPrefs.SetFloat("Vol_Bull_mass"+Get_Data_Calibr_Type,(float)Mass);//масса пуль
                                }
                            }
                            else
                            {InfoMenu.ShowInformation_Ammunition_Bullet();}
                        }
                        else//пушка
                        {
                            if(PlayerPrefs.GetInt("WeaponG"+ID)==1)
                            {
                                if(Val_Sh_Bull_Max<1){InfoMenu.ShowInformation_Ammunition_Shell_Full();}
                                else
                                {
                                    exchangeNet.moneySet(Summ_G,Summ_S,Summ_C,0);
                                    int calibr_Get=PlayerPrefs.GetInt("CaliberGunIdBot"+ID);
                                    string Get_Data_Calibr_Type=calibr_Get.ToString();
                                    Get_Data_Calibr_Type+=selectBulletsAndShell.Type_Bull;
                                    PlayerPrefs.SetInt("Vol_Shell"+Get_Data_Calibr_Type,temp_val);
                                    Mass=mass_load+MassShBull(selectBulletsAndShell.Type_Shell)*temp_val;
                                    PlayerPrefs.SetFloat("Vol_Shell_mass"+Get_Data_Calibr_Type,(float)Mass);//масса снарядов
                                }
                            }
                            else
                            {InfoMenu.ShowInformation_Ammunition_Shell();}
                        }
		            }
				}
			}
		}
    }
    //##################################################################################################################
    //в работе
    void GetVol(ref int C,ref int t, ref int V)
    {
        C=Calibr; t=typeBullShell; V=VolBullShell;
    }
    //##################################################################################################################
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
    void GetDataDB()
    {
        string strShB="";
        string tempstr=exchangeNet.BullShellExchangeGet();
        if(check_str_net.Length>0)
        Debug.Log("Данные_"+check_str_net);
        if(check_str_net.Length<1&&check_data_net2)
        {
            for(int i=0;i<262; i++)
            {
                strShB+=ConvertToStringCalibr(i);
                for(int j=0;j<7;j++)
                {
                    strShB+=ConvertToStringTypeVol(j,0);
                }
            }
            check_str_net=exchangeNet.BullShellExchangeGet();
            if(check_str_net.Length<1)check_data_net2=false;
            exchangeNet.BullShellExchangeSet(strShB);
            Debug.Log("массив пуст");
        }
    }
    void getDataBullShellInfo()
    {
        check_str_net=exchangeNet.BullShellExchangeGet();
        if(check_str_net.Length<1)check_data_net1=false;
        Debug.Log("Значение_"+check_str_net);
    }
    //##################################################################################################################
}
