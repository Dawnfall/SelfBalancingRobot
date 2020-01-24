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
    [SerializeField] float validDeltaAngle = 5f;

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

    public float GetDeltaAngle()
    {
        return CalcDesiredAngle() - GetBodyAngle();
    }

    private void Start()
    {
        leftWheel.maxAngularVelocity = maxAngularSpeed;
    }
    private void FixedUpdate()
    {
        float bodyAngle = GetBodyAngle();
        float goalBodyAngle = CalcDesiredAngle();

        float deltaAngle = CalcError(bodyAngle, goalBodyAngle);
        if (Mathf.Abs(deltaAngle) > validDeltaAngle)
        {
            if (deltaAngle > 0)
                Move(maxAngularSpeed);
            else
                Move(-maxAngularSpeed);
        }
        else
        {
            // move with inputs or stay still
            //if (Input.GetAxisRaw("Vertical") == 1f)
            //   Move(-maxAngularSpeed);
            //else if (Input.GetAxisRaw("Vertical") == -1f)
            //    Move(maxAngularSpeed);
        }
        //leftWheel.angularVelocity = leftWheel.transform.right * left * speed;
        //rightWheel.angularVelocity = rightWheel.transform.right * right * speed;

    }

    public void Move(float speed)
    {
        LeftMotorSpeed= speed;
        RightMotorSpeed = speed;
        leftWheel.angularVelocity = leftWheel.transform.right * speed;
        rightWheel.angularVelocity = rightWheel.transform.right * speed;
    }

    public float GetBodyAngle()
    {
        float cosAngle = Vector3.Dot(robotBody.up, Vector3.up);
        if (cosAngle == 1f)
            return 0f;

        Vector3 cross = Vector3.Cross(transform.up, Vector3.up);
        if (Vector3.Dot(cross, transform.right) > 0f)
            return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }
    public float CalcDesiredAngle()
    {
        //float vertInput = Input.GetAxisRaw("Vertical");
        //if (vertInput == 1f)
        //    return movingAngle;
        //if (vertInput == -1f)
        //    return -movingAngle;
        return 0f;
    }

    public float CalcError(float angle, float optimalAngle)
    {
        float currError = optimalAngle - angle;
        return currError;
    }
}
