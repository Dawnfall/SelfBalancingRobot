using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://medium.com/husarion-blog/fresh-look-at-self-balancing-robot-algorithm-d50d41711d58
//TODO: importance calculated dynamically based on angle
public class AdvancedRobot : MonoBehaviour
{
    [SerializeField] RobotController _robotController;
    [SerializeField] PID _pidMotor;
    [SerializeField] PID _pidAngle;
    [SerializeField] float _diffDriveRate;
    [SerializeField] [Range(0f, 1f)] float _angleToSpeedRatio;

    public RobotController Robot { get { return _robotController; } private set { _robotController = value; } }
    public PID PIDMotor { get { return _pidMotor; } set { _pidMotor = value; } }
    public PID PIDAngle { get { return _pidAngle; } set { _pidAngle = value; } }
    public float DiffDriveRate { get { return _diffDriveRate; } set { _diffDriveRate = value; } }

    public float BodyAngle { get; private set; }
    public float GoalBodyAngle { get; private set; }

    public float GoalMotorPower { get; private set; }
    public float AvgMotorPower { get; private set; }

    public float DiffDrive { get; private set; }
    public float AngleToSpeedRatio { get { return _angleToSpeedRatio; } set { _angleToSpeedRatio = value; } }


    public float MotorError { get; private set; }
    public float AngleError { get; private set; }
    public float TotalError { get; private set; }


    private void Update()
    {
        AvgMotorPower = (Robot.LeftMotorPower + Robot.RightMotorPower) / 2f;
        GoalMotorPower = Input.GetAxisRaw("Vertical");

        BodyAngle = Robot.GetBodyAngle();
        GoalBodyAngle = 0f;

        DiffDrive = DiffDriveRate * Input.GetAxisRaw("Horizontal");

        float motorInError = GoalMotorPower - AvgMotorPower;
        MotorError = PIDMotor.CalcError(Time.fixedDeltaTime, motorInError);

        float angleInError = (GoalBodyAngle - BodyAngle) / Robot.MaxBodyAngle;
        AngleError = PIDAngle.CalcError(Time.fixedDeltaTime, angleInError);

        float totalError = 0f;
        if (Mathf.Abs(AngleError) > AngleToSpeedRatio)
            TotalError = AngleError;// Mathf.Clamp(totalErr, -1f, 1f);
        else
            TotalError = -MotorError;
        //float totalErr = (1f - AngleToSpeedRatio) * AngleError + AngleToSpeedRatio * MotorError;


        Robot.LeftMotorPower = (1f - DiffDrive) * TotalError;
        Robot.RightMotorPower = (1f + DiffDrive) * TotalError;
    }
}
