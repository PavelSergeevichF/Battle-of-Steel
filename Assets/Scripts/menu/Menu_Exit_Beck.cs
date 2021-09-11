using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Exit_Beck : MonoBehaviour
{
        public GameObject InfoText;
        public short s_t=20,t=0;
        public bool check=false;
        void Update ()
        {
            if(check&&t<s_t){t++;InfoText.SetActive(true);}
            else {InfoText.SetActive(false);t=0;}
            if(t>=s_t){check=false;t=0;}
        }
        void OnGUI()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                check=true;
                if(t>1)Application.Quit();
            }
        }
}
