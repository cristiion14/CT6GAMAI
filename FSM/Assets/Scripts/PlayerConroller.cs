using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerConroller : MonoBehaviour {
    [SerializeField]
    private float speed = 20f;
    float lookSensitivity = 3f;
    private PlayerMotor motor;

     float speedH = 2.0f;
     float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    //   Rigidbody rb;
    // Use this for initialization
    void Start () { 
        motor = GetComponent<PlayerMotor>();
    }
	
	// Update is called once per frame
	void Update () {
        CalculateMovement();
        CalculateRotation();
     //   Rotate();
	}
    void CalculateMovement()
    {
        //transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * player.speed/2, 0, Input.GetAxisRaw("Vertical") * player.speed/2);

        //calculate movement velocity as a 3d vector
        
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;    //local
        Vector3 movVertical = transform.forward * zMov;
       
        //Final local velocity vector
        Vector3 _velocity = (movHorizontal + movVertical).normalized * speed;

        //apply movement
        motor.Move(_velocity);

    }
    void CalculateRotation()
    {
        //calculate player rot as a 3d Vector
        float yRot = Input.GetAxisRaw("Mouse X");
        //final rotation vector
        Vector3 _rotation = new Vector3(0, yRot, 0)*lookSensitivity;
        //apply rotation
        motor.Rotate(_rotation);

        //calculate camera rotation
        float xRot = Input.GetAxisRaw("Mouse Y");
        //final camera rotation vector
        Vector3 _xRot = new Vector3(xRot, 0, 0)*lookSensitivity;
        //apply
        motor.RotateCamera(_xRot);
        
    }
    void Rotate()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
