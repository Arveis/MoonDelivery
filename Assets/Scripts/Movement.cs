using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotator = 100f;
    Rigidbody rocketRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
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
            rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotator);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotator);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rocketRigidBody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // unfreezing rotation so physics can over
    }
}
