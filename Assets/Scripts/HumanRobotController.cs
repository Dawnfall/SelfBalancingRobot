using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRobotController : MonoBehaviour
{
    [SerializeField] RobotController Robot;

    private void Start()
    {
        //GameObject.FindObjectOfType<UIController>().robot = this;
    }
    private void Update()
    {
        if (Robot == null)
            return;

        Robot.GoalAvgVelocity = Robot.InputAvgVelocity;

    }
}
