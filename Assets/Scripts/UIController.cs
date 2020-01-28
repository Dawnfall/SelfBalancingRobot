using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text _wheelsSpeedText;
    [SerializeField] Text _wheelsTorqueText;
    [SerializeField] Text _bodyText;
    [SerializeField] Text _pidAngleText;
    [SerializeField] Text _motorPowerText;
    [SerializeField] Text _pidMotorText;

    //[SerializeField] SelfBalanceController _robot;
    [SerializeField] AdvancedRobot _advRobot;
    private void Awake()
    {
        //_robot = GameObject.FindObjectOfType<SelfBalanceController>();
        _advRobot = GameObject.FindObjectOfType<AdvancedRobot>();
    }
    private void Update()
    {
        //_wheelsSpeedText.text = $"Speed L: {_robot.RobotController.LeftWheelSpeed}  R: {_robot.RobotController.RightWheelSpeed}";
        //_wheelsTorqueText.text = $"Motor L:{_robot.RobotController.LeftMotorPower.ToString("0.000")}  R: {_robot.RobotController.RightMotorPower.ToString("0.000")}";
        //_bodyText.text = $"Ang: {_robot.BodyAngle.ToString("0.00")}  Goal: {_robot.GoalBodyAngle.ToString("0.00")}  Err: {_robot.Error.ToString("0.00")}";
        //_pidText.text =
        //    $"Ep: {_robot.PidController.Ep.ToString("0.000")}  Ei: {_robot.PidController.Ei.ToString("0.000")}  Ed: {_robot.PidController.Ed.ToString("0.000")}\n" +
        //    $"Err: {_robot.PidController.Err.ToString("0.000")}";

        _wheelsSpeedText.text = $"Speed L: {_advRobot.Robot.LeftWheelSpeed}  R: {_advRobot.Robot.RightWheelSpeed}";
        _wheelsTorqueText.text = $"Motor L:{_advRobot.Robot.LeftMotorPower.ToString("0.000")}  R: {_advRobot.Robot.RightMotorPower.ToString("0.000")}";

        _bodyText.text = $"Curr: {_advRobot.BodyAngle.ToString("0.00")}  Goal: {_advRobot.GoalBodyAngle.ToString("0.00")}";
        _pidAngleText.text =
            $"Ep: {_advRobot.PIDAngle.Ep.ToString("0.000")}  Ei: {_advRobot.PIDAngle.Ei.ToString("0.000")}  Ed: {_advRobot.PIDAngle.Ed.ToString("0.000")}\n" +
            $"Err: {_advRobot.PIDAngle.Err.ToString("0.000")}";

        _motorPowerText.text = $"Curr: {_advRobot.AvgMotorPower.ToString("0.00")}  Goal: {_advRobot.GoalMotorPower.ToString("0.00")}";
        _pidMotorText.text =
        $"Ep: {_advRobot.PIDMotor.Ep.ToString("0.000")}  Ei: {_advRobot.PIDMotor.Ei.ToString("0.000")}  Ed: {_advRobot.PIDMotor.Ed.ToString("0.000")}\n" +
        $"Err: {_advRobot.PIDMotor.Err.ToString("0.000")}";
    }


}
