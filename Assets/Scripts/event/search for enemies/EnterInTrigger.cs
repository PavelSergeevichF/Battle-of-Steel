using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInTrigger : MonoBehaviour
{
    public bool Enter=false;
   void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag=="Gammer")//ShellPrefabCube
        {
            Enter=true;
            Debug.Log("+Find Enemy");
        }
    }
}
