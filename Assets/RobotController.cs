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

    public float AngleError { get; private set; }
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
        AngleError = CalcError(BodyAngle, GoalBodyAngle);

        if (AngleError > validDeltaAngle)
            Move(-maxAngularSpeed);
        else if (AngleError < -validDeltaAngle)
            Move(maxAngularSpeed);
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
        LeftMotorSpeed = speed;
        RightMotorSpeed = speed;
        leftWheel.angularVelocity = leftWheel.transform.right * speed;
        rightWheel.angularVelocity = rightWheel.transform.right * speed;
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

public static class HelperMath
{
    public static float GetAngleBetween(Vector3 from, Vector3 to, Vector3 referenceNormal)
    {
        float cosAngle = Vector3.Dot(from, to);
        if (cosAngle == 1f)
            return 0f;

        Vector3 cross = Vector3.Cross(from, to);
        Debug.Log(Vector3.Dot(cross, referenceNormal));
        if (Vector3.Dot(cross, referenceNormal) > 0f)
            return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }
}
