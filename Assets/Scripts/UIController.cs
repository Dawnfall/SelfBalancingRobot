using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text _wheelsSpeedText;
    [SerializeField] Text _wheelsTorqueText;
    [SerializeField] Text _bodyText;
    [SerializeField] Text _pidText;
    [SerializeField] SelfBalanceController _robot;

    private void Awake()
    {
        _robot = GameObject.FindObjectOfType<SelfBalanceController>();
    }
    private void Update()
    {
        _wheelsSpeedText.text = $"Speed L: {_robot.RobotController.LeftWheelSpeed}  R: {_robot.RobotController.RightWheelSpeed}";
        _wheelsTorqueText.text = $"Motor L:{_robot.RobotController.LeftMotorPower.ToString("0.000")}  R: {_robot.RobotController.RightMotorPower.ToString("0.000")}";
        _bodyText.text = $"Ang: {_robot.BodyAngle.ToString("0.00")}  Goal: {_robot.GoalBodyAngle.ToString("0.00")}  Err: {_robot.Error.ToString("0.00")}";
        _pidText.text =
            $"Ep: {_robot.PidController.Ep.ToString("0.000")}  Ei: {_robot.PidController.Ei.ToString("0.000")}  Ed: {_robot.PidController.Ed.ToString("0.000")}\n" +
            $"Err: {_robot.PidController.Err.ToString("0.000")}";
    }
}
