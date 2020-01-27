using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBalanceController : MonoBehaviour
{
    [SerializeField] float movingAngle = 1f;
    [SerializeField] float diffDriveRate;
    [SerializeField] float _maxAbsValue;

    public PID PidController;
    public RobotController RobotController;

    public float BodyAngle { get; private set; }
    public float GoalBodyAngle { get; private set; }
    public float DiffDrive { get; private set; }
    public float Error { get; private set; }

    private void FixedUpdate()
    {
        BodyAngle = HelperMath.GetAngleBetween(Vector3.up, RobotController.robotBody.up, RobotController.robotBody.right);
        CalcGoalAngle();

        float inError = (BodyAngle - GoalBodyAngle)/_maxAbsValue;
        Error = PidController.CalcError(Time.fixedDeltaTime, inError);

        CalcDiffDrive();

        RobotController.LeftMotorPower = Error - Error * DiffDrive;
        RobotController.RightMotorPower = Error + Error * DiffDrive;
    }

    private void CalcGoalAngle()
    {
        float vertInput = Input.GetAxisRaw("Vertical");

        if (vertInput == 1f)
            GoalBodyAngle = movingAngle;
        else if (vertInput == -1f)
            GoalBodyAngle = -movingAngle;
        else
            GoalBodyAngle = 0f;
    }
    private void CalcDiffDrive()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        DiffDrive = diffDriveRate * horInput;
    }
}
