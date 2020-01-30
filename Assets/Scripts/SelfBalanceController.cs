using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://medium.com/husarion-blog/fresh-look-at-self-balancing-robot-algorithm-d50d41711d58
//TODO: importance calculated dynamically based on angle
public class SelfBalanceController : MonoBehaviour, IRobotController
{
    [SerializeField] RobotController _robotController;
    [SerializeField] PID _pidSpeed;
    [SerializeField] PID _pidAngle;
    [SerializeField] PID _pidLeftMotor;
    [SerializeField] PID _pidRightMotor;


    public RobotController Robot { get { return _robotController; } }
    public PID PIDSpeed { get { return _pidSpeed; } }
    public PID PIDAngle { get { return _pidAngle; } }
    public PID PIDLeftMotor { get { return _pidLeftMotor; } }
    public PID PIDRightMotor { get { return _pidRightMotor; } }


    public float BodyAngle { get; private set; }
    public float GoalBodyAngle { get; private set; }


    public float SpeedError { get; private set; }
    public float AngleError { get; private set; }
    public float TotalError { get; private set; }

    private void Start()
    {
        GameObject.FindObjectOfType<UIController>().robot = this;
    }
    private void Update()
    {
        //UpdateOnlyAngle();
        UpdateAngleAndGoalAngle();
        //UpdateAll();
    }

    private void UpdateOnlyAngle()
    {
        BodyAngle = Robot.GetBodyAngle();

        GoalBodyAngle = 0f;

        float angleInError = GoalBodyAngle - BodyAngle;
        AngleError = PIDAngle.CalcError(Time.fixedDeltaTime, angleInError);

        Robot.GoalAvgVelocity = -AngleError;
    }

    private void UpdateAngleAndGoalAngle()
    {
        BodyAngle = Robot.GetBodyAngle();

        float speedInError = Robot.InputAvgVelocity - Robot.AvgSignedVelocity;
        SpeedError = PIDSpeed.CalcError(Time.fixedDeltaTime, speedInError);
        GoalBodyAngle = SpeedError;

        float angleInError = GoalBodyAngle - BodyAngle;
        AngleError = PIDAngle.CalcError(Time.fixedDeltaTime, angleInError);

        Robot.GoalAvgVelocity = -AngleError;

    }

    private void UpdateAll()
    {
        BodyAngle = Robot.GetBodyAngle();

        float speedInError = Robot.InputAvgVelocity - Robot.AvgSignedVelocity;
        SpeedError = PIDSpeed.CalcError(Time.fixedDeltaTime, speedInError);
        GoalBodyAngle = SpeedError;

        float angleInError = GoalBodyAngle - BodyAngle;
        AngleError = PIDAngle.CalcError(Time.fixedDeltaTime, angleInError);

        //if (Mathf.Abs(AngleError) < 0.1f)
        //    Robot.
        //    Robot.GoalAvgVelocity = Robot.InputAvgVelocity;
        //else
        Robot.GoalAvgVelocity = /*GoalBodyAngle*/ -AngleError;
    }
}
