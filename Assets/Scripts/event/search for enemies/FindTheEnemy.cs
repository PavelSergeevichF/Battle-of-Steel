using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheEnemy : MonoBehaviour
{
    public bool EnterCel=false;
    public bool SpawnMG=false;
    public bool SpawnG=false;
    void OnTriggerStay(Collider Col)
    {
        if(Col.gameObject.tag=="Gammer")//ShellPrefabCube
        {
            EnterCel=true;
            SpawnMG=true;
        }
    }
    void OnTriggerExit(Collider Col)
    {
        if(Col.gameObject.tag=="Gammer")//ShellPrefabCube
        {
            EnterCel=false;
        }
    }
}
