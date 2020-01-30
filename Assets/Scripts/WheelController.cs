using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelController
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] PID _wheelPid;

    public Rigidbody WheelRigidBody { get { return _rigidbody; } }
    public PID WheelPID { get { return _wheelPid; } }

    //*****************
    // Velocity

    public float GoalVelocity { get; private set; }
    public float SignedVelocity
    {
        get
        {
            if (Vector3.Dot(WheelRigidBody.velocity, WheelForward) > 0f)
                return WheelRigidBody.velocity.magnitude;
            return -WheelRigidBody.velocity.magnitude;
        }
    }
    public float VelocityError { get; private set; }

    public float MotorPower { get; private set; }


    //public float GoalMotorPower { get; private set; }

    //public PID PIDWheel { get { return _wheelPid; } }

    public Vector3 WheelForward
    {
        get
        {
            return Vector3.Cross(WheelRigidBody.transform.right, Vector3.up).normalized;
        }
    }

    public void UpdateWheel(float goalAvgVelocity, float maxVelocity, float maxMotorPower)
    {
        GoalVelocity = goalAvgVelocity;
        WheelRigidBody.velocity = WheelForward * GoalVelocity;

        //float velocityInError = GoalVelocity - SignedVelocity;
        //VelocityError = WheelPID.CalcError(Time.fixedDeltaTime, velocityInError);

        ////TODO: pid
        //MotorPower = VelocityError * maxMotorPower;
        ////Debug.Log(MotorPower);
        //WheelRigidBody.AddForce(WheelForward * MotorPower);
    }

}
