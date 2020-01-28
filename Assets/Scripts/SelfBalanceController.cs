using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://medium.com/husarion-blog/fresh-look-at-self-balancing-robot-algorithm-d50d41711d58
//TODO: importance calculated dynamically based on angle
public class SelfBalanceController : MonoBehaviour, IRobotController
{
    [SerializeField] RobotController _robotController;
    [SerializeField] PID _pidMotor;
    [SerializeField] PID _pidAngle;
    [SerializeField] float _diffDriveRate;
    [SerializeField] [Range(0f, 1f)] float _angleToSpeedRatio;

    public RobotController Robot { get { return _robotController; } }
    public PID PIDMotor { get { return _pidMotor; } set { _pidMotor = value; } }
    public PID PIDAngle { get { return _pidAngle; } set { _pidAngle = value; } }
    public float DiffDriveRate { get { return _diffDriveRate; } set { _diffDriveRate = value; } }

    public float BodyAngle { get; private set; }
    public float GoalBodyAngle { get; private set; }

    public float GoalMotorPower { get; private set; }

    public float AngleToSpeedRatio { get { return _angleToSpeedRatio; } set { _angleToSpeedRatio = value; } }

    public float MotorError { get; private set; }
    public float AngleError { get; private set; }
    public float TotalError { get; private set; }

    private void Start()
    {
        GameObject.FindObjectOfType<UIController>().robot = this;
    }
    private void Update()
    {
        GoalMotorPower = Input.GetAxisRaw("Vertical");

        BodyAngle = Robot.GetBodyAngle();
        GoalBodyAngle = 0f;

        Robot.DiffDrive = DiffDriveRate * Input.GetAxisRaw("Horizontal");

        float motorInError = GoalMotorPower - Robot.MotorPower;
        MotorError = PIDMotor.CalcError(Time.fixedDeltaTime, motorInError);

        float angleInError = (GoalBodyAngle - BodyAngle) / Robot.MaxBodyAngle - MotorError;
        AngleError = PIDAngle.CalcError(Time.fixedDeltaTime, angleInError);

        //loat totalError = 0f;
        //if (Mathf.Abs(AngleError) > AngleToSpeedRatio)
        //    TotalError = AngleError;// Mathf.Clamp(totalErr, -1f, 1f);
        //else
        //    TotalError = -MotorError;

        Robot.MotorPower = AngleError;// +MotorError;
        //float totalErr = (1f - AngleToSpeedRatio) * AngleError + AngleToSpeedRatio * MotorError;
    }
}
