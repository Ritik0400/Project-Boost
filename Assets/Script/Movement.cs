using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rigid_body;
    [SerializeField] float MainThrust=0f;
    [SerializeField] float rotationthrust=0f;


    // Start is called before the first frame update
    void Start()
    {
        rigid_body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        

    }

    public void ProcessThrust(){        
        if(Input.GetKey(KeyCode.W)){
            rigid_body.AddRelativeForce(Vector3.up*MainThrust*Time.deltaTime);
        }
    }

    public void ProcessRotation(){

        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationthrust);
        }
        else if(Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationthrust);
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rigid_body.freezeRotation=true;     //freezing rotating so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigid_body.freezeRotation=false;    //unfreezing rotation so physics system can take over
    }
}
