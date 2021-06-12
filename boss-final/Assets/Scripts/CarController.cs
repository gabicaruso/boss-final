using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PathCreation;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float massCenter = -0.9f;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;

    [SerializeField] private Rigidbody rb;

    private float check = 0;
    public static int voltas;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("music");
        FindObjectOfType<AudioManager>().Play("car idle");
        
        rb.centerOfMass = new Vector3(0, massCenter, 0);
        voltas = 0;
    }

    private void FixedUpdate()
    {
        if(voltas >= 3)
        {
            SceneManager.LoadScene("Win");
        }

        GetInput();
        HadleSteering();
        HandleMotor();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space)) {
            FindObjectOfType<AudioManager>().Play("button");
            SceneManager.LoadScene("Drift Track");
        }
    }
    
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplayBreaking();
        }
    }

    private void ApplayBreaking()
    {
        frontLeftWheelCollider.motorTorque = currentBreakForce;
        frontRightWheelCollider.motorTorque = currentBreakForce;
        rearLeftWheelCollider.motorTorque = currentBreakForce;
        rearRightWheelCollider.motorTorque = currentBreakForce;
    }

    private void HadleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos = wheelTransform.position;
        Quaternion rot = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("check1") && check == 0)
        {
            voltas++;
            check = 1;
        }
        if(col.CompareTag("check2") && check == 1)
        {
            check = 2;
        }
        if(col.CompareTag("check3") && check == 2)
        {
            check = 3;
        }
        if(col.CompareTag("check4") && check == 3)
        {
            check = 4;
        }
        if(col.CompareTag("check5") && check == 4)
        {
            check = 0;
        }
    }  
}
