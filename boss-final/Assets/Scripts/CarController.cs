﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;

    [SerializeField] private float motorForce;
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

    public Rigidbody rb;

    private void Start()
    {
        rb.centerOfMass = new Vector3(0, massCenter, 0);
    }

    private void FixedUpdate()
    {
        GetInput();
        HadleSteering();
        HandleMotor();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    
    private void HandleMotor()
    {   
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
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

    // private float horizontalInput;
    // private float verticalInput;
    // private float currentBreakForce;
    // private float currentSteerAngle;
    // private bool isBreaking;

    // [SerializeField] private float motorForce;
    // [SerializeField] private float breakForce;
    // [SerializeField] private float maxSteerAngle;

    // [SerializeField] private WheelCollider frontLeftWheelCollider;
    // [SerializeField] private WheelCollider frontRightWheelCollider;
    // [SerializeField] private WheelCollider rearLeftWheelCollider;
    // [SerializeField] private WheelCollider rearRightWheelCollider;

    // [SerializeField] private Transform frontLeftTransform;
    // [SerializeField] private Transform frontRightTransform;
    // [SerializeField] private Transform rearLeftTransform;
    // [SerializeField] private Transform rearRightTransform;

    // private void FixedUpdate()
    // {
    //     GetInput();
    //     HandleMotor();
    //     HadleSteering();
    //     UpdateWheels();
    // }

    // private void GetInput()
    // {
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     verticalInput = Input.GetAxis("Vertical");
    //     isBreaking = Input.GetKey(KeyCode.Space);
    // }
    
    // private void HandleMotor()
    // {
    //     frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
    //     frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    //     currentBreakForce = isBreaking ? breakForce : 0f;
    //     if (isBreaking)
    //     {
    //         ApplayBreaking();
    //     }
    // }

    // private void ApplayBreaking()
    // {
    //     frontLeftWheelCollider.motorTorque = currentBreakForce;
    //     frontRightWheelCollider.motorTorque = currentBreakForce;
    //     rearLeftWheelCollider.motorTorque = currentBreakForce;
    //     rearRightWheelCollider.motorTorque = currentBreakForce;
    // }

    // private void HadleSteering()
    // {
    //     currentSteerAngle = maxSteerAngle * horizontalInput;
    //     frontLeftWheelCollider.steerAngle = currentSteerAngle;
    //     frontRightWheelCollider.steerAngle = currentSteerAngle;
    // }

    // private void UpdateWheels()
    // {
    //     UpdateSingleWheel(frontLeftWheelCollider, frontLeftTransform);
    //     UpdateSingleWheel(frontRightWheelCollider, frontRightTransform);
    //     UpdateSingleWheel(rearLeftWheelCollider, rearLeftTransform);
    //     UpdateSingleWheel(rearRightWheelCollider, rearRightTransform);
    // }

    // private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    // {
    //     Vector3 pos;
    //     Quaternion rot;
    //     wheelCollider.GetWorldPose(out pos, out rot);
    //     wheelTransform.rotation = rot;
    //     wheelTransform.position = pos;
    // }    
}
