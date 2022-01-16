using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] AudioClip mainEngine;
    [SerializeField] float MainThrust=0f;
    [SerializeField] float rotationthrust=0f;
    [SerializeField] ParticleSystem thrusteffect;
    [SerializeField] ParticleSystem rightrotateeffect;
    [SerializeField] ParticleSystem leftrotateeffect;

    Rigidbody rigid_body;
    AudioSource rocketnoise;


    void Start()
    {
        rigid_body=GetComponent<Rigidbody>();
        rocketnoise=GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    public void ProcessThrust(){        
        if(Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            rocketnoise.Pause();
            thrusteffect.Stop();
        }
    }
    public void ProcessRotation(){

        if(Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }
        else
        {
            leftrotateeffect.Stop();
            rightrotateeffect.Stop();
        }
    }


    public void StartThrusting()
    {
        rigid_body.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
        if (!thrusteffect.isPlaying)
        {
            thrusteffect.Play();
        }
        if (!rocketnoise.isPlaying)
        {
            rocketnoise.PlayOneShot(mainEngine);
        }
    }
    
    public void StartRotatingRight()
    {
        if (!rightrotateeffect.isPlaying)
        {
            rightrotateeffect.Play();
        }
        ApplyRotation(-rotationthrust);
    }
    public void StartRotatingLeft()
    {
        if (!leftrotateeffect.isPlaying)
        {
            leftrotateeffect.Play();
        }
        ApplyRotation(rotationthrust);
    }
    public void ApplyRotation(float rotationThisFrame)
    {
        rigid_body.freezeRotation=true;     //freezing rotating so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigid_body.freezeRotation=false;    //unfreezing rotation so physics system can take over
    }

}
