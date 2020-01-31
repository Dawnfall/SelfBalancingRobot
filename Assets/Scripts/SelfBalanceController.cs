using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*********************
// Uses 4 PID controllers
//
// PIDspeed calculates desired angle based on current average motor speed and desired motor speed
// PIDangle calculates desired average motor speed based on desired goal angle and current angle
// PIDleftMotor and PIDrightMotor calculate each motor speed based on each desired speed(influenced also by differential drive) and current speed

public class SelfBalanceController : MonoBehaviour
{
    [SerializeField] PID _pidSpeed;
    [SerializeField] PID _pidAngle;
    [SerializeField] PID _pidLeftMotor;
    [SerializeField] PID _pidRightMotor;

    public PID PIDSpeed { get { return _pidSpeed; } }
    public PID PIDAngle { get { return _pidAngle; } }
    public PID PIDLeftMotor { get { return _pidLeftMotor; } }
    public PID PIDRightMotor { get { return _pidRightMotor; } }


    //******************
    // Goals calculated by this controller

    public float GoalBodyAngle { get; private set; }
    public float GoalDiffDrive { get; private set; }

    public float GoalMotors { get; private set; }
    public float GoalLeftMotor { get { return GoalMotors * (1f + GoalDiffDrive); } }
    public float GoalRightMotor { get { return GoalMotors * (1f - GoalDiffDrive); } }


    //*****************
    // PID errors

    public float MotorError { get; private set; }
    public float AngleError { get; private set; }
    public float LeftWheelMotorError { get; private set; }
    public float RightWheelMotorError { get; private set; }

    public void HandleRobot(Robot robot)
    {
        //diff drive based on horizontal input
        GoalDiffDrive = robot.DiffDriveRate * robot.HorInput;

        //motor speed error to get desired body angle
        float motorInError = robot.VertInput - robot.AvgMotorPower / robot.MaxMotorPower;
        MotorError = PIDSpeed.CalcError(Time.fixedDeltaTime, motorInError);
        GoalBodyAngle = MotorError;

        //angle error to get desired velocity
        float angleInError = GoalBodyAngle - robot.BodyAngle;
        AngleError = PIDAngle.CalcError(Time.fixedDeltaTime, angleInError);

        GoalMotors = -AngleError;

        //we set each wheel motor power based on their appropriate goals
        float leftVelInError = GoalLeftMotor - robot.LeftMotorPower;
        LeftWheelMotorError = PIDLeftMotor.CalcError(Time.fixedDeltaTime, leftVelInError);
        robot.LeftMotorPower = Mathf.Clamp(LeftWheelMotorError, -robot.MaxMotorPower, robot.MaxMotorPower);

        float rightVelInError = GoalRightMotor - robot.RightMotorPower;
        RightWheelMotorError = PIDRightMotor.CalcError(Time.fixedDeltaTime, rightVelInError);
        robot.RightMotorPower = Mathf.Clamp(RightWheelMotorError, -robot.MaxMotorPower, robot.MaxMotorPower);
    }
}