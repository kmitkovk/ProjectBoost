using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // does not two oscilator scripts for this component
public class Oscillator : MonoBehaviour
{
    
    //TODO remove from inspector later (the current test value)

    //below two you can stack up above the variable to modify it 
    // (but can all be done in the same line too) kinda like a @ in python
    [Range(0,1)]
    [SerializeField]
    float movementFactor; // 0 for not moved, 1 for fully moved

    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f; //seconds
    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //set Movement facor auctomatically:
        if (period <= Mathf.Epsilon) { return; } // protect agains period is zero,, Lecture 63
        float cycles = Time.time / period; // grows continually from 0 (#cycles) 
        // Time.Time is already making it frame rate independent

        const float tau = Mathf.PI * 2; //tau is about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
