using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetAdjustments : MonoBehaviour
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

    public void SetTargetSize(float S)
    {
        Instantiate_target.instance.targetScale = new Vector3(S, S, S);
    }
    
    public void SetCognitiveSize(float cS)
    {
        CognitiveSphereSpawner.instance.cognitScale = new Vector3(cS, cS, cS);
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
