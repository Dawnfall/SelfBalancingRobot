using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] RobotController m_robot;
    [SerializeField] Text m_robotText;
    [SerializeField] Text m_errorText;

    private void Update()
    {
        m_robotText.text =
            $"Left Wheel Speed: {m_robot.LeftWheelSpeed}\n" +
            $"Right Wheel Speed: {m_robot.RightWheelSpeed}\n" +
            $"Body Angle: {m_robot.BodyAngle}\n" +
            $"DesiredAngle: {m_robot.GoalBodyAngle}\n" +
            $"Delta Angle: {m_robot.Error}\n" +
            $"Right Motor: {m_robot.RightMotorSpeed}\n" +
            $"Left Motor: {m_robot.LeftMotorSpeed}";

        m_errorText.text =
            $"Ep: {m_robot.PidController.Ep}  \n" +
            $"Ei: {m_robot.PidController.Ei}  \n" +
            $"Ed: {m_robot.PidController.Ed}  \n" +
            $"E:  {m_robot.PidController.Err}  ";
    }
}
