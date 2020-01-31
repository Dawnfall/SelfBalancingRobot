using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float distance;

    [SerializeField] Robot Robot;

    private void Update()
    {
        if (Robot == null)
            return;

        Vector3 leftWheelPos = Robot.LeftWheelRigidbody.transform.position;
        Vector3 rightWheelPos = Robot.RightWheelRigidbody.transform.position;

        Vector3 centerPoint = leftWheelPos + (rightWheelPos - leftWheelPos);

        Vector3 camPos = centerPoint + Vector3.up * distance;

        transform.position = camPos;

    }
}
