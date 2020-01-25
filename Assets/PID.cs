using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID : MonoBehaviour
{
    [SerializeField] float _ki;
    [SerializeField] float _kp;
    [SerializeField] float _kd;
    [SerializeField] float _maxAbsValue;

    public float Kp { get; set; }
    public float Ki { get; set; }
    public float Kd { get; set; }
    public float MaxAbsValue { get; set; }

    public float Ep { get; private set; }
    public float Ei { get; private set; }
    public float Ed { get; private set; }
    public float Err { get; private set; }

    private float m_prevError = 0f;

    private void Awake()
    {
        Kp = _kp;
        Ki = _ki;
        Kd = _kd;
        MaxAbsValue = _maxAbsValue;
    }


    public float CalcError(float deltaTime, float angle, float goalAngle) //0 - 90
    {
        float ki = Ki * deltaTime;
        float kd = Kd * deltaTime;

        Debug.Log("coefs : " + Kp + " , " + ki + " , " + kd);
        float error = angle - goalAngle;

        Ep = Kp * error; //proportional error
        Ei = Mathf.Clamp(Ei + error * ki, -MaxAbsValue, MaxAbsValue); //integral error
        Ed = (error - m_prevError) * kd; //derivative error
        Err = Mathf.Clamp(Ep + Ei + Ed, -MaxAbsValue, MaxAbsValue); //total error as the sum

        m_prevError = error;
        return Err;
    }
}