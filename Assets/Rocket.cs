using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //TODO TIME.DELTATIME
    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

    int currentLevel = 0;

    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 300f;
    [SerializeField] float levelLoadDelay = 1f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip sucessSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;


    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        { 
            RespondToThrustInput();
            RespondToRotateInput();
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive) { return; } //ignore colisions when dead
        

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing on friendly objects
                print("OK collision");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        //do nothing on friendly objects
        print("END LEVEL");
        state = State.Transcending;
        audioSource.Stop();
        if(!successParticles.isPlaying) { successParticles.Play(); }
        audioSource.PlayOneShot(sucessSound, 0.2f); // second arg for volume
        currentLevel++;
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    void StartDeathSequence()
    {
        print("Hit Something Deadly"); // kill player
        state = State.Dying;
        audioSource.Stop();
        //transform.DetachChildren(); find a way to make the booster and nose fly off
        if (!deathParticles.isPlaying) { deathParticles.Play(); }
        audioSource.PlayOneShot(deathSound, 0.2f); // second arg for volume
        Invoke("LoadFirstLevel", 0.5f);
    }

    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ApplyThrust()
    {
        ///print("Thursting"); // Can thurst while rotating thats while blow we have IF not else...its inclusinve
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) //so it doesnt layer on top of each other..
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying) // simple mainEnginePartc.Play() wasnt working
        {
            mainEngineParticles.Play();
        }
        
    }

    void RespondToRotateInput()
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


    void LoadNextLevel()
    {
        audioSource.Stop();
        SceneManager.LoadScene(currentLevel); // TODO allow for more than 2 levels.
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(currentLevel); // todo allow for more than 2 levels
    }
}
