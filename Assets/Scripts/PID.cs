using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PID
{
    [SerializeField] float _kp;
    [SerializeField] float _ki;
    [SerializeField] float _kd;

    public float Kp { get { return _kp; } set { _kp = value; } }
    public float Ki { get { return _ki; } set { _ki = value; } }
    public float Kd { get { return _kd; } set { _kd = value; } }

    public float Ep { get; private set; }
    public float Ei { get; private set; }
    public float Ed { get; private set; }
    public float Err { get; private set; }

    private float m_prevError = 0f;


    public float CalcError(float deltaTime, float error) //0 - 90
    {
        float ki = Ki * deltaTime;
        float kd = Kd * deltaTime;

        Ep = Kp * error; //proportional error
        Ei = Mathf.Clamp(Ei + error * ki, -1f, 1f); //integral error
        Ed = (error - m_prevError) * kd; //derivative error
        Err = Mathf.Clamp(Ep + Ei + Ed, -1f, 1f); //total error as the sum

        m_prevError = error;
        return Err;
    }
}