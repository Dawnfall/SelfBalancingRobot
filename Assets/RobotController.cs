using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] Rigidbody leftWheel;
    [SerializeField] Rigidbody rightWheel;
    [SerializeField] Transform robotBody;

    [SerializeField] float speed;
    [SerializeField] float movingAngle = 20f;
    private void FixedUpdate()
    {
        float vert = Input.GetAxisRaw("Vertical");

        float left = 0.5f * vert + ((hor == 1f) ? 0.5f * vert : 0f);
        float right = 0.5f * vert + ((hor == -1f) ? 0.5f * vert : 0f);
        Debug.Log("left: " + left + " ,right: " + right);

        //leftWheel.angularVelocity = leftWheel.transform.right * left * speed;
        //rightWheel.angularVelocity = rightWheel.transform.right * right * speed;

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

    public float CalcError(float angle,float optimalAngle)
    {
        float currError = optimalAngle - angle;
        return currError;
    }
}
