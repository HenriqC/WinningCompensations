using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetAdjustments : MonoBehaviour
{
    public Toggle manual;
    public Slider tSize;
    public Slider tRange;
    public Slider tRadius;
    public Slider tDuration;
    public Slider cogSize;
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

    public void CognitiveOnOff (bool isToggled)
    {
        if (isToggled)
        {
            CognitiveSphereSpawner.instance.stopSpawning_P = true;

            var purpleSphere = GameObject.FindGameObjectsWithTag("CognitiveCollider_P");
            foreach (GameObject target_p in purpleSphere)
            {
                Object.Destroy(target_p);
            }
        }
    }

    public void ResetParams()
    {
        var purpleSphere = GameObject.FindGameObjectsWithTag("CognitiveCollider_P");
        foreach (GameObject target_p in purpleSphere)
        {
            Object.Destroy(target_p);
        }

        Instantiate_target.instance.manualDiff.isOn = false;
        Instantiate_target.instance.nTargets = 0;
        manual.isOn = false;
        tSize.value = 0.125f;
        tRange.value = 2;
        tRadius.value = 0.35f;
        tDuration.value = 0.55f;
        cogSize.value = 0.125f;
    }
    
}
