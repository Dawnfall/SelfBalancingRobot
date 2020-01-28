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
    public IRobotController robot;

    private void Update()
    {
        if (robot == null)
            return;

        _wheelsSpeedText.text = $"Speed L: {robot.Robot.LeftWheelSpeed}  R: {robot.Robot.RightWheelSpeed}";
        _wheelsTorqueText.text = $"Motor L:{robot.Robot.LeftMotorPower.ToString("0.000")}  R: {robot.Robot.RightMotorPower.ToString("0.000")}";

        SelfBalanceController selfBalanceController = robot as SelfBalanceController;
        if (selfBalanceController != null)
        {
            _bodyText.text = $"Curr: {selfBalanceController.BodyAngle.ToString("0.00")}  Goal: {selfBalanceController.GoalBodyAngle.ToString("0.00")}";
            _pidAngleText.text =
                $"Ep: {selfBalanceController.PIDAngle.Ep.ToString("0.000")}  Ei: {selfBalanceController.PIDAngle.Ei.ToString("0.000")}  Ed: {selfBalanceController.PIDAngle.Ed.ToString("0.000")}\n" +
                $"Err: {selfBalanceController.PIDAngle.Err.ToString("0.000")}";

            _motorPowerText.text = $"Curr: {robot.Robot.MotorPower.ToString("0.00")}  Goal: {selfBalanceController.GoalMotorPower.ToString("0.00")}";
            _pidMotorText.text =
            $"Ep: {selfBalanceController.PIDMotor.Ep.ToString("0.000")}  Ei: {selfBalanceController.PIDMotor.Ei.ToString("0.000")}  Ed: {selfBalanceController.PIDMotor.Ed.ToString("0.000")}\n" +
            $"Err: {selfBalanceController.PIDMotor.Err.ToString("0.000")}";
        }


    }


}
