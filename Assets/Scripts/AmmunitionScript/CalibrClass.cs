using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrClass : MonoBehaviour
{
    short Calibr;
    short typeBullShell;
    short VolBullShell;
    public CalibrClass()
    {
        Calibr=0;typeBullShell=0;VolBullShell=0;
    }
    public void SetVol(int C, int t, int V)
    {
        Calibr=(short)C; typeBullShell=(short)t; VolBullShell=(short)V;
    }
    public void GetVol(ref int C,ref int t, ref int V)
    {
        C=Calibr; t=typeBullShell; V=VolBullShell;
    }
    public string ConvertToString()
    {
        string str,C,V;
        str="#";
        if(Calibr<10){C="00";C+=Calibr;}
        else
        {
            if(Calibr<100){C="0";C+=Calibr;}
            else{C=Calibr.ToString();}
        }
        str+=C;
        str+=typeBullShell;
        if(VolBullShell<10){V="000";V+=VolBullShell;}
        else
        {
            if(VolBullShell<100){V="00";V+=VolBullShell;}
            else
            {
                if(VolBullShell<1000){V="0";V+=VolBullShell;}
                else{V=VolBullShell.ToString();}
            }
        }
        str+=V;
        str+="*";
        return str;
    }
}
