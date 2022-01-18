using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float movementFactor;
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period=2f;
    

    void Start()
    {
        startingPos=transform.position;
    }


    void Update()
    {
        
        float cycles=Time.time/period;
        const float tau=Mathf.PI*2;
        float rawSineWave=Mathf.Sin(cycles*tau);
        
        movementFactor=(rawSineWave+1f)/2f;

        Vector3 offset=movementVector*movementFactor;
        transform.position=startingPos+offset;
    }
}
