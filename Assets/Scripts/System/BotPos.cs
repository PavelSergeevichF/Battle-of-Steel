using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotPos : MonoBehaviour
{
    public Text PrintData;
    public float XPos, YPos, ZPos,YMaxPos;
    // Start is called before the first frame update
    void Start()
    {
        XPos = YPos = ZPos= YMaxPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ShowPos();
    }
    public void setPos(float PosX,float PosY,float PosZ)
    { 
        XPos = PosX; YPos = PosY; ZPos = PosZ;
        if (YMaxPos < YPos) YMaxPos = YPos;
    }
    void ShowPos()
    {
        string strPos="Position Bot"+"\nX: "+XPos+ "\nY: "+YPos+ "\nZ: "+ZPos+"\nYMax: "+ YMaxPos;
        PrintData.text = strPos;
    }
}
