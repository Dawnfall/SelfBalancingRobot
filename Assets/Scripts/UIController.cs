using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static string format1 = "+0.0;-0.0";
    [SerializeField] Text _wheelsSpeedText;
    [SerializeField] Text _inputText;
    [SerializeField] Text _bodyText;
    [SerializeField] Text _pidAngleText;
    [SerializeField] Text _pidSpeedText;
    [SerializeField] Text _pidLeftMotorText;
    [SerializeField] Text _pidRightMotorText;

    public Robot robot;

    private void Update()
    {
        if (robot == null)
            return;

        _bodyText.text = $"Curr: {robot.BodyAngle.ToString(format1)}  Goal: {robot.Controller.GoalBodyAngle.ToString(format1)}";
        _inputText.text = $"Vert: {robot.VertInput.ToString(format1)}  Hor: {robot.HorInput.ToString(format1)}";

        _wheelsSpeedText.text =
            $"Speed\n" +
            $"Curr: {robot.AvgMotorPower.ToString(format1)}  Goal: {robot.Controller.GoalMotors.ToString(format1)}\n" +
            $"L: Curr: {robot.LeftMotorPower.ToString(format1)}  Goal: {robot.Controller.GoalLeftMotor.ToString(format1)}\n" +
            $"R: Curr: {robot.RightMotorPower.ToString(format1)}  Goal: {robot.Controller.GoalRightMotor.ToString(format1)}";

        _pidAngleText.text =
            $"Ep: {robot.Controller.PIDAngle.Ep.ToString(format1)}  Ei: {robot.Controller.PIDAngle.Ei.ToString(format1)}  Ed: {robot.Controller.PIDAngle.Ed.ToString(format1)}\n" +
            $"Err: {robot.Controller.PIDAngle.Err.ToString(format1)}";
        _pidSpeedText.text =
            $"Ep: {robot.Controller.PIDSpeed.Ep.ToString(format1)}  Ei: {robot.Controller.PIDSpeed.Ei.ToString(format1)}  Ed: {robot.Controller.PIDSpeed.Ed.ToString(format1)}\n" +
            $"Err: {robot.Controller.PIDSpeed.Err.ToString(format1)}";
        _pidLeftMotorText.text =
            $"Ep: {robot.Controller.PIDLeftMotor.Ep.ToString(format1)}  Ei: {robot.Controller.PIDLeftMotor.Ei.ToString(format1)}  Ed: {robot.Controller.PIDLeftMotor.Ed.ToString(format1)}\n" +
            $"Err: {robot.Controller.PIDLeftMotor.Err.ToString(format1)}";
        _pidRightMotorText.text =
            $"Ep: {robot.Controller.PIDRightMotor.Ep.ToString(format1)}  Ei: {robot.Controller.PIDRightMotor.Ei.ToString(format1)}  Ed: {robot.Controller.PIDRightMotor.Ed.ToString(format1)}\n" +
            $"Err: {robot.Controller.PIDRightMotor.Err.ToString(format1)}";
    }
}
