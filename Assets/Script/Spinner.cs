using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float SpinnerSpeed=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zValue=SpinnerSpeed*Time.deltaTime;
    
        transform.Rotate(0,0,zValue);
    }
}
