using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobotController
{
    RobotController Robot { get; }
}
public class RobotController : MonoBehaviour
{
    [SerializeField] Rigidbody _robotBody;
    [SerializeField] WheelController _leftWheel;
    [SerializeField] WheelController _rightWheel;
    [SerializeField] RobotParameters _robotParams;
    [SerializeField] Vector3 _centerOfMassOffset;

    public Rigidbody RobotBody { get { return _robotBody; } }
    public WheelController LeftWheel { get { return _leftWheel; } }
    public WheelController RightWheel { get { return _rightWheel; } }
    public RobotParameters Params { get { return _robotParams; } }

    public float AvgSignedVelocity { get { return (LeftWheel.SignedVelocity + RightWheel.SignedVelocity) / 2f; } }

    //*****************
    // set by balance controller

    public float GoalAvgVelocity { get; set; }
    public float GoalDiffDrive { get; set; }

    //******************
    // user inputs

    public float InputAvgVelocity { get; private set; }

    private void Start()
    {
        RobotBody.centerOfMass = _centerOfMassOffset;
        LeftWheel.WheelRigidBody.maxAngularVelocity = 10000f;
        RightWheel.WheelRigidBody.maxAngularVelocity = 10000f;

    }

    private void Update()
    {
        InputAvgVelocity = Input.GetAxisRaw("Vertical") * Params.MaxVelocity;
        GoalDiffDrive = Input.GetAxisRaw("Horizontal") * Params.DiffDriveRate;
    }

    private void FixedUpdate()
    {
        LeftWheel.UpdateWheel(GoalAvgVelocity * (1f + GoalDiffDrive), Params.MaxVelocity * (1f - GoalDiffDrive), Params.MaxMotorPower);
        RightWheel.UpdateWheel(GoalAvgVelocity * (1f - GoalDiffDrive), Params.MaxVelocity * (1f + GoalDiffDrive), Params.MaxMotorPower);
    }

    public float GetBodyAngle()
    {
        float cosAngle = Vector3.Dot(RobotBody.transform.up, Vector3.up);
        if (cosAngle == 1f)
            return 0f;

        Vector3 cross = Vector3.Cross(Vector3.up, RobotBody.transform.up);
        if (Vector3.Dot(cross, RobotBody.transform.right) > 0f)
            return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(LeftWheel.WheelRigidBody.transform.position, LeftWheel.WheelForward * LeftWheel.SignedVelocity);
        Gizmos.DrawRay(RightWheel.WheelRigidBody.transform.position, RightWheel.WheelForward * LeftWheel.SignedVelocity);
    }
}

[System.Serializable]
public class RobotParameters
{
    [SerializeField] float _diffDriveRate;
    [SerializeField] float _maxBodyAngle;
    [SerializeField] float _maxMotorPower;
    [SerializeField] float _maxVelocity;

    public float DiffDriveRate { get { return _diffDriveRate; } set { _diffDriveRate = value; } }
    public float MaxBodyAngle { get { return _maxBodyAngle; } set { _maxBodyAngle = value; } }
    public float MaxMotorPower { get { return _maxMotorPower; } set { _maxMotorPower = value; } }
    public float MaxVelocity { get { return _maxVelocity; } set { _maxVelocity = value; } }

}
//private void MoveByControlls()
//{
//    float vertInput = Input.GetAxisRaw("Vertical");
//    float horInput = Input.GetAxisRaw("Horizontal");

//    float deltaTorque = vertInput * horInput * diffDriveRate;

//    LeftMotorTorque = leftWheel.transform.right * (vertInput + deltaTorque) * maxTorque;
//    RightMotorTorque = rightWheel.transform.right * (vertInput - deltaTorque) * maxTorque;
//    leftWheel.AddTorque(LeftMotorTorque);
//    rightWheel.AddTorque(RightMotorTorque);
//}

//private void SmoothDirection()
//{
//    Vector3 vec;
//    vec = LeftWheelSpeed;
//    vec.y = 0f;
//    vec.z = 0f;
//    leftWheel.angularVelocity = vec;

//    vec = RightWheelSpeed;
//    vec.y = 0f;
//    vec.z = 0f;
//    rightWheel.angularVelocity = vec;

//    vec = transform.InverseTransformDirection(leftWheel.velocity);
//    vec.y = 0f;
//    vec.z = 0f;
//    leftWheel.velocity = transform.TransformDirection(vec);

//    vec = transform.InverseTransformDirection(rightWheel.velocity);
//    vec.y = 0f;
//    vec.z = 0f;
//    rightWheel.velocity = transform.TransformDirection(vec);
//}