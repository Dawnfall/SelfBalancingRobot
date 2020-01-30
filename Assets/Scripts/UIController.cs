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
    [SerializeField] Text _motorPowerText;
    [SerializeField] Text _pidSpeedText;

    //[SerializeField] SelfBalanceController _robot;
    public SelfBalanceController robot;

    private void Update()
    {
        if (robot == null)
            return;

        _bodyText.text = $"Curr: {robot.BodyAngle.ToString(format2)}  Goal: {robot.GoalBodyAngle.ToString(format2)}";

        _wheelsSpeedText.text =
            $"Speed\n" +
            $"Curr: {robot.Robot.AvgSignedVelocity.ToString(format2)}  Goal: {robot.Robot.GoalAvgVelocity.ToString(format2)}  Input: {robot.Robot.InputAvgVelocity.ToString(format2)}\n" +
            $"L: Curr: {robot.Robot.LeftWheel.SignedVelocity.ToString(format2)}  Goal: {robot.Robot.LeftWheel.GoalVelocity.ToString(format2)}  Err: {robot.Robot.LeftWheel.VelocityError}\n" +
            $"R: Curr: {robot.Robot.RightWheel.SignedVelocity.ToString(format2)}  Goal: {robot.Robot.RightWheel.GoalVelocity.ToString(format2)}  Err: {robot.Robot.RightWheel.VelocityError}";
        _wheelsMotorsText.text =
            $"Motor\n" +
            $"L: {robot.Robot.LeftWheel.MotorPower}\n" +
            $"R: {robot.Robot.RightWheel.MotorPower}";

        _pidAngleText.text =
            $"Ep: {robot.PIDAngle.Ep.ToString(format3)}  Ei: {robot.PIDAngle.Ei.ToString(format3)}  Ed: {robot.PIDAngle.Ed.ToString(format3)}\n" +
            $"Err: {robot.PIDAngle.Err.ToString(format3)}";
        _pidSpeedText.text =
            $"Ep: {robot.PIDSpeed.Ep.ToString(format3)}  Ei: {robot.PIDSpeed.Ei.ToString(format3)}  Ed: {robot.PIDSpeed.Ed.ToString(format3)}\n" +
            $"Err: {robot.PIDSpeed.Err.ToString(format3)}";
        //_motorPowerText.text = $"Curr: Goal: {selfBalanceController.GoalMotorPower.ToString(format2)}";




    }

    /* {robot.Robot.GoalMotorPower.ToString(format2)} */
}
