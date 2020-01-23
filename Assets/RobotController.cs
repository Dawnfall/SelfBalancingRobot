using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] Rigidbody leftWheel;
    [SerializeField] Rigidbody rightWheel;
    [SerializeField] Transform robotBody;
    [SerializeField] float speed;

    private void FixedUpdate()
    {
        float vert = Input.GetAxisRaw("Vertical");
        float hor = Input.GetAxisRaw("Horizontal");
        Debug.Log("vert: " + vert + " ,hor: " + hor);

        float left = 0.5f * vert + ((hor == 1f) ? 0.5f * vert : 0f);
        float right = 0.5f * vert + ((hor == -1f) ? 0.5f * vert : 0f);

        Debug.Log("left: " + left + " ,right: " + right);

        leftWheel.angularVelocity = leftWheel.transform.right * left * speed;
        rightWheel.angularVelocity = rightWheel.transform.right * right * speed;

        float bodyAngle = GetBodyAngle();
    }

    public float GetBodyAngle()
    {
        float angle = Vector3.Dot(robotBody.up, Vector3.up);
        if (angle == 0)
            return angle;

        Vector3 cross = Vector3.Cross(transform.up, Vector3.up);
        if (Vector3.Dot(cross, transform.right) > 0f)
            return angle;
        return -angle;
    }
}
