using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobotController
{
    RobotController Robot { get; }
}
public class RobotController : MonoBehaviour
{
    [SerializeField] float _maxAngularVelocity;

    [SerializeField] Rigidbody _leftWheel;
    [SerializeField] Rigidbody _rightWheel;
    [SerializeField] Transform _robotBody;
    [SerializeField] float _maxMotorPower;
    [SerializeField] float _maxBodyAngle;

    public Rigidbody LeftWheel { get { return _leftWheel; } set { _leftWheel = value; } }
    public Rigidbody RightWheel { get { return _rightWheel; } set { _rightWheel = value; } }
    public Transform RobotBody { get { return _robotBody; } set { _robotBody = value; } }
    public float MaxMotorPower { get { return _maxMotorPower; } set { _maxMotorPower = value; } }
    public float MaxBodyAngle { get { return _maxBodyAngle; } set { _maxBodyAngle = value; } }

    public Vector3 LeftWheelSpeed
    {
        get { return LeftWheel.angularVelocity; }
        private set { LeftWheel.angularVelocity = value; }
    }
    public Vector3 RightWheelSpeed
    {
        get { return RightWheel.angularVelocity; }
        private set { RightWheel.angularVelocity = value; }
    }

    public float MotorPower { get; set; } = 0f;
    public float DiffDrive { get; set; } = 0f;

    public float LeftMotorPower { get { return MotorPower * (1f + DiffDrive); } }
    public float RightMotorPower { get { return MotorPower * (1f - DiffDrive); } }


    private void Start()
    {
        LeftWheel.maxAngularVelocity = _maxAngularVelocity;
        RightWheel.maxAngularVelocity = _maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        LeftWheel.AddTorque(LeftWheel.transform.right * LeftMotorPower * MaxMotorPower * (1f + DiffDrive));
        RightWheel.AddTorque(RightWheel.transform.right * RightMotorPower * MaxMotorPower * (1f - DiffDrive));
    }

    public float GetBodyAngle()
    {
        float cosAngle = Vector3.Dot(RobotBody.up, Vector3.up);
        if (cosAngle == 1f)
            return 0f;

        Vector3 cross = Vector3.Cross(RobotBody.up, Vector3.up);
        if (Vector3.Dot(cross, RobotBody.right) > 0f)
            return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
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