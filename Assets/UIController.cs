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
        float bodyAngle = m_robot.GetBodyAngle();
        float goalAngle = m_robot.CalcDesiredAngle();
        float error = m_robot.CalcError(bodyAngle, goalAngle);

        m_text.text =
            $"Left Wheel Speed: {m_robot.LeftWheelSpeed}\n" +
            $"Right Wheel Speed: {m_robot.RightWheelSpeed}\n" +
            $"Body Angle: {bodyAngle}\n" +
            $"DesiredAngle: {goalAngle}\n" +
            $"Delta Angle: {error}\n" +
            $"Right Motor: {m_robot.RightMotorSpeed}\n" +
            $"Left Motor: {m_robot.LeftMotorSpeed}";
    }
}
