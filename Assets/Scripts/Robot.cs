using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//***********************
// Represents 2wheeled robot

// it is capable of detecting controll input
// it can measure +/- angle from the global up vector and body up vector3
// it has two identical motors for powering wheels


public class Robot : MonoBehaviour
{
    [SerializeField] Rigidbody _robotBody;
    [SerializeField] Rigidbody _leftWheelRigidbody;
    [SerializeField] Rigidbody _rightWheelRigidbody;
    [SerializeField] float _diffDriveRate;
    [SerializeField] float _maxMotorPower;
    [SerializeField] SelfBalanceController _selfBalanceController;

    public Rigidbody RobotBody { get { return _robotBody; } }
    public Rigidbody LeftWheelRigidbody { get { return _leftWheelRigidbody; } }
    public Rigidbody RightWheelRigidbody { get { return _rightWheelRigidbody; } }
    public float DiffDriveRate { get { return _diffDriveRate; } }
    public float MaxMotorPower { get { return _maxMotorPower; } }

    public SelfBalanceController Controller { get { return _selfBalanceController; } }

    //******************
    // Robot measurements

    public float BodyAngle { get; private set; }
    public float VertInput { get; private set; }
    public float HorInput { get; private set; }

    //******************
    // Current motor powers

    public float LeftMotorPower { get; set; }
    public float RightMotorPower { get; set; }
    public float AvgMotorPower { get { return (LeftMotorPower + RightMotorPower) / 2f; } }

    private void Start()
    {
        //default is too low
        LeftWheelRigidbody.maxAngularVelocity = 10000f;
        RightWheelRigidbody.maxAngularVelocity = 10000f;
    }

    private void FixedUpdate()
    {
        // inputs and measurements
        VertInput = Input.GetAxisRaw("Vertical");
        HorInput = Input.GetAxisRaw("Horizontal");
        BodyAngle = getSignedAngleBetween(RobotBody.transform.up, Vector3.up, RobotBody.transform.right);

        // self balancing by the controller
        Controller.HandleRobot(this);

        // applying torque produced by the motors to the wheels 
        LeftWheelRigidbody.AddTorque(LeftWheelRigidbody.transform.right * LeftMotorPower);
        RightWheelRigidbody.AddTorque(RightWheelRigidbody.transform.right * RightMotorPower);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(LeftWheelRigidbody.transform.position, Forward * LeftMotorPower);
        Gizmos.DrawRay(RightWheelRigidbody.transform.position, Forward * RightMotorPower);
    }

    public Vector3 Forward
    {
        get
        {
            return Vector3.Cross(RobotBody.transform.right, Vector3.up).normalized;
        }
    }

    //assumes normalized vectors!
    public static float getSignedAngleBetween(Vector3 a, Vector3 b, Vector3 referenceNormal)
    {
        float cosAngle = Vector3.Dot(a, b); //gets angle between two normalized vectors
        if (cosAngle == 1f)
            return 0f;

        //this checks the orientation of the vectors to get either positive or negative angle
        Vector3 cross = Vector3.Cross(b, a);
        if (Vector3.Dot(cross, referenceNormal) > 0f)
            return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }
}