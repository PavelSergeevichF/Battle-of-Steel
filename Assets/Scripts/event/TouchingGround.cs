using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingGround : MonoBehaviour
{
    public Renderer Obj;
    private CharacterController ch_controller;
    public bool isTouchingGround=false;
    // Start is called before the first frame update
    void Start()
    {
		ch_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate ()
    {
        if (ch_controller.isGrounded){isTouchingGround=true; Obj.material.color=Color.green;}
        else {isTouchingGround=false;Obj.material.color=Color.red;}
    }
}
