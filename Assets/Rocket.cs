using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //TODO TIME.DELTATIME
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 300f;


    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ///print("Thursting"); // Can thurst while rotating thats while blow we have IF not else...its inclusinve
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) //so it doesnt layer on top of each other..
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void Rotate()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        // take manual control of the roatation (below)
        // freeze physics rotatation before we take manual control of rotation
        rigidBody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A)) // here in the below we have else becase we want these exclusive
        {
            //print("Rotating Right");
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D)) // A takes presedence because it comes first
        {
            //print("Rotating Left");
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //here we resume the physics control of rotation
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing on friendly objects
                print("OK collision");
                break;

            case "Fuel":
                //do nothing on friendly objects
                print("OK collision");
                break;

            default:
                print("DEAD"); // kill player
                break;
        }
    }
}
