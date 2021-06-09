using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarNpcController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private Vector3 lookDirection;
    // private Quaternion lookRotation;

    [SerializeField] private float motorForce;
    // [SerializeField] private float speed = 10f;
    // [SerializeField] private float rotSpeed = 10f;
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
    [SerializeField] private Transform targetToBeFollowed;

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;

    private void Start()
    {
        rb.centerOfMass = new Vector3(0, massCenter, 0);
    }

    private void FixedUpdate()
    {
        // lookDirection = (targetToBeFollowed.position - transform.position).normalized;
        // transform.Translate(lookDirection * Time.deltaTime * speed);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

        // lookDirection = targetToBeFollowed.position - transform.position;
        // float singleStep = speed * Time.deltaTime;
        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, lookDirection, singleStep, 0.0f);
        // transform.rotation = Quaternion.LookRotation(newDirection);

        // transform.LookAt(targetToBeFollowed.position + new Vector3(0.0f, 2.0f, 0.0f));
        // transform.LookAt(targetToBeFollowed.position);
        // transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);

        GetInput();
        HadleSteering();
        HandleMotor();
        UpdateWheels();
    }

    private void GetInput()
    {
        // Debug.Log("GetInput()");
        Vector3 pontoDeProva = transform.position + transform.forward * 3;
        float distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(pontoDeProva);
        Vector3 posicaoPontoNoPath = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

        Vector3 t1 = Vector3.Scale((posicaoPontoNoPath - transform.position).normalized, new Vector3(1, 0, 1));
        float ang = Vector3.SignedAngle(transform.forward, t1, Vector3.up);
        if(ang > 0)
        {
            horizontalInput = 1;
        }
        else if(ang < 0)
        {
            horizontalInput = -1;
        }
        else if(ang == 0)
        {
            horizontalInput = 0;
        }
        verticalInput = 1;

        // horizontalInput = Input.GetAxis("Horizontal");
        // verticalInput = Input.GetAxis("Vertical");

        Debug.Log(horizontalInput);
        Debug.Log(verticalInput);
    }
    
    private void HandleMotor()
    {
        // Debug.Log("HandleMotor()");
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
}
