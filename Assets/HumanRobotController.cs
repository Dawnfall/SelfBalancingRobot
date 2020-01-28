using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanRobotController : MonoBehaviour, IRobotController
{
    [SerializeField] RobotController _robotController;
    public float diffDriveRate;

    public RobotController Robot { get { return _robotController; } }
    public float LeftMotorPower { get; set; }
    public float RightMotorPower { get; set; }

    private void Start()
    {
        GameObject.FindObjectOfType<UIController>().robot = this;
    }

    private void Update()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        float horInput = Input.GetAxisRaw("Horizontal");

        _robotController.MotorPower = vertInput;
        _robotController.DiffDrive = diffDriveRate * horInput;

    }

}
