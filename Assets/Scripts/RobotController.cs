using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Rigidbody leftWheel;
    public Rigidbody rightWheel;
    public Transform robotBody;

    public float maxMotorTorque;
    public float maxAngularVelocity;
    public Vector3 LeftWheelSpeed
    {
        get { return leftWheel.angularVelocity; }
        private set { leftWheel.angularVelocity = value; }
    }
    public Vector3 RightWheelSpeed
    {
        get { return rightWheel.angularVelocity; }
        set { rightWheel.angularVelocity = value; }
    }

    public float LeftMotorPower { get; set; }
    public float RightMotorPower { get; set; }

    public float MaxLeftAngularVelocity { get { return LeftMotorPower * maxAngularVelocity; } }
    public float MaxRightAngularVelocity { get { return RightMotorPower * maxAngularVelocity; } }


    private void Start()
    {
        leftWheel.maxAngularVelocity = maxAngularVelocity;
        rightWheel.maxAngularVelocity = maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        //SmoothDirection();

        //float newLeftX = LeftWheelSpeed.x + LeftMotorPower * maxMotorTorque * Time.fixedDeltaTime;
        //if (Mathf.Abs(newLeftX) < MaxLeftAngularVelocity || Mathf.Abs(newLeftX) < LeftWheelSpeed.x)
        //    LeftWheelSpeed = new Vector3(newLeftX, 0f, 0f);

        //float newRightX = RightWheelSpeed.x + RightMotorPower * maxMotorTorque * Time.fixedDeltaTime;
        //if (Mathf.Abs(newRightX) < MaxRightAngularVelocity || Mathf.Abs(newRightX) < RightWheelSpeed.x)
        //    RightWheelSpeed = new Vector3(newRightX, 0f, 0f);

        leftWheel.AddTorque(leftWheel.transform.right * LeftMotorPower * maxMotorTorque);
        rightWheel.AddTorque(rightWheel.transform.right * RightMotorPower * maxMotorTorque);



        //leftWheel.angularVelocity = new Vector3(leftWheel.angularVelocity.x, 0f, 0f);
        //rightWheel.angularVelocity = new Vector3(rightWheel.angularVelocity.x, 0f, 0f); ;


        //if (Mathf.Abs(Error) > validError)
        //else
        //{
        // move with inputs or stay still
        //if (Input.GetAxisRaw("Vertical") == 1f)
        //   Move(-maxAngularSpeed);
        //else if (Input.GetAxisRaw("Vertical") == -1f)
        //    Move(maxAngularSpeed);
        // }
        //leftWheel.angularVelocity = leftWheel.transform.right * left * speed;
        //rightWheel.angularVelocity = rightWheel.transform.right * right * speed;
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


    private void SmoothDirection()
    {
        Vector3 vec;
        vec = LeftWheelSpeed;
        vec.y = 0f;
        vec.z = 0f;
        leftWheel.angularVelocity = vec;

        vec = RightWheelSpeed;
        vec.y = 0f;
        vec.z = 0f;
        rightWheel.angularVelocity = vec;

        vec = transform.InverseTransformDirection(leftWheel.velocity);
        vec.y = 0f;
        vec.z = 0f;
        leftWheel.velocity = transform.TransformDirection(vec);

        vec = transform.InverseTransformDirection(rightWheel.velocity);
        vec.y = 0f;
        vec.z = 0f;
        rightWheel.velocity = transform.TransformDirection(vec);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}

