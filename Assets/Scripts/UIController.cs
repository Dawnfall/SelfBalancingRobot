using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static string format2 = "0.00";
    static string format3 = "0.000";
    [SerializeField] Text _wheelsSpeedText;
    [SerializeField] Text _wheelsMotorsText;
    [SerializeField] Text _bodyText;
    [SerializeField] Text _pidAngleText;
    [SerializeField] Text _pidSpeedText;

    public Robot robot;

    private void Update()
    {
        if (robot == null)
            return;

        _bodyText.text = $"Curr: {robot.BodyAngle.ToString(format2)}  Goal: {robot.Controller.GoalBodyAngle.ToString(format2)}";

        _wheelsSpeedText.text =
            $"Speed\n" +
            $"Curr: {robot.AvgMotorPower.ToString(format2)}  Goal: {robot.Controller.GoalMotors.ToString(format2)}  Input: {robot.VertInput.ToString(format2)}\n" +
            $"L: Curr: {robot.LeftMotorPower.ToString(format2)}  Goal: {robot.Controller.GoalLeftMotor.ToString(format2)}  Err: {robot.Controller.LeftWheelMotorError}\n" +
            $"R: Curr: {robot.RightMotorPower.ToString(format2)}  Goal: {robot.Controller.GoalRightMotor.ToString(format2)}  Err: {robot.Controller.RightWheelMotorError}";
        _wheelsMotorsText.text =
            $"Motor\n" +
            $"L: {robot.LeftMotorPower}\n" +
            $"R: {robot.RightMotorPower}";

        _pidAngleText.text =
            $"Ep: {robot.Controller.PIDAngle.Ep.ToString(format3)}  Ei: {robot.Controller.PIDAngle.Ei.ToString(format3)}  Ed: {robot.Controller.PIDAngle.Ed.ToString(format3)}\n" +
            $"Err: {robot.Controller.PIDAngle.Err.ToString(format3)}";
        _pidSpeedText.text =
            $"Ep: {robot.Controller.PIDSpeed.Ep.ToString(format3)}  Ei: {robot.Controller.PIDSpeed.Ei.ToString(format3)}  Ed: {robot.Controller.PIDSpeed.Ed.ToString(format3)}\n" +
            $"Err: {robot.Controller.PIDSpeed.Err.ToString(format3)}";
    }
}
