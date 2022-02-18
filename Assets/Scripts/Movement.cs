using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotator = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustVFX;
    [SerializeField] ParticleSystem rightThrustVFX;
    [SerializeField] ParticleSystem leftThrustVFX;
    
    Rigidbody rocketRigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrustVFX.Stop();
    }

    private void StartThrusting()
    {
        rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustVFX.isPlaying)
        {
            mainThrustVFX.Play();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartLeftThrust();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            StartRightThrust();
        }

        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        rightThrustVFX.Stop();
        leftThrustVFX.Stop();
    }

    private void StartRightThrust()
    {
        ApplyRotation(-rotator);
        if (!rightThrustVFX.isPlaying)
        {
            rightThrustVFX.Play();
        }
    }

    private void StartLeftThrust()
    {
        ApplyRotation(rotator);
        if (!leftThrustVFX.isPlaying)
        {
            leftThrustVFX.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rocketRigidBody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // unfreezing rotation so physics can over
    }
}
