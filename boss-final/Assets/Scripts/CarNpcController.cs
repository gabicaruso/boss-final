using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.SceneManagement;

public class CarNpcController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float distanceTravelled = 0;
    private float nextDistanceTravelled = 0;

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

    [SerializeField] private Rigidbody rb;

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;

    private float checkNPC = 0;
    public static int voltasNPC;

    private void Start()
    {
        rb.centerOfMass = new Vector3(0, massCenter, 0);
        voltasNPC = 0;
    }

    private void FixedUpdate()
    {
        if(voltasNPC >= 4)
        {
            SceneManager.LoadScene("Lose");
        }

        GetInput();
        HadleSteering();
        HandleMotor();
        UpdateWheels();
    }

    float hinputForAngle(float angle)
    {
        float deadzone = 5.0f;

        if(Mathf.Abs(angle) <= deadzone) return 0;
        return angle / Mathf.Abs(angle);
    }

    float vinputForAngle(float ang)
    {
        ang = Mathf.Abs(ang);
        return (180 - ang) / 180;
    }

    private void GetInput()
    {
        Vector3 pontoDeProva = transform.position + transform.forward * 5;
        nextDistanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(pontoDeProva);
        if (nextDistanceTravelled > distanceTravelled + 0.005f)
        {
            distanceTravelled = nextDistanceTravelled;
        }
        else
        {
            distanceTravelled = nextDistanceTravelled + 6f;
        }
        Vector3 pontoNoPath = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

        Vector3 t1 = Vector3.Scale((pontoNoPath - transform.position).normalized, new Vector3(1, 0, 1));
        float ang = Vector3.SignedAngle(transform.forward, t1, Vector3.up);
        horizontalInput = hinputForAngle(ang);
        verticalInput = vinputForAngle(ang);
    }
    
    private void HandleMotor()
    {
        if (rb.velocity.magnitude > 9)
        {
            motorForce = 100;
        } 
        else if (rb.velocity.magnitude < 2)
        {
            motorForce = 300;
        }
        else if (rb.velocity.magnitude < 9 && rb.velocity.magnitude > 2)
        {
            motorForce = 180;
        }
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
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
        if(col.CompareTag("check1") && checkNPC == 0)
        {
            voltasNPC++;
            checkNPC = 1;
        }
        if(col.CompareTag("check2") && checkNPC == 1)
        {
            checkNPC = 2;
        }
        if(col.CompareTag("check3") && checkNPC == 2)
        {
            checkNPC = 3;
        }
        if(col.CompareTag("check4") && checkNPC == 3)
        {
            checkNPC = 4;
        }
        if(col.CompareTag("check5") && checkNPC == 4)
        {
            checkNPC = 0;
        }
    }  
}
