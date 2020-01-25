using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] Rigidbody leftWheel;
    [SerializeField] Rigidbody rightWheel;
    [SerializeField] Transform robotBody;
    [SerializeField] float maxAngularSpeed;
    [SerializeField] float movingAngle = 20f;
    [SerializeField] float validError = 5f;
    public PID PidController;

    public Vector3 LeftWheelSpeed
    {
        get { return leftWheel.angularVelocity; }
    }
    public Vector3 RightWheelSpeed
    {
        get { return rightWheel.angularVelocity; }
    }
    public float LeftMotorSpeed { get; private set; }
    public float RightMotorSpeed { get; private set; }

    public float Error { get; private set; }
    public float BodyAngle { get; private set; }
    public float GoalBodyAngle { get; private set; }

    private void Start()
    {
        leftWheel.maxAngularVelocity = maxAngularSpeed;
    }
    private void FixedUpdate()
    {
        BodyAngle = HelperMath.GetAngleBetween(Vector3.up, robotBody.up, robotBody.right);
        GoalBodyAngle = CalcDesiredAngle();
        Error = PidController.CalcError(Time.fixedDeltaTime, BodyAngle, GoalBodyAngle);
        Move(maxAngularSpeed * Error);

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

    public void Move(float speed)
    {
        LeftMotorSpeed = speed;
        RightMotorSpeed = speed;
        leftWheel.AddTorque(leftWheel.transform.right * speed);
        rightWheel.AddTorque(rightWheel.transform.right * speed);
    }

    public float CalcDesiredAngle()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        if (vertInput == 1f)
            return movingAngle;
        if (vertInput == -1f)
            return -movingAngle;
        return 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}


