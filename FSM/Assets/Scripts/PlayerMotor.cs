using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    // public Camera cam;
    public Camera cm;

    Vector3 velocity = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Vector3 cameraRot = Vector3.zero;
    Rigidbody rb;

    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // cam = GetComponent<Camera>();
    }

    // gets a movement vector 
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _xRot)
    {
        cameraRot = _xRot;
    }
    // Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
    // perform movement based on velocity
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position+velocity*Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        if(rotation!=Vector3.zero)
        {
            rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));
        }

        if(cm!= null)
        {
            cm.transform.Rotate(-cameraRot);
        }
    }
}
