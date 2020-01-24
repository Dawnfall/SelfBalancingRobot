using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] RobotController m_robot;
    [SerializeField] Text m_text;

    private void Update()
    {
        m_text.text =
            $"Left Wheel Speed: {m_robot.LeftWheelSpeed}\n" +
            $"Right Wheel Speed: {m_robot.RightWheelSpeed}\n" +
            $"Body Angle: {m_robot.BodyAngle}\n" +
            $"DesiredAngle: {m_robot.GoalBodyAngle}\n" +
            $"Delta Angle: {m_robot.AngleError}\n" +
            $"Right Motor: {m_robot.RightMotorSpeed}\n" +
            $"Left Motor: {m_robot.LeftMotorSpeed}";
    }
}
