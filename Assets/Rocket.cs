using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;


    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            ///print("Thursting"); // Can thurst while rotating thats while blow we have IF not else...its inclusinve
            rigidBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.D)) // here in the below we have else becase we want these exclusive
        {
            //print("Rotating Right");
            transform.Rotate(-Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.A)) // D takes presedence because it comes first
        {
            //print("Rotating Left");
            transform.Rotate(Vector3.forward);
        }
    }
}
