using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPosSpawn : MonoBehaviour
{
    public Text PosSpawnA;
    public Text PosSpawnB;
    public GameManager gameManager;
    float[,] PlayerSpawnObjA = new float[10,3];
    float[,] PlayerSpawnObjB = new float[10,3];
    int timeUpDate = 30;
    // Start is called before the first frame update
    void Start()
    {
        GetPosition();
        ShowPosition();
    }

    // Update is called once per frame
    void Update()
    {
        timeUpDate--;
        if(timeUpDate<1)
        {
            GetPosition();
            ShowPosition();
            timeUpDate = 30;
        }
    }
    void GetPosition()
    { 
        for(int i=0;i<10;i++)
        {
            PlayerSpawnObjA[i, 0] = gameManager.pos_spawn[0, i].x;
            PlayerSpawnObjA[i, 1] = gameManager.pos_spawn[0, i].y;
            PlayerSpawnObjA[i, 2] = gameManager.pos_spawn[0, i].z;
            PlayerSpawnObjB[i, 0] = gameManager.pos_spawn[1, i].x;
            PlayerSpawnObjB[i, 1] = gameManager.pos_spawn[1, i].y;
            PlayerSpawnObjB[i, 2] = gameManager.pos_spawn[1, i].z;
        }
    }
    void ShowPosition()
    {
        string str = "InfoSpawnPos";
        string strA = str + "A\n";
        string strB = str + "B\n";
        for (int j=0;j<10;j++)
        for(int i=0;i<2;i++)
        {
            if (i == 0) 
            {
                strA +=
                        "X:"+ NFData(PlayerSpawnObjA[j, 0]) +
                        " Y:"+ NFData(PlayerSpawnObjA[j, 1]) +
                        " Z:"+ NFData(PlayerSpawnObjA[j, 2]);
                strA += "\n";
            }
            else 
            {
                strB +=
                        "X:" + NFData(PlayerSpawnObjB[j, 0]) +
                        " Y:" + NFData(PlayerSpawnObjB[j, 1]) +
                        " Z:" + NFData(PlayerSpawnObjB[j, 2]);
                strB += "\n";
            }
        }
        PosSpawnA.text= strA;
        PosSpawnB.text= strB;
}
    float NFData(float GetData)//not fool data
    {
        float temp;
        temp = GetData * 100;
        int tempI = (int)temp;
        temp = tempI / 100;
        return temp;
    }
}
