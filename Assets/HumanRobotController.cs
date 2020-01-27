using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRobotController : MonoBehaviour
{
    public Rigidbody leftWheel;
    public Rigidbody rightWheel;

    public float maxMotorTorque;
    public float diffDriveRate;

    public float LeftMotorPower { get; set; }
    public float RightMotorPower { get; set; }
    public float DiffDrive { get; private set; }
    private void Start()
    {
        leftWheel.maxAngularVelocity = 1000f;
        rightWheel.maxAngularVelocity = 1000f;
    }

    private void FixedUpdate()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        float horInput = Input.GetAxisRaw("Horizontal");
        DiffDrive = diffDriveRate * horInput;

        LeftMotorPower = vertInput + vertInput * DiffDrive;
        RightMotorPower = vertInput - vertInput * DiffDrive;


        leftWheel.AddTorque(leftWheel.transform.right * LeftMotorPower * maxMotorTorque);
        rightWheel.AddTorque(rightWheel.transform.right * RightMotorPower * maxMotorTorque);
    }

}
