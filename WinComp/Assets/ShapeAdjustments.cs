using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAdjustments : MonoBehaviour
{
    public void SetTolerance(float compMax)
    {
        Instantiate_target.instance.maxComp = compMax;
    }

    public void ManualOnOff_Shapes (bool isToggled)
    {
        if (isToggled)
        {
            Instantiate_target.instance.compCount = 0;
            Instantiate_target.instance.manualDiff_Shapes.isOn = true;
        }
        else
        {
            Instantiate_target.instance.manualDiff_Shapes.isOn = false;
        }
    }
}
