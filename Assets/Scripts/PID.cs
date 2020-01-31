using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PID
{
    [SerializeField] float _kp;
    [SerializeField] float _ki;
    [SerializeField] float _kd;
    [SerializeField] float _integralClamp = 0f;
    public float Kp { get { return _kp; } set { _kp = value; } }
    public float Ki { get { return _ki; } set { _ki = value; } }
    public float Kd { get { return _kd; } set { _kd = value; } }
    public float IntegralClamp { get { return _integralClamp; } set { _integralClamp = value; } }

    public float Ep { get; private set; }
    public float Ei { get; private set; }
    public float Ed { get; private set; }
    public float Err { get; private set; }

    private float m_prevError = 0f;


    public float CalcError(float deltaTime, float error) //0 - 90
    {
        float ki = Ki * deltaTime;
        float kd = Kd * deltaTime * 1000f;

        //proportional error
        Ep = Kp * error;

        //integral error
        if (IntegralClamp != 0f)
            Ei = Mathf.Clamp(Ei + error * ki, -IntegralClamp, IntegralClamp);
        else
            Ei = Ei + error * ki; //integral error

        //derivative error
        Ed = (error - m_prevError) * kd;
        m_prevError = error;

        Err = Ep + Ei + Ed; //total error as the sum

        return Err;
    }
}