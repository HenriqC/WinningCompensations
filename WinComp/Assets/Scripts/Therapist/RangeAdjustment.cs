using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeAdjustment : MonoBehaviour
{
    public void SetRange(float range)
    {
        Instantiate_target.instance.subState = range;
    }
    public void SetRadius(float Radius)
    {
        Instantiate_target.instance.manualRadius = Radius;
    }
    public void SetSpeed (float Speed)
    {
        Instantiate_target.instance.manualSpeed = Speed;
    }

    public void ManualOnOff(bool isToggled)
    {
        if (isToggled)
        {
            Instantiate_target.instance.manualDiff.isOn = true;
        }
        else
        {
            Instantiate_target.instance.manualDiff.isOn = false;
        }
    }

    
}
