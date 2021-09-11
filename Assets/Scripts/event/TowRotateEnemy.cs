using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowRotateEnemy : MonoBehaviour
{
    public int angel=0;
    public int Speed=2;
    public int timeRotat=0,targetRotat=1;
    public Vector3 Rotation;
    public EnterInTrigger Trig; 
    public FindTheEnemy findTheEnemy;
    void Start()
    {
        
    }

    void Update()
    {
        if(Trig.Enter&&!findTheEnemy.EnterCel)
        {
            if(timeRotat<1)
            {
                if(targetRotat>0)targetRotat=-1;
                else targetRotat=1;
                timeRotat=Random.Range(0, 80);
            }
            else{timeRotat--;}
            Rotation.y=40*targetRotat;
            transform.Rotate(Rotation*Time.deltaTime*Speed);
        }
        else
        {
            findTheEnemy.EnterCel=false;
        }
    }
}
